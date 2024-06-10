using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using System.Linq;
using PRN221_GroupProject.DTO;

namespace PRN221_GroupProject.Repository.Coupons
{
    public class CouponRepository : ICouponRepository
    {
        private readonly Prn221GroupProjectContext _context;

        public CouponRepository(Prn221GroupProjectContext context)
        {
            _context = context;
        }

        public CouponListDTO GetList(double? minAmountParam, double? maxAmountParam, string searchterm, int pageNumberParam, int pageSizeParam)
        {
            //Get List from db
            var result = _context.Coupons.ToList();


            //Call filter function 
            result = Filter(minAmountParam, maxAmountParam, result);
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

        public async Task DeleteCouponAsync(Coupon coupon)
        {
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
        }

        private List<Coupon> Search(List<Coupon> list, string searchterm)
        {
            if (!string.IsNullOrEmpty(searchterm))
            {
                list = list.Where(p =>
                            p.CouponCode.Contains(searchterm, StringComparison.OrdinalIgnoreCase) ||
                            p.DiscountAmount.ToString().Contains(searchterm, StringComparison.OrdinalIgnoreCase) ||
                            p.CreatedBy.Contains(searchterm, StringComparison.OrdinalIgnoreCase)||
                           p.MinAmount.ToString().Contains(searchterm, StringComparison.OrdinalIgnoreCase)||
                             p.MaxAmount.ToString().Contains(searchterm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return list;
        }

        private List<Coupon> Filter(double? minAmount, double? maxAmount, List<Coupon> list)
        {
            if (minAmount.HasValue && minAmount.Value > 0)
            {
                list = list.Where(e => e.MinAmount.HasValue && e.MinAmount.Value >= minAmount.Value).ToList();
            }

            if (maxAmount.HasValue && maxAmount.Value > 0)
            {
                list = list.Where(e => e.MaxAmount.HasValue && e.MaxAmount.Value <= maxAmount.Value).ToList();
            }

            return list;
        }
    }
}
