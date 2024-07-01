using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Enums;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Prn221GroupProjectContext _context;
        public OrderRepository(Prn221GroupProjectContext context)
        {
            _context = context;
        }

        public async Task CreateOrderHeader(OrderHeader orderHeader, string userId)
        {

            try
            {
                orderHeader.OrderStatus = OrderStatusEnum.Pending.ToString();
                orderHeader.TotalPrice = orderHeader.TotalPrice;
                orderHeader.CreatedBy = userId;
                orderHeader.CreatedDate = DateTime.Now;
                _context.OrderHeaders.Add(orderHeader);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task CreateOrderDetail(CartDetail cartDetail, string orderHeaderId)
        {
            try
            {
                var orderHeader = GetOrderHeaderById(orderHeaderId);

                OrderDetail newOrderDetail = new OrderDetail
                {
                    OrderHeaderId = orderHeader.OrderHeaderId,
                    ProductId = cartDetail.ProductId,
                    Color = cartDetail.Color,
                    Count = cartDetail.Count,
                    Price = cartDetail.Price,
                };
                await _context.OrderDetails.AddAsync(newOrderDetail);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public OrderHeader GetOrderHeaderByUserId(string userId)
        {
            try
            {
                return _context.OrderHeaders.FirstOrDefault(o => o.CreatedBy == userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public OrderDetail GetOrderDetailByOrderHeaderId(string orderHeaderId)
        {
            try
            {
                return _context.OrderDetails.FirstOrDefault(od => od.OrderHeaderId == orderHeaderId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public OrderHeader GetOrderHeaderById(string orderHeaderId)
        {
            try
            {
                return _context.OrderHeaders.Include(oh => oh.OrderDetails)
                                            .FirstOrDefault(oh => oh.OrderHeaderId.Equals(orderHeaderId));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public OrderListDTO GetList(string[] statusesParam, string[] categoriesParam, string searchterm, int pageNumberParam, int pageSizeParam)
        {
            //Get List from db
            var result = _context.OrderHeaders.ToList();

            //Call filter function 
            result = Filter(statusesParam, categoriesParam, result);
            result = Search(result, searchterm);
            
            //Calculate pagination
            var totalItems = result.Count();
            var TotalPages = (int)Math.Ceiling((double)totalItems / pageSizeParam);

            result = result.Skip((pageNumberParam - 1) * pageSizeParam)
                           .Take(pageSizeParam)
                           .ToList();

            return new OrderListDTO
            {
                listOrder = result,
                totalPages = TotalPages,
            };
        }

        private List<OrderHeader> Filter(string[] statuses, string[] categories, List<OrderHeader> list) //string coupon
        {
            if (statuses != null && statuses.Length > 0)
            {
                list = list.Where(l => statuses.Contains(l.OrderStatus)).ToList();
            }

            if (categories != null && categories.Length > 0)
            {
                list = list.Where(l => l.CouponId != null).ToList();
            }
            return list;
        }
        
        private List<OrderHeader> Search(List<OrderHeader> list, string searchtearm)
        {
            if (!string.IsNullOrEmpty(searchtearm))
            {
                list = list.Where(l => 
                            l.Name.Contains(searchtearm, StringComparison.OrdinalIgnoreCase) ||
                            l.Phone.Contains(searchtearm, StringComparison.OrdinalIgnoreCase) ||
                            l.Email.Contains(searchtearm, StringComparison.OrdinalIgnoreCase))
                            .ToList();
            }
            return list;
        }
    }
}
