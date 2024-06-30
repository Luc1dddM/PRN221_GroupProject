using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository
{
    public interface IEmailRepository
    {
        public EmailListDTO GetList(string[] statusesParam, string[] categoriesParam, string searchterm, string sortBy, string sortOrder, int pageNumberParam, int pageSizeParam);
        public List<EmailTemplate> GetList();
        public Task SendEmailByEmailTemplate(string templateId, string to);
        public void AddEmailTemplate(EmailTemplate newEmailTemplate);
        public Task<EmailTemplate> UpdateEmailTemplate(EmailTemplate newEmailTemplate);
        public Task<EmailTemplate> GetEmailTemplateById(string id);
        public Task SendEmailToAll(string emailTemplateId);
        public Task SendCouponToAll(string emailTemplateId, string coupon);
        public Task SendEmailCoupon(string templateId, string to, string couponCode);
        public Task SendEmailOrder(OrderHeader orderHeader);
        public Task ImportEmailTemplates(IFormFile excelFile, string user);
        public Task<Byte[]> ExportEmailFilter(string[] statusesParam, string[] categoriesParam, string searchterm, int pageNumberParam, int pageSizeParam);
    }
}
