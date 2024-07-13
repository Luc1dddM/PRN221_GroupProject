using ExcelDataReader;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Enums;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Products;
using PRN221_GroupProject.Repository.Users;
using Syncfusion.EJ2.Linq;
using System.Data;
using System.Globalization;

namespace PRN221_GroupProject.Repository.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Prn221GroupProjectContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        public OrderRepository(Prn221GroupProjectContext context,
            IUserRepository userRepository,
            IProductRepository productRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _productRepository = productRepository;
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


        public void AdminChangeOrderStatus(string orderHeaderId, string userId)
        {
            try
            {
                var orderHeaderToUpdate = GetOrderHeaderById(orderHeaderId);

                if (orderHeaderToUpdate.OrderStatus.Equals(OrderStatusEnum.Pending.ToString()))
                {
                    orderHeaderToUpdate.OrderStatus = OrderStatusEnum.Approved.ToString();
                }
                else if (orderHeaderToUpdate.OrderStatus.Equals(OrderStatusEnum.Approved.ToString()))
                {
                    orderHeaderToUpdate.OrderStatus = OrderStatusEnum.Processing.ToString();
                }
                else if (orderHeaderToUpdate.OrderStatus.Equals(OrderStatusEnum.Processing.ToString()))
                {
                    orderHeaderToUpdate.OrderStatus = OrderStatusEnum.Shipping.ToString();
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void CancelOrderStatus(string orderHeaderId, string userId)
        {
            try
            {
                var orderHeaderToUpdate = GetOrderHeaderById(orderHeaderId);

                if (orderHeaderToUpdate.OrderStatus.Equals(OrderStatusEnum.Pending.ToString()) ||
                    orderHeaderToUpdate.OrderStatus.Equals(OrderStatusEnum.Approved.ToString()) ||
                    orderHeaderToUpdate.OrderStatus.Equals(OrderStatusEnum.Processing.ToString()))
                {
                    orderHeaderToUpdate.OrderStatus = OrderStatusEnum.Cancelled.ToString();
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void CustomerChangeOrderStatus(string orderHeaderId, string userId)
        {
            try
            {
                var orderHeaderToUpdate = GetOrderHeaderById(orderHeaderId);

                if (orderHeaderToUpdate.OrderStatus.Equals(OrderStatusEnum.Shipping.ToString()))
                {
                    orderHeaderToUpdate.OrderStatus = OrderStatusEnum.Shipped.ToString();
                }
                else if (orderHeaderToUpdate.OrderStatus.Equals(OrderStatusEnum.Shipped.ToString()))
                {
                    orderHeaderToUpdate.OrderStatus = OrderStatusEnum.Refunded.ToString();
                }

                _context.SaveChanges();

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

        public OrderListDTO GetCustomerList(string[] statusesParam, string[] categoriesParam, string sortBy, string sortOrder, string searchterm, int pageNumberParam, int pageSizeParam, string userId)
        {
            var result = _context.OrderHeaders.Where(o => o.CreatedBy.Equals(userId)).Include(o => o.OrderDetails).ToList();

            result = Filter(statusesParam, categoriesParam, result);
            result = Search(result, searchterm);
            result = Sort(sortBy, sortOrder, result);

            var totalItems = result.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSizeParam);

            result = result.Skip((pageNumberParam - 1) * pageSizeParam)
                           .Take(pageSizeParam)
                           .ToList();

            return new OrderListDTO
            {
                listOrder = result,
                totalPages = totalPages
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

        public List<double> StatisticIncomeForYear()
        {
            try
            {
                List<double> list = new List<double>();
                for (var i = 1; i < 13; i++)
                {
                    var orders = _context.OrderHeaders.Where(c => c.UpdatedDate.HasValue && c.UpdatedDate.Value.Month == i && c.OrderStatus.Equals("Shipped"));
                    double income = 0;
                    foreach (var order in orders)
                    {
                        income += order.TotalPrice;
                    }
                    list.Add(income);
                }
                return list;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<double> StatisticImcomeForFourWeek()
        {
            try
            {
                List<double> list = new List<double>();
                var firstDayOfCurrentWeek = GetTheFirstDateOfWeek();
                for (var i = 0; i < 4; i++)
                {
                    var orders = _context.OrderHeaders.Where(
                        c => c.UpdatedDate.HasValue &&
                        c.UpdatedDate.Value.Date >= firstDayOfCurrentWeek.AddDays(-i * 7) &&
                        c.UpdatedDate.Value.Date <= firstDayOfCurrentWeek.AddDays((-i * 7) + 6)
                        && c.OrderStatus.Equals("Shipped"));
                    double income = 0;
                    foreach (var order in orders)
                    {
                        income += order.TotalPrice;
                    }
                    list.Add(income);
                }
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private DateTime GetTheFirstDateOfWeek()
        {
            try
            {
                DateTime d = DateTime.Now;
                var culture = CultureInfo.CurrentCulture;
                var diff = d.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
                if (diff < 0)
                    diff += 7;
                d = d.AddDays(-diff).Date;
                return d;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TopProductDTO> GetTop12Sales()
        {
            try
            {
                var productDTO = new List<TopProductDTO>();
                foreach (var item in _productRepository.GetAll())
                {
                    var count = _context.OrderDetails.Include(o => o.OrderHeader).Where(c => c.ProductId.Equals(item.ProductId) && c.OrderHeader.OrderStatus.Equals("Shipped")).Sum(o => o.Count);
                    var tmp = new TopProductDTO()
                    {
                        Product = item,
                        count = count
                    };
                    productDTO.Add(tmp);
                }

                    productDTO = productDTO.OrderByDescending(p => p.count).Take(12).ToList();
                

                return productDTO;
            }
            catch (Exception e) 
            { 
                throw new Exception(e.Message);
            }

        }

        public double StatisticImcomePerDay()
        {
            try
            {
                return _context.OrderHeaders.Where(o => o.UpdatedDate.HasValue && o.UpdatedDate.Value.Date == DateTime.Now.Date && o.OrderStatus.Equals("Shipped")).Sum(o => o.TotalPrice);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int StatisticProductSaledPerDay()
        {
            try
            {
                var dayOrder = _context.OrderHeaders.Include(o => o.OrderDetails).Where(o => o.UpdatedDate.HasValue && o.UpdatedDate.Value.Date == DateTime.Now.Date && o.OrderStatus.Equals("Shipped"));
                int productSaled = 0;
                foreach (var item in dayOrder)
                {
                    productSaled += item.OrderDetails.Sum(o => o.Count);
                }
                return productSaled;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
