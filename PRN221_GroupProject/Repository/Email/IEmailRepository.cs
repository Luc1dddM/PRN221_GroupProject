using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository
{
    public interface IEmailRepository
    {
        public Task<EmailListDTO> GetList(string[] statusesParam, string[] categoriesParam, string searchterm, string sortBy, string sortOrder, int pageNumberParam, int pageSizeParam);
        public Task<List<EmailTemplate>> GetList();
        public Task SendEmailByEmailTemplate(EmailTemplate template, string to);
        public void AddEmailTemplate(EmailTemplate newEmailTemplate);
        public Task<EmailTemplate> UpdateEmailTemplate(EmailTemplate newEmailTemplate);
        public Task<EmailTemplate> GetEmailTemplateById(string id);
        public Task SendEmailToAll(EmailTemplate emailTemplate);
        public Task SendCouponToAll(EmailTemplate emailTemplate, string coupon);
        public Task SendEmailCoupon(EmailTemplate template, string to, string couponCode);
        public Task SendEmailOrder(OrderHeader orderHeader);
        public Task ImportEmailTemplates(IFormFile excelFile, string user);
        public Task<Byte[]> ExportEmailFilter(string[] statusesParam, string[] categoriesParam, string searchterm, int pageNumberParam, int pageSizeParam);

    }
}
