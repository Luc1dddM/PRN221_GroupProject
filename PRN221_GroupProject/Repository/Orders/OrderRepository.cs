using ExcelDataReader;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Enums;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Users;
using System.Data;

namespace PRN221_GroupProject.Repository.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly IUserRepository _userRepository;
        public OrderRepository(Prn221GroupProjectContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task CreateOrderHeader(OrderHeader orderHeader, string userId)
        {

            try
            {
                orderHeader.OrderStatus = OrderStatusEnum.Pending.ToString();
                orderHeader.TotalPrice = orderHeader.TotalPrice;
                orderHeader.CreatedBy = userId;
                orderHeader.CreatedDate = DateTime.Now;
                _context.OrderHeaders.Add(orderHeader);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task CreateOrderDetail(CartDetail cartDetail, string orderHeaderId)
        {
            try
            {
                var orderHeader = GetOrderHeaderById(orderHeaderId);

                OrderDetail newOrderDetail = new OrderDetail
                {
                    OrderHeaderId = orderHeader.OrderHeaderId,
                    ProductId = cartDetail.ProductId,
                    Color = cartDetail.Color,
                    Count = cartDetail.Count,
                    Price = cartDetail.Price,
                };
                await _context.OrderDetails.AddAsync(newOrderDetail);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task AdminChangeOrderStatus(OrderHeader orderHeader, string userId)
        {
            try
            {
                if (orderHeader.OrderStatus.Equals(OrderStatusEnum.Pending.ToString()))
                {
                    orderHeader.OrderStatus = OrderStatusEnum.Approved.ToString();
                }
                else if (orderHeader.OrderStatus.Equals(OrderStatusEnum.Approved.ToString()))
                {
                    orderHeader.OrderStatus = OrderStatusEnum.Processing.ToString();
                }
                else if (orderHeader.OrderStatus.Equals(OrderStatusEnum.Processing.ToString()))
                {
                    orderHeader.OrderStatus = OrderStatusEnum.Shipping.ToString();
                }
                else if (orderHeader.OrderStatus.Equals(OrderStatusEnum.Pending.ToString()) ||
                         orderHeader.OrderStatus.Equals(OrderStatusEnum.Approved.ToString()) ||
                         orderHeader.OrderStatus.Equals(OrderStatusEnum.Processing.ToString()))
                {
                    orderHeader.OrderStatus = OrderStatusEnum.Cancelled.ToString();
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task CustomerChangeOrderStatus(OrderHeader orderHeader, string userId)
        {
            try
            {
                var existingOrderHeader = await _context.OrderHeaders.FirstOrDefaultAsync(o => o.OrderHeaderId == orderHeader.OrderHeaderId);
                if (existingOrderHeader != null)
                {
                    if (orderHeader.OrderStatus.Equals(OrderStatusEnum.Pending.ToString()) ||
                                        orderHeader.OrderStatus.Equals(OrderStatusEnum.Approved.ToString()) ||
                                        orderHeader.OrderStatus.Equals(OrderStatusEnum.Processing.ToString()))
                    {
                        orderHeader.OrderStatus = OrderStatusEnum.Cancelled.ToString();
                    }
                    else if (orderHeader.OrderStatus.Equals(OrderStatusEnum.Shipping.ToString()))
                    {
                        orderHeader.OrderStatus = OrderStatusEnum.Shipped.ToString();
                    }
                    else if (orderHeader.OrderStatus.Equals(OrderStatusEnum.Shipped.ToString()))
                    {
                        orderHeader.OrderStatus = OrderStatusEnum.Refunded.ToString();
                    }
                }
                _context.OrderHeaders.Update(existingOrderHeader);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public OrderHeader GetOrderHeaderByUserId(string userId)
        {
            try
            {
                return _context.OrderHeaders.FirstOrDefault(o => o.CreatedBy == userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public OrderDetail GetOrderDetailByOrderHeaderId(string orderHeaderId)
        {
            try
            {
                return _context.OrderDetails.FirstOrDefault(od => od.OrderHeaderId == orderHeaderId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public OrderHeader GetOrderHeaderById(string orderHeaderId)
        {
            try
            {
                return _context.OrderHeaders.Include(oh => oh.OrderDetails)
                                            .FirstOrDefault(oh => oh.OrderHeaderId.Equals(orderHeaderId));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public OrderListDTO GetList(string[] statusesParam, string[] categoriesParam, string sortBy, string sortOrder, string searchterm, int pageNumberParam, int pageSizeParam)
        {
            //Get List from db
            var result = _context.OrderHeaders.ToList();

            //Call filter function 
            result = Filter(statusesParam, categoriesParam, result);
            result = Search(result, searchterm);
            result = Sort(sortBy, sortOrder, result);

            //Calculate pagination
            var totalItems = result.Count();
            var TotalPages = (int)Math.Ceiling((double)totalItems / pageSizeParam);

            result = result.Skip((pageNumberParam - 1) * pageSizeParam)
                           .Take(pageSizeParam)
                           .ToList();

            return new OrderListDTO
            {
                listOrder = result,
                totalPages = TotalPages,
            };
        }

        private List<OrderHeader> Filter(string[] statuses, string[] categories, List<OrderHeader> list) //string coupon
        {
            if (statuses != null && statuses.Length > 0)
            {
                list = list.Where(l => statuses.Contains(l.OrderStatus)).ToList();
            }

            if (categories != null && categories.Length > 0)
            {
                list = list.Where(l => l.CouponId != null).ToList();
            }
            return list;
        }

        private List<OrderHeader> Search(List<OrderHeader> list, string searchtearm)
        {
            if (!string.IsNullOrEmpty(searchtearm))
            {
                list = list.Where(l =>
                            l.Name.Contains(searchtearm, StringComparison.OrdinalIgnoreCase) ||
                            l.Phone.Contains(searchtearm, StringComparison.OrdinalIgnoreCase) ||
                            (l.Email?.Contains(searchtearm, StringComparison.OrdinalIgnoreCase) ?? false))
                            .ToList();
            }
            return list;
        }

        private List<OrderHeader> Sort(string sortBy, string sortOrder, List<OrderHeader> list)
        {
            switch (sortBy)
            {
                case "name":
                    list = sortOrder == "asc" ? list.OrderBy(o => o.Name).ToList() : list.OrderByDescending(o => o.Name).ToList();
                    break;

                case "phone":
                    list = sortOrder == "asc" ? list.OrderBy(o => o.Phone).ToList() : list.OrderByDescending(o => o.Phone).ToList();
                    break;

                case "email":
                    list = sortOrder == "asc" ? list.OrderBy(o => o.Email).ToList() : list.OrderByDescending(o => o.Email).ToList();
                    break;

                case "payment":
                    list = sortOrder == "asc" ? list.OrderBy(o => o.PaymentMethod).ToList() : list.OrderByDescending(o => o.PaymentMethod).ToList();
                    break;

                case "status":
                    list = sortOrder == "asc" ? list.OrderBy(o => o.OrderStatus).ToList() : list.OrderByDescending(o => o.OrderStatus).ToList();
                    break;

                case "createBy":
                    list = sortOrder == "asc" ? list.OrderBy(o => o.CreatedBy).ToList() : list.OrderByDescending(o => o.CreatedBy).ToList();
                    break;

                case "createDate":
                    list = sortOrder == "asc" ? list.OrderBy(o => o.CreatedDate).ToList() : list.OrderByDescending(o => o.CreatedDate).ToList();
                    break;

                default:
                    list = list.OrderByDescending(o => o.Id).ToList();
                    break;
            }
            return list;
        }

        public async Task ImportOrdersFile(IFormFile excelFile, string user)
        {
            try
            {
                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\uploads\\";

                var filePath = Path.Combine(uploadsFolder, excelFile.Name);

                //Creates a new file stream for writing the uploaded file.
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    //copies the uploaded file to the file stream
                    await excelFile.CopyToAsync(stream);
                }

                //read excel file
                List<OrderHeader> orderHeaders = new List<OrderHeader>();
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read)) //Opens the excel file for reading
                {
                    // Auto-detect format, supports:
                    //  - Binary Excel files (2.0-2003 format; *.xls)
                    //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                    using (var reader = ExcelReaderFactory.CreateReader(stream)) //Creates reader to read Excel file
                    {
                        do
                        {
                            bool isHeaderSkipped = false; //flag to skip the header row
                            while (reader.Read())
                            {
                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;
                                }
                                var test = reader.GetValue(0).ToString();
                                var test1 = reader.GetValue(1).ToString();
                                var test2 = reader.GetValue(2)?.ToString();
                                var test3 = reader.GetValue(3).ToString();
                                var test4 = reader.GetValue(4).ToString();
                                var test5 = reader.GetValue(5).ToString();
                                var test6 = reader.GetValue(6).ToString();
                                var test7 = reader.GetValue(7).ToString();
                                var test8 = reader.GetValue(8).ToString();
                                var test9 = double.Parse(reader.GetValue(9).ToString());
                                var test10 = reader.GetValue(10)?.ToString();

                                //Creates new OrderHeader object with values from the Excel row
                                OrderHeader order = new OrderHeader()
                                {
                                    Name = reader.GetValue(0).ToString() ?? "Error Customer Name!",
                                    Phone = reader.GetValue(1).ToString() ?? "Error Phone Number!",
                                    Email = reader.GetValue(2)?.ToString(), //can be nullable
                                    Address = reader.GetValue(3).ToString() ?? "Error Address",
                                    City = reader.GetValue(4).ToString() ?? "Error City Name",
                                    District = reader.GetValue(5).ToString() ?? "Error District Name",
                                    Ward = reader.GetValue(6).ToString() ?? "Error Ward Name",
                                    PaymentMethod = reader.GetValue(7).ToString() ?? "Error Payment Method",
                                    OrderStatus = reader.GetValue(8).ToString() ?? "Error Status",
                                    TotalPrice = double.Parse(reader.GetValue(9).ToString() ?? "Error Total Price"),
                                    CreatedBy = user,
                                    CreatedDate = DateTime.Now,
                                    /*                                    UpdatedBy = reader.GetValue(12)?.ToString(),
                                                                        UpdatedDate = DateTime.Parse(reader.GetValue(13)?.ToString()),*/
                                    /*CouponId = reader.GetValue(14)?.ToString()*/
                                };
                                orderHeaders.Add(order); //add each order to orderHeader list
                            }
                        } while (reader.NextResult());
                    }
                }
                //add list to db and save changes
                await _context.OrderHeaders.AddRangeAsync(orderHeaders);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<byte[]> ExportOrdersFilter(string[] statusesParam, string[] categoriesParam, string searchterm, int pageNumberParam, int pageSizeParam)
        {
            try
            {
                //Get List from db
                var result = await _context.OrderHeaders.ToListAsync();

                //Call filter function 
                result = Filter(statusesParam, categoriesParam, result);
                result = Search(result, searchterm);

                DataTable dt = new DataTable();
                dt.Columns.Add("Customer Name", typeof(string));
                dt.Columns.Add("Phone Number", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Address", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("District", typeof(string));
                dt.Columns.Add("Ward", typeof(string));
                dt.Columns.Add("Payment Method", typeof(string));
                dt.Columns.Add("Order Status", typeof(string));
                dt.Columns.Add("Total Price", typeof(double)); //double type
                dt.Columns.Add("Created By", typeof(string));
                dt.Columns.Add("Created Date", typeof(string));
                dt.Columns.Add("Updated By", typeof(string));
                dt.Columns.Add("Updated Date", typeof(string));
                /*                dt.Columns.Add("Coupon", typeof(string));*/

                foreach (var item in result)
                {
                    DataRow row = dt.NewRow();
                    row[0] = item.Name;
                    row[1] = item.Phone;
                    row[2] = item.Email;
                    row[3] = item.Address;
                    row[4] = item.City;
                    row[5] = item.District;
                    row[6] = item.Ward;
                    row[7] = item.PaymentMethod;
                    row[8] = item.OrderStatus;
                    row[9] = item.TotalPrice;
                    row[10] = await _userRepository.GetUserNameById(item.CreatedBy);
                    row[11] = item.CreatedDate;
                    row[12] = !string.IsNullOrEmpty(item.UpdatedBy) ? await _userRepository.GetUserNameById(item.UpdatedBy) : "";
                    row[13] = item.UpdatedDate;
                    /*                    row[14] = !string.IsNullOrEmpty(item.CouponId) ? item.CouponId : "";*/

                    dt.Rows.Add(row);
                }

                var memory = new MemoryStream();
                using (var excel = new ExcelPackage(memory))
                {
                    var worksheet = excel.Workbook.Worksheets.Add("Sheet1");

                    worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                    worksheet.Cells["A1:AN1"].Style.Font.Bold = true;
                    worksheet.DefaultRowHeight = 25;


                    return excel.GetAsByteArray();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
