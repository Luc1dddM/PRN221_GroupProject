using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository;

namespace PRN221_GroupProject.Pages.Email
{
    [BindProperties]
    public class testModel : PageModel
    {
        private readonly Prn221GroupProjectContext _dbcontext;
        public IEmailRepository _emailRepo;
        public string emailTemplateId { get; set; }
        public testModel(Prn221GroupProjectContext dbcontext, IEmailRepository emailRepository)
        {
            _dbcontext = dbcontext;
            _emailRepo = emailRepository;
        }
        public async Task<PageResult> OnGetAsync()
        {
            await _emailRepo.SendEmailOrder(new OrderHeader());
            return Page();
        }

        public PageResult OnPost() { 
            Console.WriteLine(emailTemplateId);
            return Page();
        }

    }
}
