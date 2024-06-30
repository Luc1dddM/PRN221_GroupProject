using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Coupons
{
    public interface ICouponRepository
    {
        public CouponListDTO GetList(string[] statusesParam, double? minAmount, double? maxAmount, string searchterm, int pageNumberParam, int pageSizeParam);
        Task<Coupon> GetCouponByIdAsync(int id);
        Task<Coupon> GetCouponByCodeAsync(string couponCode);
        public Task<Byte[]> ExportCouponFilter(string[] statusesParam, double? minAmount, double? maxAmount, string searchterm, int pageNumberParam, int pageSizeParam);
        public Task ImportCoupons(IFormFile excelFile, string user);
    }
}
