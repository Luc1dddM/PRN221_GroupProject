using PRN221_GroupProject.Models;
using PRN221_GroupProject.Models.DTO;

namespace PRN221_GroupProject.Repository.Coupons
{
    public interface ICouponRepository
    {
        public CouponListDTO GetList(double? minAmount, double? maxAmount, string searchterm, int pageNumberParam, int pageSizeParam);
        Task<Coupon> GetCouponByIdAsync(int id);
        Task<Coupon> GetCouponByCodeAsync(string couponCode);

        Task DeleteCouponAsync(Coupon coupon);
    }
}
