using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using System.Linq;
using PRN221_GroupProject.DTO;
using System.Data;
using PRN221_GroupProject.Repository.Users;
using OfficeOpenXml;
using ExcelDataReader;
using DocumentFormat.OpenXml.Spreadsheet;

namespace PRN221_GroupProject.Repository.Coupons
{
    public class CouponRepository : ICouponRepository
    {
        private readonly Prn221GroupProjectContext _context;
        public IUserRepository _userRepo;

        public CouponRepository(Prn221GroupProjectContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepo = userRepository;
        }

        public CouponListDTO GetList(string[] statusesParam, double? minAmountParam, double? maxAmountParam, string searchterm, string sortBy, string sortOrder, int pageNumberParam, int pageSizeParam)
        {
            //Get List from db
            var result = _context.Coupons.ToList();


            //Call filter function 
            result = Filter(statusesParam, minAmountParam, maxAmountParam, result);
            result = Search(result, searchterm);
            result = Sort(sortBy, sortOrder, result);


            //Calculate pagination
            var totalItems = result.Count();
            var TotalPages = (int)Math.Ceiling((double)totalItems / pageSizeParam);

            //Get final result base on page size and page number 
            result = result
                    .Skip((pageNumberParam - 1) * pageSizeParam)
                    .Take(pageSizeParam)
                    .ToList();

            return new CouponListDTO()
            {
                listCoupon = result,
                totalPages = TotalPages
            };
        }

        public Coupon GetCouponById(string id)
        {
            return _context.Coupons.FirstOrDefault(c => c.CouponId == id);
        }

        public Coupon GetCouponByCode(string couponCode)
        {
            return _context.Coupons.FirstOrDefault(c => c.CouponCode == couponCode);
        }


        private List<Coupon> Search(List<Coupon> list, string searchterm)
        {
            if (!string.IsNullOrEmpty(searchterm))
            {
                list = list.Where(p =>
                            p.CouponCode.Contains(searchterm, StringComparison.OrdinalIgnoreCase) ||
                            p.DiscountAmount.ToString().Contains(searchterm, StringComparison.OrdinalIgnoreCase) ||
                            p.MinAmount.ToString().Contains(searchterm, StringComparison.OrdinalIgnoreCase) ||
                            p.MaxAmount.ToString().Contains(searchterm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return list;
        }

        private List<Coupon> Filter(string[] statuses, double? minAmount, double? maxAmount, List<Coupon> list)
        {
            if (minAmount.HasValue && minAmount.Value > 0)
            {
                list = list.Where(e => e.MinAmount.HasValue && e.MinAmount.Value >= minAmount.Value).ToList();
            }

            if (maxAmount.HasValue && maxAmount.Value > 0)
            {
                list = list.Where(e => e.MaxAmount.HasValue && e.MaxAmount.Value >= maxAmount.Value).ToList();
            }

            if (statuses != null && statuses.Length > 0)
            {
                list = list.Where(e => statuses.Contains(e.Status.ToString())).ToList();
            }
            return list;
        }

        private List<Coupon> Sort(string sortBy, string sortOrder, List<Coupon> list)
        {
            switch (sortBy)
            {
                case "couponCode":
                    list = sortOrder == "asc" ? list.OrderBy(e => e.CouponCode).ToList() : list.OrderByDescending(e => e.CouponCode).ToList();
                    break;
                case "discountAmount":
                    list = sortOrder == "asc" ? list.OrderBy(e => e.DiscountAmount).ToList() : list.OrderByDescending(e => e.DiscountAmount).ToList();
                    break;
                case "minAmount":
                    list = sortOrder == "asc" ? list.OrderBy(e => e.MinAmount).ToList() : list.OrderByDescending(e => e.MinAmount).ToList();
                    break;
                case "maxAmount":
                    list = sortOrder == "asc" ? list.OrderBy(e => e.MaxAmount).ToList() : list.OrderByDescending(e => e.MaxAmount).ToList();
                    break;
                case "status":
                    list = sortOrder == "asc" ? list.OrderBy(e => e.Status).ToList() : list.OrderByDescending(e => e.Status).ToList();
                    break;
                case "createdBy":
                    list = sortOrder == "asc" ? list.OrderBy(e => e.CreatedBy).ToList() : list.OrderByDescending(e => e.CreatedBy).ToList();
                    break;
                case "createdDate":
                    list = sortOrder == "asc" ? list.OrderBy(e => e.CreatedDate).ToList() : list.OrderByDescending(e => e.CreatedDate).ToList();
                    break;
                default:
                    list = list.OrderByDescending(e => e.Id).ToList();
                    break;
            }
            return list;
        }
        public List<Coupon> GetList()
        {

            return _context.Coupons.Where(e => e.Status).OrderByDescending(e => e.Id).ToList();
        }

        public async Task<byte[]> ExportCouponFilter(string[] statusesParam, double? minAmountParam, double? maxAmountParam, string searchterm, int pageNumberParam, int pageSizeParam)
        {
            try
            {
                //Get List from db
                var result = await _context.Coupons.ToListAsync();


                //Call filter function 
                result = Filter(statusesParam, minAmountParam, maxAmountParam, result);
                result = Search(result, searchterm);

                DataTable dt = new DataTable();
                dt.Columns.Add("Coupon Code", typeof(string));
                dt.Columns.Add("Discount Amount", typeof(double));
                dt.Columns.Add("Min Amount", typeof(double));
                dt.Columns.Add("Max Amount", typeof(double));
                dt.Columns.Add("Status", typeof(bool));
                dt.Columns.Add("Created By", typeof(string));
                dt.Columns.Add("Created Date", typeof(string));
                dt.Columns.Add("Updated By", typeof(string));
                dt.Columns.Add("Updated Date", typeof(string));

                foreach (var item in result)
                {
                    DataRow row = dt.NewRow();
                    row[0] = item.CouponCode;
                    row[1] = item.DiscountAmount;
                    row[2] = item.MinAmount.HasValue ? (object)item.MinAmount : DBNull.Value;
                    row[3] = item.MaxAmount.HasValue ? (object)item.MaxAmount : DBNull.Value;
                    row[4] = item.Status;
                    row[5] = await _userRepo.GetUserNameById(item.CreatedBy);
                    row[6] = item.CreatedDate;
                    row[7] = await _userRepo.GetUserNameById(item.UpdatedBy);
                    row[8] = item.UpdatedDate;
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ImportCoupons(IFormFile excelFile, string user)
        {
            try
            {
                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\uploads\\";
                var filePath = Path.Combine(uploadsFolder, excelFile.Name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await excelFile.CopyToAsync(stream);
                }


                List<Coupon> coupons = new List<Coupon>();
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    // Auto-detect format, supports:
                    //  - Binary Excel files (2.0-2003 format; *.xls)
                    //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            bool isHeaderSkipped = false;
                            while (reader.Read())
                            {
                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;
                                }
                                double? minAmount = null;
                                double? maxAmount = null;

                                if (!string.IsNullOrEmpty(reader.GetValue(2)?.ToString()))
                                {
                                    minAmount = double.Parse(reader.GetValue(2).ToString());
                                }

                                if (!string.IsNullOrEmpty(reader.GetValue(3)?.ToString()))
                                {
                                    maxAmount = double.Parse(reader.GetValue(3).ToString());
                                }

                                // Validate MinAmount < MaxAmount
                                if (minAmount.HasValue && maxAmount.HasValue && minAmount >= maxAmount)
                                {
                                    throw new Exception("MinAmount must be less than MaxAmount.");
                                }

                                Coupon s = new Coupon()
                                {
                                    CouponCode = reader.GetValue(0).ToString() ?? "Error CouponCode!",
                                    DiscountAmount = double.Parse(reader.GetValue(1).ToString() ?? "Error DiscountAmount!"),
                                    MinAmount = minAmount,
                                    MaxAmount = maxAmount,
                                    Status = bool.Parse(reader.GetValue(4).ToString() ?? "False"),
                                    CreatedBy = user,
                                    CreatedDate = DateTime.Now,
                                    UpdatedBy = user,
                                    UpdatedDate = DateTime.Now
                                };
                                coupons.Add(s);
                            }
                        } while (reader.NextResult());
                    }
                }
                await _context.Coupons.AddRangeAsync(coupons);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void Create(Coupon coupon, string user)
        {
            try
            {
                coupon.CreatedDate = DateTime.Now;
                coupon.CreatedBy = user;
                _context.Coupons.Add(coupon);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exception (logging, etc.)
                throw new Exception("Failed to create coupon", ex);
            }
        }

        public void Update(Coupon coupon, string user)
        {
            var couponToUpdate = _context.Coupons.Find(coupon.Id);

            if (couponToUpdate == null)
            {
                throw new ArgumentNullException("Coupon not found");
            }

            // Update fields
            couponToUpdate.CouponCode = coupon.CouponCode;
            couponToUpdate.DiscountAmount = coupon.DiscountAmount;
            couponToUpdate.MinAmount = coupon.MinAmount;
            couponToUpdate.MaxAmount = coupon.MaxAmount;
            couponToUpdate.Status = coupon.Status;
            couponToUpdate.UpdatedDate = DateTime.Now;
            couponToUpdate.UpdatedBy = user;

            try
            {
                _context.Entry(couponToUpdate).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
              
            }
        }
    }
}
