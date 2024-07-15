using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Orders
{
    public interface IOrderRepository
    {
        public Task CreateOrderHeader(OrderHeader orderHeader, string userId, string couponId);
        public Task CreateOrderDetail(CartDetail cartDetail, string orderHeaderId);
        public void AdminChangeOrderStatus(string orderHeaderId, string userId);
        public void CancelOrderStatus(string orderHeadeId, string userId);
        public void CustomerChangeOrderStatus(string orderHeaderId, string userId);
        public OrderHeader GetOrderHeaderById(string orderHeaderId);
        public OrderHeader GetOrderHeaderByUserId(string userId);
        public OrderDetail GetOrderDetailByOrderHeaderId(string orderHeaderId);
        public OrderListDTO GetList(string[] statusesParam, string[] categoriesParam, string sortBy, string sortOrder, string searchterm, int pageNumberParam, int pageSizeParam);
        public OrderListDTO GetCustomerList(string[] statusesParam, string[] categoriesParam, string sortBy, string sortOrder, string searchterm, int pageNumberParam, int pageSizeParam, string userId);
        public Task ImportOrdersFile(IFormFile excelFile, string user);
        public Task<Byte[]> ExportOrdersFilter(string[] statusesParam, string[] categoriesParam,  string searchterm, int pageNumberParam, int pageSizeParam);
        public List<double> StatisticIncomeForYear();
        public List<double> StatisticImcomeForFourWeek();
        public double StatisticImcomePerDay();
        public int StatisticProductSaledPerDay();

        public List<TopProductDTO> GetTop12Sales();
    }
}
