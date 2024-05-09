using PRN221_GroupProject.Models;
using PRN221_GroupProject.Models.DTO;
using Syncfusion.EJ2.FileManager.Base;

namespace PRN221_GroupProject.Repository
{
    public class EmailRepository : IEmailRepository
    {

        private readonly Prn221GroupProjectContext _dbContext;
        public EmailRepository(Prn221GroupProjectContext context)
        {
            _dbContext = context;
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
