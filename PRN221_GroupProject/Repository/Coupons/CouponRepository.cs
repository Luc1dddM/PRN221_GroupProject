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

        public CouponListDTO GetList(string[] statusesParam, double? minAmountParam, double? maxAmountParam, string searchterm, int pageNumberParam, int pageSizeParam)
        {
            //Get List from db
            var result = _context.Coupons.ToList();


            //Call filter function 
            result = Filter(statusesParam,minAmountParam, maxAmountParam, result);
            result = Search(result, searchterm);

            //Calculate pagination
            var totalItems = result.Count();
            var TotalPages = (int)Math.Ceiling((double)totalItems / pageSizeParam);

            //Get final result base on page size and page number 
            result = result.OrderByDescending(e => e.Id)
                    .Skip((pageNumberParam - 1) * pageSizeParam)
                    .Take(pageSizeParam)
                    .ToList();

            return new CouponListDTO()
            {
                listCoupon = result,
                totalPages = TotalPages
            };
        }

        public async Task<Coupon> GetCouponByIdAsync(int id)
        {
            return await _context.Coupons.FindAsync(id);
        }

        public async Task<Coupon> GetCouponByCodeAsync(string couponCode)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
            return coupon;
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
                dt.Columns.Add("Created By", typeof(string));
                dt.Columns.Add("Created Date", typeof(string));
                dt.Columns.Add("Updated By", typeof(string));
                dt.Columns.Add("Updated Date", typeof(string));

                foreach (var item in result)
                {
                    DataRow row = dt.NewRow();
                    row[0] = item.CouponCode;
                    row[1] = item.DiscountAmount;
                    row[2] = item.MinAmount;
                    row[3] = item.MaxAmount;
                    row[4] = await _userRepo.GetUserNameById(item.CreatedBy);
                    row[5] = item.CreatedDate;
                    row[6] = await _userRepo.GetUserNameById(item.UpdatedBy);
                    row[7] = item.UpdatedDate;
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
                                var test = reader.GetValue(4).ToString();
                                Coupon s = new Coupon()
                                {
                                    CouponCode = reader.GetValue(0).ToString() ?? "Error Name!",
                                    DiscountAmount = double.Parse(reader.GetValue(1).ToString() ?? "Error Description!"),
                                    MinAmount = double.Parse(reader.GetValue(2).ToString() ?? "Error Subject!"),
                                    MaxAmount = double.Parse(reader.GetValue(3).ToString() ?? "Error Body!"),
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
    }
}
