﻿using Microsoft.AspNetCore.Mvc;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Models.DTO;
using PRN221_GroupProject.Pages.Email;

namespace PRN221_GroupProject.Repository
{
    public class EmailRepository : IEmailRepository
    {

        private readonly Prn221GroupProjectContext _dbContext;
        public ISenderEmail _emailSend;
        public EmailRepository(Prn221GroupProjectContext context, ISenderEmail senderEmail)
        {
            _dbContext = context;
            _emailSend = senderEmail;
        }

        public EmailListDTO GetList(string[] statusesParam, string[] categoriesParam,string searchterm, int pageNumberParam, int pageSizeParam)
        {
            //Get List from db
            var result = _dbContext.EmailTemplates.ToList();

            //Call filter function 
            result = Filter(statusesParam, categoriesParam, result);
            result = Search(result, searchterm);

            //Calculate pagination
            var totalItems = result.Count();
            var TotalPages = (int)Math.Ceiling((double)totalItems / pageSizeParam);

            //Get final result base on page size and page number 
            result = result.OrderByDescending(e => e.Id)
                    .Skip((pageNumberParam - 1) * pageSizeParam)
                    .Take(pageSizeParam)
                    .ToList();

            return new EmailListDTO()
            {
                listEmail = result,
                totalPages = TotalPages
            };
        }

        public async Task SendEmailByEmailTemplate(string templateId, string to, string? orderId, string? couponId) { 
            var template = _dbContext.EmailTemplates.SingleOrDefault(tp => tp.EmailTemplateId == templateId);
            if (template == null)
            {
                throw new Exception("Email Template Not Found");
            }
            else
            {
                var body = template.Body += "<br/> " +
                    "<table class=\"table\">\r\n  <thead>\r\n    <tr>\r\n      <th scope=\"col\">#</th>\r\n      <th scope=\"col\">First</th>\r\n      <th scope=\"col\">Last</th>\r\n      <th scope=\"col\">Handle</th>\r\n    </tr>\r\n  </thead>\r\n  <tbody>\r\n    <tr>\r\n      <th scope=\"row\">1</th>\r\n      <td>Mark</td>\r\n      <td>Otto</td>\r\n      <td>@mdo</td>\r\n    </tr>\r\n    <tr>\r\n      <th scope=\"row\">2</th>\r\n      <td>Jacob</td>\r\n      <td>Thornton</td>\r\n      <td>@fat</td>\r\n    </tr>\r\n    <tr>\r\n      <th scope=\"row\">3</th>\r\n      <td>Larry</td>\r\n      <td>the Bird</td>\r\n      <td>@twitter</td>\r\n    </tr>\r\n  </tbody>\r\n</table>";
                await _emailSend.SendEmailAsync("lamnguyen6556@gmail.com", template.Subject, body, true);
            }
        }

        private List<EmailTemplate> Filter(string[] statuses, string[] categories, List<EmailTemplate> list)
        {
            if (categories != null && categories.Length > 0)
            {
                list = list.Where(e => categories.Contains(e.Category)).ToList();
            }

            if (statuses != null && statuses.Length > 0)
            {
                list = list.Where(e => statuses.Contains(e.Active.ToString())).ToList();
            }

            return list;
        }

        private List<EmailTemplate> Search(List<EmailTemplate> list, string searchtearm)
        {
            if (!string.IsNullOrEmpty(searchtearm))
            {
                list = list.Where(p =>
                            p.Name.Contains(searchtearm, StringComparison.OrdinalIgnoreCase) ||
                            p.Description.Contains(searchtearm, StringComparison.OrdinalIgnoreCase) ||
                            p.CreatedBy.Contains(searchtearm, StringComparison.OrdinalIgnoreCase))
                            .ToList();
            }
            return list;
        }
    }
}