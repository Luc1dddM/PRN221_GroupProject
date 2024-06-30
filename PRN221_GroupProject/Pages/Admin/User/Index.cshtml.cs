using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository;
using PRN221_GroupProject.Repository.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN221_GroupProject.Pages.User
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailRepository _emailRepository;

        public IndexModel(IUserRepository userRepository, IEmailRepository emailRepository)
        {
            _userRepository = userRepository;
            _emailRepository = emailRepository;
        }

        public IList<UserListDTO> Users { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string SearchTerm { get; set; }

        public string[] statuses { get; set; }
        /*public string[] roles { get; set; }*/

        public async Task<IActionResult> OnGetAsync(string[] statusesParam, string[] rolesParam, string searchTermParam = "", int pageNumberParam = 1, int pageSizeParam = 5)
        {
            try
            {
                PageSize = pageSizeParam;
                PageNumber = pageNumberParam;
                SearchTerm = searchTermParam;

                statuses = statusesParam;
                /*roles = rolesParam;*/

                var result = await _userRepository.GetUsersAsync(statusesParam, rolesParam, searchTermParam, pageNumberParam, pageSizeParam);
                Users = result.Users;
                TotalPages = result.totalPages;

                if (PageNumber < 1 || (PageNumber > TotalPages && TotalPages > 0))
                {
                    return RedirectToPage(new { PageNumber = 1, PageSize = PageSize });
                }
            }
            catch
            {
                TempData["error"] = "You don't have access";
            }
            return Page();
        }


        public ActionResult OnPostSendMail(string emailTemplateId, string userEmail, string? couponCode)
        {
            try
            {
                if (!string.IsNullOrEmpty(couponCode))
                {
                    _emailRepository.SendEmailCoupon(emailTemplateId, userEmail, couponCode);
                }
                else
                {
                    _emailRepository.SendEmailByEmailTemplate(emailTemplateId, userEmail);
                }
                TempData["success"] = "Send Email To User Successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Redirect("/admin/user");
        }

        public async Task<ActionResult> OnPostSendMailToAll(string emailTemplateId, string? couponCode)
        {
            try
            {
                if (!string.IsNullOrEmpty(couponCode))
                {
                    await _emailRepository.SendCouponToAll(emailTemplateId, couponCode);
                }
                else
                {
                    await _emailRepository.SendEmailToAll(emailTemplateId);
                }
                TempData["success"] = "Send Email To All User Successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Redirect("/admin/user");
        }
    }
}
