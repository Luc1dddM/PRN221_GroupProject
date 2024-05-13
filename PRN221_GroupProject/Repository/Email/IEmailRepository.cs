using PRN221_GroupProject.Models;
using PRN221_GroupProject.Models.DTO;

namespace PRN221_GroupProject.Repository
{
    public interface IEmailRepository
    {
        public EmailListDTO GetList(string[] statusesParam, string[] categoriesParam,string searchterm, int pageNumberParam, int pageSizeParam);
        public Task SendEmailByEmailTemplate(string templateId, string to, string? orderId, string? couponId);

    }
}
