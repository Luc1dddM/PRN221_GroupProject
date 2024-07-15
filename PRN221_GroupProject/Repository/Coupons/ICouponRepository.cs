using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Coupons
{
    public interface ICouponRepository
    {
        public List<Coupon> GetList();
        public CouponListDTO GetList(string[] statusesParam, double? minAmount, double? maxAmount, string searchterm, string sortBy, string sortOrder, int pageNumberParam, int pageSizeParam);
        public void Create(Coupon coupon, string user);
        public void Update(Coupon coupon, string user);
        Coupon GetCouponById(string id);
        Coupon GetCouponByCode(string couponCode);
        public Task<Byte[]> ExportCouponFilter(string[] statusesParam, double? minAmount, double? maxAmount, string searchterm, int pageNumberParam, int pageSizeParam);
        public Task ImportCoupons(IFormFile excelFile, string user);
    }
}
