using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository
{
    public interface IEmailRepository
    {
        public EmailListDTO GetList(string[] statusesParam, string[] categoriesParam,string searchterm, int pageNumberParam, int pageSizeParam);
        public Task SendEmailByEmailTemplate(string templateId, string to, string? orderId, string? couponId);
        public void AddEmailTemplate(EmailTemplate newEmailTemplate);

        public Task<EmailTemplate> UpdateEmailTemplate(EmailTemplate newEmailTemplate);
        public Task<EmailTemplate> GetEmailTemplateById(string id);    
    }
}
