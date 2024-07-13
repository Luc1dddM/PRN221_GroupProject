using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_GroupProject.Repository.Orders;

namespace PRN221_GroupProject.Pages.Admin.Statistic
{
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public IndexModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<double> statisticForFourWeek {  get; set; }
        public List<double> statisticForYear {  get; set; }
        public double statisticIncomePerDay { get; set; }
        public int statisticProductSaledPerDay { get; set; }
        public void OnGet()
        {
            statisticForFourWeek = _orderRepository.StatisticImcomeForFourWeek();
            statisticForYear = _orderRepository.StatisticIncomeForYear();
            statisticIncomePerDay = _orderRepository.StatisticImcomePerDay();
            statisticProductSaledPerDay = _orderRepository.StatisticProductSaledPerDay();
        }
    }
}
