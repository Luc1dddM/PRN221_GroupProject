﻿using ExcelDataReader;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Pages.Email;
using PRN221_GroupProject.Repository.Products;
using PRN221_GroupProject.Repository.Users;
using System.Xml.Linq;

namespace PRN221_GroupProject.Repository
{
    public class EmailRepository : IEmailRepository
    {

        private readonly Prn221GroupProjectContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public IUserRepository _userRepo;
        public ISenderEmail _emailSend;
        public IProductRepository _productRepository;
        public EmailRepository(Prn221GroupProjectContext context, ISenderEmail senderEmail, IUserRepository userRepository, IProductRepository productRepository, UserManager<ApplicationUser> userManager)
        {
            _dbContext = context;
            _emailSend = senderEmail;
            _userRepo = userRepository;
            _productRepository = productRepository;
            _userManager = userManager;
        }

        public void AddEmailTemplate(EmailTemplate newEmailTemplate)
        {
            try
            {
                _dbContext.Add(newEmailTemplate);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<EmailTemplate> GetEmailTemplateById(string id)
        {
            try
            {
                return _dbContext.EmailTemplates.FirstOrDefaultAsync(e => e.EmailTemplateId.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public EmailListDTO GetList(string[] statusesParam, string[] categoriesParam, string searchterm, int pageNumberParam, int pageSizeParam)
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

        public List<EmailTemplate> GetList()
        {
            return _dbContext.EmailTemplates.Where(e => e.Active).OrderByDescending(e => e.Id).ToList();
        }

        public async Task SendEmailByEmailTemplate(string templateId, string to)
        {
            var template = await _dbContext.EmailTemplates.FirstAsync(tp => tp.EmailTemplateId == templateId);
            if (template == null)
            {
                throw new Exception("Email Template Not Found");
            }
            else
            {
                var body = template.Body;
                await _emailSend.SendEmailAsync(to, template.Subject, body, true);
            }
        }

        public async Task SendEmailCoupon(string templateId, string to, string couponCode)
        {
            var template = _dbContext.EmailTemplates.SingleOrDefault(tp => tp.EmailTemplateId == templateId);
            if (template == null)
            {
                throw new Exception("Email Template Not Found");
            }
            else
            {
                var body = template.Body;
                body += "<br> <p>Mã Giảm Giá: " + couponCode + "</p>";
                await _emailSend.SendEmailAsync(to, template.Subject, body, true);
            }
        }

        public async Task SendEmailOrder(OrderHeader orderHeader)
        {
            var template = await _dbContext.EmailTemplates.FirstOrDefaultAsync();
            if (template == null)
            {
                throw new Exception("Order Template Not Found");
            }

            var order = await _dbContext.OrderHeaders.Include(oh => oh.OrderDetails).FirstOrDefaultAsync(oh => oh.OrderHeaderId.Trim().Equals(orderHeader.OrderHeaderId));

            var body = "<table style=\"table-layout:fixed;vertical-align:top;min-width:320px;margin:0 auto;border-spacing:0;border-collapse:collapse;background-color:#ffffff;width:100%\" role=\"presentation\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#FFFFFF\">\r\n<tbody>\r\n<tr style=\"vertical-align:top\" valign=\"top\">\r\n<td style=\"word-break:break-word;vertical-align:top;border-collapse:collapse\" valign=\"top\">\r\n<div style=\"background-color:transparent\">\r\n<div style=\"margin:0 auto;min-width:320px;max-width:640px;word-wrap:break-word;word-break:break-word;background-color:#313130\">\r\n<div style=\"border-collapse:collapse;display:table;width:100%;background-color:#313130\">\r\n\r\n\r\n\r\n<div style=\"min-width:320px;max-width:640px;display:table-cell;vertical-align:top\">\r\n<div style=\"width:100%!important\">\r\n\r\n\r\n<div style=\"border:0px solid transparent;padding:5px 0px 5px 0px\">\r\n\r\n\r\n<div style=\"padding-right:10px;padding-left:10px\" align=\"center\">\r\n\r\n\r\n<div style=\"font-size:1px;line-height:10px\"></div>\r\n<img style=\"outline:none;text-decoration:none;clear:both;border:0;height:auto;float:none;width:100%;max-width:43px;display:block\" title=\"Image\" src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAHXPluq6GtTRPDIHRv5kJPy86uFjp5sO7hg&s\" alt=\"Image\" width=\"43\" align=\"center\" border=\"0\" class=\"CToWUd\" data-bit=\"iit\">\r\n<div style=\"font-size:1px;line-height:10px\"></div>\r\n\r\n\r\n</div>\r\n\r\n\r\n</div>\r\n\r\n\r\n</div>\r\n</div>\r\n\r\n\r\n\r\n</div>\r\n</div>\r\n</div>\r\n<div style=\"background-color:transparent\">\r\n<div style=\"margin:0 auto;min-width:320px;max-width:640px;word-wrap:break-word;word-break:break-word;background-color:#cccccb\">\r\n<div style=\"border-collapse:collapse;display:table;width:100%;background-color:#cccccb\">\r\n\r\n\r\n\r\n<div style=\"min-width:320px;max-width:640px;display:table-cell;vertical-align:top\">\r\n<div style=\"width:100%!important\">\r\n\r\n\r\n<div style=\"border:0px solid transparent;padding:5px 0px 5px 0px\">\r\n\r\n<div style=\"color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;line-height:120%;padding:5px\">\r\n<div style=\"font-size:12px;line-height:14px;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;color:#555555\">\r\n<p style=\"font-size:14px;line-height:13px;text-align:center;margin:0\"><span style=\"font-size:11px\"><em><span style=\"line-height:13px;font-size:11px\">Cuộc sống\r\nvốn có rất nhiều lựa chọn, cảm ơn vì mày đã chọn Cick&Clack.</span></em>\r\n</span></p>\r\n\r\n</div>\r\n</div>\r\n\r\n\r\n\r\n</div>\r\n\r\n\r\n</div>\r\n</div>\r\n\r\n\r\n\r\n</div>\r\n</div>\r\n</div>";

            //Greeting User
            body += $"<div style=\"background-color:transparent\">\r\n<div style=\"margin:0 auto;min-width:320px;max-width:640px;word-wrap:break-word;word-break:break-word;background-color:#fff\">\r\n<div style=\"border-collapse:collapse;display:table;width:100%;background-color:#fff\">\r\n\r\n\r\n\r\n<div style=\"min-width:320px;max-width:640px;display:table-cell;vertical-align:top\">\r\n<div style=\"width:100%!important\">\r\n\r\n\r\n<div style=\"border:0px solid transparent;padding:5px 0px 5px 0px\">\r\n\r\n<div style=\"color:#000;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;line-height:150%;padding:30px 50px 10px 50px\">\r\n<div style=\"font-size:12px;line-height:18px;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;color:#000\">\r\n<p style=\"font-size:12px;line-height:16px;margin:0\"><span style=\"font-size:11px\">Xin\r\nchào <strong>{(await _userRepo.GetUserNameById(order?.UserId))}</strong> , </span></p>\r\n<p style=\"font-size:12px;line-height:16px;margin:0\"><span style=\"font-size:11px\">Click&Clack xin thông báo đã nhận được đơn đặt hàng mang mã số <span style=\"color:#f15f2e;line-height:16px;font-size:11px\"><strong><a style=\"text-decoration:underline;color:#f15f2e\" rel=\"noopener noreferrer\">{order.OrderHeaderId}</a></strong></span> của bạn. </span></p>\r\n<p style=\"font-size:12px;line-height:16px;margin:0\"><span style=\"font-size:11px\">Đơn\r\nhàng của mày bạn được tiếp nhận và trong quá trình xử lí. Dưới\r\nđây là thông tin đơn hàng, bạn cũng có thể theo dõi trạng thái đơn hàng bất cứ lúc\r\nnào bạn muốn.</span></p>\r\n\r\n</div>\r\n</div>\r\n\r\n<div style=\"padding:0px 10px 30px 10px\" align=\"center\">\r\n\r\n\r\n\r\n\r\n</div>\r\n\r\n\r\n</div>\r\n\r\n\r\n</div>\r\n</div>\r\n\r\n\r\n\r\n</div>\r\n</div>\r\n</div>";

            //Table Head of Order Detail
            body += "<div style=\"background-color:transparent\">\r\n    <div style=\"Margin:0 auto;width:640px;word-wrap:break-word;word-break:break-word;background-color:#fff\">\r\n            <div style=\"border-collapse:collapse;display:table;width:100%;background-color:#fff\">\r\n                \r\n                \r\n\r\n\r\n                <div style=\"width:640px;display:table-cell;vertical-align:top\">\r\n                    <div style=\"width:100%!important\">\r\n                        \r\n                        <div style=\"border-top:0px solid transparent;border-left:0px solid transparent;border-bottom:0px solid transparent;border-right:0px solid transparent;padding-top:5px;padding-bottom:5px;padding-right:50px;padding-left:50px\">\r\n                            \r\n                            \r\n                            <div style=\"color:#231f20;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;line-height:120%;padding-top:0px;padding-right:50px;padding-bottom:0px;padding-left:50px\">\r\n                                <div style=\"font-size:12px;line-height:14px;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;color:#231f20\">\r\n                                    <p style=\"font-size:14px;line-height:16px;margin:0\"><span style=\"color:#000000;font-size:14px;line-height:16px\"><strong>CHI TIẾT ĐƠN\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tHÀNG</strong></span></p>\r\n                                </div>\r\n                            </div>\r\n                                                        \r\n                            \r\n                            <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"table-layout:fixed;vertical-align:top;border-spacing:0;border-collapse:collapse;min-width:100%\" valign=\"top\" width=\"100%\">\r\n                                <tbody>\r\n                                <tr style=\"vertical-align:top\" valign=\"top\">\r\n                                    <td style=\"word-break:break-word;vertical-align:top;min-width:100%;padding-top:10px;padding-right:50px;padding-bottom:10px;padding-left:50px;border-collapse:collapse\" valign=\"top\">\r\n                                        <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"0\" role=\"presentation\" style=\"table-layout:fixed;vertical-align:top;border-spacing:0;border-collapse:collapse;width:100%;border-top:2px solid #000;height:0px\" valign=\"top\" width=\"100%\">\r\n                                            <tbody>\r\n                                            <tr style=\"vertical-align:top\" valign=\"top\">\r\n                                                <td height=\"0\" style=\"word-break:break-word;vertical-align:top;border-collapse:collapse\" valign=\"top\"><span></span></td>\r\n                                            </tr>\r\n                                            </tbody>\r\n                                        </table>\r\n                                    </td>\r\n                                </tr>\r\n                                </tbody>\r\n                            </table>";

            Product product;
            var format = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            //Order Detail
            foreach (var detail in order.OrderDetails)
            {
                product = _productRepository.GetProductByID(detail.ProductId);
                body += $"  <div style=\"font-size:16px;text-align:center;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif\">\r\n                                    <div style=\"padding:10px 50px\">\r\n                                        <table style=\"width:100%;height:90px;font-size:10px;color:#939598;border-spacing:0px;border-collapse:collapse\">\r\n                                            <tbody><tr>\r\n                                                <td style=\"width:100px;text-align:left\">\r\n\r\n</td>\r\n<td style=\"text-align:left\">\r\n<table style=\"width:100%;height:100%;border-spacing:0px;border-collapse:collapse\">\r\n                                                        <tbody><tr>\r\n                                                            <td style=\"font-size:14px;font-weight:bold;vertical-align:top\">\r\n                                                                {product.Name}<br>                                                            </td>\r\n                                                        </tr>\r\n                                                                                                                  <tr>\r\n                                                            <td style=\"height:10px\"><b>Số\r\n                                                                    lượng:</b>&nbsp;&nbsp;{detail.Count}</td>\r\n                                                        </tr>\r\n                                                        <tr>\r\n                                                                                                                            <td style=\"height:10px\">\r\n                                                                    <b>Giá:</b> {String.Format(format, "{0:c0}", detail.Price)} \r\n                                                                </td>\r\n                                                                                                                    </tr>\r\n                                                    </tbody></table>\r\n                                                </td>\r\n                                                <td style=\"width:100px;text-align:right;vertical-align:top;font-size:11px;font-weight:bold;padding-top:4px\">\r\n                                                    {String.Format(format, "{0:c0}", detail.Price)}\r\n                                                </td>\r\n                                            </tr>\r\n                                        </tbody></table>\r\n                                    </div>\r\n                                </div>";
            }

            //Table Tail
            body += "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"table-layout:fixed;vertical-align:top;border-spacing:0;border-collapse:collapse;min-width:100%\" valign=\"top\" width=\"100%\">\r\n                                <tbody>\r\n                                <tr style=\"vertical-align:top\" valign=\"top\">\r\n                                    <td style=\"word-break:break-word;vertical-align:top;min-width:100%;padding-top:10px;padding-right:50px;padding-bottom:10px;padding-left:50px;border-collapse:collapse\" valign=\"top\">\r\n                                        <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"0\" role=\"presentation\" style=\"table-layout:fixed;vertical-align:top;border-spacing:0;border-collapse:collapse;width:100%;border-top:2px solid #000;height:0px\" valign=\"top\" width=\"100%\">\r\n                                            <tbody>\r\n                                            <tr style=\"vertical-align:top\" valign=\"top\">\r\n                                                <td height=\"0\" style=\"word-break:break-word;vertical-align:top;border-collapse:collapse\" valign=\"top\"><span></span></td>\r\n                                            </tr>\r\n                                            </tbody>\r\n                                        </table>\r\n                                    </td>\r\n                                </tr>\r\n                                </tbody>\r\n                            </table>\r\n                            \r\n\r\n                            \r\n                        </div>\r\n                        \r\n                    </div>\r\n                </div>\r\n                \r\n                \r\n            </div>\r\n        </div>\r\n    </div>";

            //Order Total 
            body += $"<div style=\"background-color:transparent\">\r\n        <div style=\"Margin:0 auto;width:640px;word-wrap:break-word;word-break:break-word;background-color:#fff\">\r\n            <div style=\"border-collapse:collapse;display:table;width:100%;background-color:#fff\">\r\n                \r\n                \r\n                <div style=\"min-width:320px;max-width:320px;display:table-cell;vertical-align:top\">\r\n                    <div style=\"width:100%!important\">\r\n                        \r\n                        <div style=\"border-top:0px solid transparent;border-left:0px solid transparent;border-bottom:0px solid transparent;border-right:0px solid transparent;padding-top:0px;padding-bottom:30px;padding-right:0px;padding-left:50px\">\r\n                            \r\n                            \r\n\r\n                            <div style=\"color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;line-height:150%;padding-top:0px;padding-right:0px;padding-bottom:0px;padding-left:50px\">\r\n                                <div style=\"font-size:12px;line-height:18px;color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif\">\r\n                                    <p style=\"font-size:14px;line-height:16px;margin:0\"><span style=\"font-size:11px\"><strong><span style=\"line-height:16px;font-size:11px\">Tổng\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tgiá trị đơn hàng: </span></strong></span></p>\r\n                                    <p style=\"font-size:14px;line-height:16px;margin:0\"><span style=\"font-size:11px\"><strong><span style=\"line-height:16px;font-size:11px\">Khuyến\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tmãi: </span></strong></span></p>\r\n                                    <p style=\"font-size:14px;line-height:16px;margin:0\"><span style=\"font-size:11px\"><strong><span style=\"line-height:16px;font-size:11px\">Phí vận\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tchuyển: </span></strong></span></p>\r\n                                    <p style=\"font-size:14px;line-height:16px;margin:0\"><span style=\"font-size:11px\"><strong><span style=\"line-height:16px;font-size:11px\">Phí thanh toán: </span></strong></span></p>\r\n                                    <p style=\"font-size:14px;line-height:21px;margin:0\"><span style=\"color:#f15f2e;font-size:14px;line-height:21px\"><strong>Tổng thanh\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ttoán:</strong></span></p>\r\n                                </div>\r\n                            </div>\r\n                            \r\n                            \r\n                        </div>\r\n                        \r\n                    </div>\r\n                </div>\r\n                \r\n                \r\n                <div style=\"min-width:320px;max-width:320px;display:table-cell;vertical-align:top\">\r\n                    <div style=\"width:100%!important\">\r\n                        \r\n                        <div style=\"border-top:0px solid transparent;border-left:0px solid transparent;border-bottom:0px solid transparent;border-right:0px solid transparent;padding-top:0px;padding-bottom:0px;padding-right:50px;padding-left:0px\">\r\n                            \r\n                            \r\n                                                        <div style=\"color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;line-height:150%;padding-top:0px;padding-right:50px;padding-bottom:0px;padding-left:0px\">\r\n                                <div style=\"font-size:12px;line-height:18px;color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif\">\r\n                                    <p style=\"font-size:14px;line-height:16px;text-align:right;margin:0\"><span style=\"font-size:11px\"><strong><span style=\"line-height:16px;font-size:11px\">{String.Format(format, "{0:c0}", order.TotalPrice)}</span></strong></span>\r\n                                    </p>\r\n                                    <p style=\"font-size:14px;line-height:16px;text-align:right;margin:0\"><span style=\"font-size:11px\"><strong><span style=\"line-height:16px;font-size:11px\"> 0\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t</span></strong></span></p>\r\n                                    <p style=\"font-size:14px;line-height:16px;text-align:right;margin:0\"><span style=\"font-size:11px\"><strong><span style=\"line-height:16px;font-size:11px\">0\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t</span></strong></span></p>\r\n                                    <p style=\"font-size:14px;line-height:16px;text-align:right;margin:0\"><span style=\"font-size:11px\"><strong><span style=\"line-height:16px;font-size:11px\"> 0                                                   </span></strong></span></p>\r\n                                    <p style=\"font-size:14px;line-height:21px;text-align:right;margin:0\"><span style=\"color:#f15f2e;font-size:14px;line-height:21px\"><strong> {@String.Format(format, "{0:c0}", order.TotalPrice)}\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t</strong></span></p>\r\n                                </div>\r\n                            </div>\r\n                            \r\n                            \r\n                        </div>\r\n                        \r\n                    </div>\r\n                </div>\r\n                \r\n                \r\n            </div>\r\n        </div>\r\n    </div>";

            //Shipping Infor
            body += $" <div style=\"background-color:transparent\">\r\n        <div style=\"Margin:0 auto;width:640px;word-wrap:break-word;word-break:break-word;background-color:#fff\">\r\n            <div style=\"border-collapse:collapse;display:table;width:100%;background-color:#fff\">\r\n                \r\n                \r\n                <div style=\"width:640px;display:table-cell;vertical-align:top\">\r\n                    <div style=\"width:100%!important\">\r\n                        \r\n                        <div style=\"border-top:0px solid transparent;border-left:0px solid transparent;border-bottom:0px solid transparent;border-right:0px solid transparent;padding-top:0px;padding-bottom:0px;padding-right:50px;padding-left:50px\">\r\n                            \r\n                            \r\n                            <div style=\"color:#231f20;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;line-height:120%;padding-top:0px;padding-right:50px;padding-bottom:0px;padding-left:50px\">\r\n                                <div style=\"font-size:12px;line-height:14px;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;color:#231f20\">\r\n                                    <p style=\"font-size:14px;line-height:16px;margin:0\"><strong>THÔNG TIN GIAO\r\n                                            NHẬN</strong>\r\n                                    </p>\r\n                                </div>\r\n                            </div>\r\n                            \r\n                            <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"table-layout:fixed;vertical-align:top;border-spacing:0;border-collapse:collapse;min-width:100%\" valign=\"top\" width=\"100%\">\r\n                                <tbody>\r\n                                <tr style=\"vertical-align:top\" valign=\"top\">\r\n                                    <td style=\"word-break:break-word;vertical-align:top;min-width:100%;padding-top:10px;padding-right:50px;padding-bottom:10px;padding-left:50px;border-collapse:collapse\" valign=\"top\">\r\n                                        <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"0\" role=\"presentation\" style=\"table-layout:fixed;vertical-align:top;border-spacing:0;border-collapse:collapse;width:100%;border-top:2px solid #000;height:0px\" valign=\"top\" width=\"100%\">\r\n                                            <tbody>\r\n                                            <tr style=\"vertical-align:top\" valign=\"top\">\r\n                                                <td height=\"0\" style=\"word-break:break-word;vertical-align:top;border-collapse:collapse\" valign=\"top\"><span></span></td>\r\n                                            </tr>\r\n                                            </tbody>\r\n                                        </table>\r\n                                    </td>\r\n                                </tr>\r\n                                </tbody>\r\n                            </table>\r\n                            \r\n                        </div>\r\n                        \r\n                    </div>\r\n                </div>\r\n                \r\n                \r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div style=\"background-color:transparent\">\r\n        <div style=\"Margin:0 auto;width:640px;word-wrap:break-word;word-break:break-word;background-color:#fff\">\r\n            <div style=\"border-collapse:collapse;display:table;width:100%;background-color:#fff\">\r\n                \r\n                \r\n                <div style=\"min-width:320px;max-width:320px;display:table-cell;vertical-align:top\">\r\n                    <div style=\"width:100%!important\">\r\n                        \r\n                        <div style=\"border-top:0px solid transparent;border-left:0px solid transparent;border-bottom:0px solid transparent;border-right:0px solid transparent;padding-top:0px;padding-bottom:40px;padding-right:0px;padding-left:50px\">\r\n                            \r\n                            \r\n                            <div style=\"color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;line-height:120%;padding-top:0px;padding-right:0px;padding-bottom:0px;padding-left:50px\">\r\n                                <div style=\"font-size:12px;line-height:14px;color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif\">\r\n                                    <p style=\"font-size:14px;line-height:13px;margin:0\"><span style=\"font-size:11px\">Họ\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ttên: {order.Name}</span></p>\r\n                                    <p style=\"font-size:14px;line-height:13px;margin:0\"><span style=\"font-size:11px\"> Điện\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthoại: {order.Phone} </span></p>\r\n                                    <p style=\"font-size:14px;line-height:13px;margin:0\"><span style=\"font-size:11px\">Email: <a href=\"mailto:buingocminhtam2k3@gmail.com\" target=\"_blank\">{order.Email}</a> </span></p>\r\n                                    <p style=\"font-size:14px;line-height:13px;margin:0\"><span style=\"font-size:11px\">Địa\r\n                                            chỉ: {order.Address} </span></p>\r\n                                    <p style=\"font-size:14px;line-height:13px;margin:0\"><span style=\"font-size:11px\">Phường/xã: {order.Ward}</span></p>\r\n                                    <p style=\"font-size:14px;line-height:13px;margin:0\"><span style=\"font-size:11px\">Quận/Huyện: {order.District}</span></p>\r\n                                    <p style=\"font-size:14px;line-height:13px;margin:0\"><span style=\"font-size:11px\">Thành\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tphố/Tỉnh: {order.City}</span></p>\r\n                                </div>\r\n                            </div>\r\n                            \r\n                            \r\n                        </div>\r\n                        \r\n                    </div>\r\n                </div>\r\n                \r\n                \r\n                <div style=\"min-width:320px;max-width:320px;display:table-cell;vertical-align:top\">\r\n                    <div style=\"width:100%!important\">\r\n                        \r\n                        <div style=\"border-top:0px solid transparent;border-left:0px solid transparent;border-bottom:0px solid transparent;border-right:0px solid transparent;padding-top:0px;padding-bottom:0px;padding-right:50px;padding-left:0px\">\r\n                            \r\n                            \r\n                            <div style=\"color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;line-height:120%;padding-top:0px;padding-right:50px;padding-bottom:0px;padding-left:0px\">\r\n                                <div style=\"font-size:12px;line-height:14px;color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif\">\r\n                                    <p style=\"font-size:14px;line-height:13px;margin:0\"><span style=\"font-size:11px\"><strong><span style=\"line-height:13px;font-size:11px\">Phương\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthức thanh toán:</span></strong></span></p>\r\n                                    <p style=\"font-size:14px;line-height:13px;margin:0\"><span style=\"font-size:11px\">Thanh toán bằng tiền mặt khi nhận hàng (COD)</span></p>\r\n                                    <p style=\"font-size:14px;line-height:13px;margin:0\"><span style=\"font-size:11px\">&nbsp;</span></p>\r\n                                </div>\r\n                            </div>\r\n                            \r\n                            \r\n                        </div>\r\n                        \r\n                    </div>\r\n                </div>\r\n                \r\n                \r\n            </div>\r\n        </div>\r\n    </div>";

            //Footer
            body += "\r\n<div style=\"background-color:transparent\">\r\n<div style=\"margin:0 auto;min-width:320px;max-width:640px;word-wrap:break-word;word-break:break-word;background-color:#fff\">\r\n<div style=\"border-collapse:collapse;display:table;width:100%;background-color:#fff\">\r\n\r\n\r\n\r\n<div style=\"min-width:320px;max-width:640px;display:table-cell;vertical-align:top\">\r\n<div style=\"width:100%!important\">\r\n\r\n\r\n\r\n<table style=\"table-layout:fixed;vertical-align:top;border-spacing:0;border-collapse:collapse;min-width:100%\" role=\"presentation\" border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\r\n<tbody>\r\n<tr style=\"vertical-align:top\" valign=\"top\">\r\n<td style=\"word-break:break-word;vertical-align:top;min-width:100%;border-collapse:collapse;padding:10px 50px 10px 50px\" valign=\"top\">\r\n<table style=\"table-layout:fixed;vertical-align:top;border-spacing:0;border-collapse:collapse;width:100%;border-top:1px dashed #cccccb;height:0px\" role=\"presentation\" border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\">\r\n<tbody>\r\n<tr style=\"vertical-align:top\" valign=\"top\">\r\n<td style=\"word-break:break-word;vertical-align:top;border-collapse:collapse\" valign=\"top\" height=\"0\"></td>\r\n</tr>\r\n</tbody>\r\n</table>\r\n</td>\r\n</tr>\r\n</tbody>\r\n</table>\r\n\r\n<div style=\"color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;line-height:120%;padding:0px 50px 20px 50px\">\r\n<div style=\"font-size:12px;line-height:14px;color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif\">\r\n<p style=\"font-size:14px;line-height:12px;margin:0\"><span style=\"color:#939598;font-size:10px\"><em><span style=\"line-height:12px;font-size:10px\">Đây là email được gửi tự động, vui lòng\r\nkhông phản hồi email này. Để tìm hiểu thêm các quy định về đơn hàng hay các chính sách\r\nsau bán hàng của Click&Clack, vui lòng truy cập <span style=\"color:#f15f2e;line-height:12px;font-size:10px\"><strong><a style=\"text-decoration:none;color:#f15f2e\" href=\"https://ananas.vn/faqs\" rel=\"noopener noreferrer\" target=\"_blank\" data-saferedirecturl=\"https://www.google.com/url?q=https://ananas.vn/faqs&amp;source=gmail&amp;ust=1718110141220000&amp;usg=AOvVaw2XzuS4endcAcO0J1piRWg2\">tại\r\nlink</a></strong></span> hoặc gọi đến<strong> 0914468405</strong> (trong giờ\r\nhành chính) để được hướng dẫn.</span>\r\n</em>\r\n</span></p>\r\n\r\n</div>\r\n</div>\r\n\r\n\r\n\r\n</div>\r\n\r\n\r\n</div>\r\n</div>\r\n\r\n\r\n\r\n</div>\r\n</div>\r\n</div>\r\n<div style=\"background-color:transparent\">\r\n<div style=\"margin:0 auto;min-width:320px;max-width:640px;word-wrap:break-word;word-break:break-word;background-color:#4d4d4d\">\r\n<div style=\"border-collapse:collapse;display:table;width:100%;background-color:#4d4d4d\">\r\n\r\n\r\n\r\n<div style=\"min-width:320px;max-width:320px;display:table-cell;vertical-align:top\">\r\n<div style=\"width:100%!important\">\r\n\r\n\r\n<div style=\"border:0px solid transparent;padding:30px 0px 30px 50px\">\r\n\r\n\r\n<div style=\"padding-right:0px;padding-left:0px\" align=\"left\">\r\n\r\n\r\n</div>\r\n\r\n<div style=\"color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;line-height:120%;padding:20px 0px 0px 0px\">\r\n<div style=\"font-size:12px;line-height:14px;color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif\">\r\n<p style=\"font-size:14px;line-height:13px;margin:0\"><span style=\"font-size:11px\"><strong><span style=\"color:#939598;line-height:13px;font-size:11px\">Click&Clack Online Team\r\n</span></strong>\r\n</span></p>\r\n<p style=\"font-size:14px;line-height:13px;margin:0\"><span style=\"color:#939598;font-size:11px\"><strong>Phone:</strong> 0914468405 </span></p>\r\n<p style=\"font-size:14px;line-height:16px;margin:0\"><span style=\"color:#939598;line-height:16px;font-size:14px\"><span style=\"font-size:11px;line-height:13px\"><strong>Add:</strong>95/27, hẻm 95, đường Đ. Mậu Thân, An Phú, Ninh Kiều, Cần Thơ, Việt Nam</span> </span></p>\r\n\r\n</div>\r\n</div>\r\n\r\n\r\n\r\n</div>\r\n\r\n\r\n</div>\r\n</div>\r\n\r\n\r\n<div style=\"min-width:320px;max-width:320px;display:table-cell;vertical-align:top\">\r\n<div style=\"width:100%!important\">\r\n\r\n\r\n<div style=\"border:0px solid transparent;padding:0px 50px 0px 0px\">\r\n\r\n\r\n<table style=\"table-layout:fixed;vertical-align:top;border-spacing:0;border-collapse:collapse\" role=\"presentation\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\r\n<tbody>\r\n<tr style=\"vertical-align:top\" valign=\"top\">\r\n<td style=\"word-break:break-word;vertical-align:top;border-collapse:collapse;padding:35px 0px 0px 0px\" valign=\"top\">\r\n<table style=\"table-layout:fixed;vertical-align:top;border-spacing:0;border-collapse:undefined\" role=\"presentation\" cellspacing=\"0\" cellpadding=\"0\" align=\"left\">\r\n<tbody>\r\n<tr style=\"vertical-align:top;display:inline-block;text-align:left\" align=\"left\" valign=\"top\">\r\n<td style=\"word-break:break-word;vertical-align:top;padding-bottom:5px;padding-right:20px;padding-left:0px;border-collapse:collapse\" valign=\"top\"><a href=\"https://www.facebook.com/Ananas.vietnam/\" rel=\"noopener noreferrer\" target=\"_blank\" data-saferedirecturl=\"https://www.google.com/url?q=https://www.facebook.com/Ananas.vietnam/&amp;source=gmail&amp;ust=1718110141220000&amp;usg=AOvVaw2jWX1FZZ7LST3oNJa22ySH\"><img style=\"outline:none;text-decoration:none;clear:both;height:auto;float:none;border:none;display:block\" title=\"Facebook\" src=\"https://ci3.googleusercontent.com/meips/ADKq_NZe4JWXi6DmuLxqNoFQIK8uLex9IV2FAwTgx6bWaikKDKMsd1R2Dw-rH6P-vQG8nCtOPOR86d6OIYpLKx2SzyF6lZdvTD5zcgs59xrmFw=s0-d-e1-ft#https://ananas.vn/wp-content/uploads/icon_facebook-1.png\" alt=\"Facebook\" width=\"26\" height=\"25\" class=\"CToWUd\" data-bit=\"iit\"></a></td>\r\n<td style=\"word-break:break-word;vertical-align:top;padding-bottom:5px;padding-right:20px;padding-left:0px;border-collapse:collapse\" valign=\"top\"><a href=\"https://www.instagram.com/ananasvn/\" rel=\"noopener noreferrer\" target=\"_blank\" data-saferedirecturl=\"https://www.google.com/url?q=https://www.instagram.com/ananasvn/&amp;source=gmail&amp;ust=1718110141220000&amp;usg=AOvVaw3wwQdce83phIu_9EeyZ9HK\"><img style=\"outline:none;text-decoration:none;clear:both;height:auto;float:none;border:none;display:block\" title=\"Instagram\" src=\"https://ci3.googleusercontent.com/meips/ADKq_NYhfHeHeVL404fHhznJ-K8p7l22cQvbJnSpaCxqEGJZFrEM3BbXuGt0qUKp-HocK9ZzkakHTLMqQ6Znmw6hVyodmeXShOuCXDrb9Oxn=s0-d-e1-ft#https://ananas.vn/wp-content/uploads/icon_instagram.png\" alt=\"Instagram\" width=\"26\" height=\"25\" class=\"CToWUd\" data-bit=\"iit\"></a></td>\r\n<td style=\"word-break:break-word;vertical-align:top;padding-bottom:5px;padding-right:20px;padding-left:0px;border-collapse:collapse\" valign=\"top\"><a href=\"https://www.youtube.com/discoveryou\" rel=\"noopener noreferrer\" target=\"_blank\" data-saferedirecturl=\"https://www.google.com/url?q=https://www.youtube.com/discoveryou&amp;source=gmail&amp;ust=1718110141220000&amp;usg=AOvVaw3OzYlIUH6DALXofaLvh2bq\"><img style=\"outline:none;text-decoration:none;clear:both;height:auto;float:none;border:none;display:block\" title=\"YouTube\" src=\"https://ci3.googleusercontent.com/meips/ADKq_NZsyYGgtFg8RbBr7zukwlwHGRS1HQSldC3PeWNgTfNxrzThzKnrJs1lkc0flhCZhMO7IKEbVYB5qY8nkIYD8UQyn07EdmbBda40PA=s0-d-e1-ft#https://ananas.vn/wp-content/uploads/icon_youtube.png\" alt=\"YouTube\" width=\"26\" height=\"25\" class=\"CToWUd\" data-bit=\"iit\"></a></td>\r\n</tr>\r\n</tbody>\r\n</table>\r\n</td>\r\n</tr>\r\n</tbody>\r\n</table>\r\n<div style=\"padding:20px 0px 0px 0px\" align=\"left\">\r\n\r\n\r\n\r\n</div>\r\n\r\n<div style=\"color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif;line-height:120%;padding:28px 0px 0px 0px\">\r\n<div style=\"font-size:12px;line-height:14px;color:#555555;font-family:Arial,'Helvetica Neue',Helvetica,sans-serif\">\r\n<p style=\"font-size:14px;line-height:13px;margin:0\"><span style=\"font-size:11px\"><em><span style=\"color:#808080;line-height:13px;font-size:11px\"><span style=\"color:#939598;line-height:13px;font-size:11px\">Copyright © 2024 Click&Clack.\r\nAll rights reserved.</span> </span>\r\n</em>\r\n</span></p>\r\n\r\n</div>\r\n</div>\r\n\r\n\r\n\r\n</div>\r\n\r\n\r\n</div>\r\n</div>\r\n\r\n\r\n\r\n</div>\r\n</div>\r\n</div>\r\n</td>\r\n</tr>\r\n</tbody>\r\n</table>";

            var userEmail = !string.IsNullOrEmpty(order.Email) ? order.Email : _userRepo.FindUserByIdAsync(order.UserId).Result.Email;
            await _emailSend.SendEmailAsync(userEmail, template.Subject, body, true);
        }

        public async Task SendEmailToAll(string emailTemplateId)
        {

            var users = await _userRepo.GetUsersAsync();
            foreach (var user in users)
            {
                await SendEmailByEmailTemplate(emailTemplateId, user?.Email);
            }

        }

        public async Task SendCouponToAll(string emailTemplateId, string coupon)
        {
            var users = await _userRepo.GetUsersAsync();
            foreach (var user in users)
            {
                await SendEmailCoupon(emailTemplateId, user?.Email, coupon);
            }
        }

        public async Task<EmailTemplate> UpdateEmailTemplate(EmailTemplate updatedEmailTemplate)
        {
            try
            {
                var newEmailTemplate = _dbContext.EmailTemplates.FirstOrDefault(e => e.EmailTemplateId.Equals(updatedEmailTemplate.EmailTemplateId));
                if (newEmailTemplate == null)
                {
                    throw new Exception("Email template is not exist!");
                }
                newEmailTemplate.Active = updatedEmailTemplate.Active;
                newEmailTemplate.Subject = updatedEmailTemplate.Subject;
                newEmailTemplate.Description = updatedEmailTemplate.Description;
                newEmailTemplate.Category = updatedEmailTemplate.Category;
                newEmailTemplate.Body = updatedEmailTemplate.Body;
                newEmailTemplate.Name = updatedEmailTemplate.Name;
                newEmailTemplate.UpdatedBy = updatedEmailTemplate.UpdatedBy;
                newEmailTemplate.UpdatedDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
                return newEmailTemplate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        public async Task ImportEmailTemplates(IFormFile excelFile, string user)
        {
            try
            {
                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\uploads\\";
                if (Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, excelFile.Name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await excelFile.CopyToAsync(stream);
                }


                List<EmailTemplate> templates = new List<EmailTemplate>();
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
                                EmailTemplate s = new EmailTemplate()
                                {
                                    Name = reader.GetValue(0).ToString() ?? "Error Name!",
                                    Description = reader.GetValue(1).ToString() ?? "Error Description!",
                                    Subject = reader.GetValue(2).ToString() ?? "Error Subject!",
                                    Body = reader.GetValue(3).ToString() ?? "Error Body!",
                                    Active = bool.Parse(reader.GetValue(4).ToString() ?? "False"),
                                    Category = reader.GetValue(5).ToString() ?? "Error Category!",
                                    CreatedBy = user,
                                    CreatedDate = DateTime.Now
                                };
                                templates.Add(s);
                            }
                        } while (reader.NextResult());
                    }
                }
                await _dbContext.EmailTemplates.AddRangeAsync(templates);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }
    }
}
