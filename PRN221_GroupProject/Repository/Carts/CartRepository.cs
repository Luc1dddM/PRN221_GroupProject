using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Categories;
using PRN221_GroupProject.Repository.Products;
using System.Drawing;

namespace PRN221_GroupProject.Repository.Carts
{
    public class CartRepository : ICartRepository
    {
        private readonly Prn221GroupProjectContext _context;
        public CartRepository(Prn221GroupProjectContext context)
        {
            _context = context;
        }

        public void CreateCartHeader(string userId)
        {
            var cartHeader = GetCartHeaderByUserId(userId);
            try
            {
                if (cartHeader == null)
                {
                    CartHeader newCartHeader = new CartHeader
                    {
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now,
                    };
                    _context.CartHeaders.Add(newCartHeader);

                }
                else
                {
                    cartHeader.UpdatedBy = userId;
                    cartHeader.UpdatedDate = DateTime.Now;
                    _context.CartHeaders.Update(cartHeader);
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CreateCartDetail(CartDetail cartDetail, string userId)
        {
            try
            {
                var cartHeader = GetCartHeaderByUserId(userId);

                var product = _context.Products.FirstOrDefault(p => p.ProductId == cartDetail.ProductId);
                /*var categoryColor = _context.Categories.Include(ct => ct.ProductCategories)
                                                       .ThenInclude(ct => ct.Product)
                                                       .Where(ct => ct.Type.Equals("Color") &&
                                                                    ct.ProductCategories.Any(pc => pc.ProductId.Equals(product.ProductId)))
                                                       .Select(ct => ct.Name).FirstOrDefault();*/
                cartDetail.CartId = cartHeader.CartId;
                cartDetail.Price = product.Price;
                _context.CartDetails.Add(cartDetail);
                _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void UpdateCartDetailQuantity(CartDetail cartDetail, string userId)
        {
            try
            {
                var cartHeader = GetCartHeaderByUserId(userId);
                var existedCartDetail = GetCartDetailByCartId_ProId(cartHeader.CartId, cartDetail.ProductId, cartDetail.Color);
                if (existedCartDetail != null)
                {
                    existedCartDetail.Count += cartDetail.Count;
                    _context.CartDetails.Update(existedCartDetail);
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void UpdateCartDetailColor(CartDetail cartDetail, string userId)
        {
            try
            {
                var cartHeader = GetCartHeaderByUserId(userId);
                var existedCartDetail = GetCartDetailByCartId_ProId(cartHeader.CartId, cartDetail.ProductId, cartDetail.Color);
                if (existedCartDetail != null)
                {
                    //get the same cartDetail with different color 
                    var sameCartDetail = GetCartDetailByCartId_ProId(cartHeader.CartId, cartDetail.ProductId, cartDetail.Color);
                    if ((existedCartDetail.ProductId.Equals(sameCartDetail.ProductId)) && (existedCartDetail.Color.Equals(sameCartDetail.Color)))
                    {
                        sameCartDetail.Count += existedCartDetail.Count;
                        DeleteCartDetail(existedCartDetail, userId);
                    }
                    existedCartDetail.Color = cartDetail.Color;
                    _context.CartDetails.Update(existedCartDetail);
                }
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void UpdateCartDetailQuantityByInputField(CartDetail cartDetail, string userId)
        {
            try
            {
                var cartHeader = GetCartHeaderByUserId(userId);
                var existedCartDetail = GetCartDetailByCartId_ProId(cartHeader.CartId, cartDetail.ProductId, cartDetail.Color);
                if (existedCartDetail != null)
                {
                    existedCartDetail.Count = cartDetail.Count;
                    _context.CartDetails.Update(existedCartDetail);
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteCartDetail(CartDetail cartDetail, string userId)
        {
            try
            {
                var cartHeader = GetCartHeaderByUserId(userId);
                var existedCartDetail = GetCartDetailByCartId_ProId(cartHeader.CartId, cartDetail.ProductId, cartDetail.Color);
                if (existedCartDetail != null)
                {
                    _context.CartDetails.Remove(existedCartDetail);
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public CartHeader GetCartHeaderByUserId(string userId)
        {
            try
            {
                return _context.CartHeaders.FirstOrDefault(c => c.CreatedBy == userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CartDetail GetCartDetailByCartId_ProId(string cartHeaderId, string productId, string color)
        {
            try
            {
                return _context.CartDetails.Include(cd => cd.Product).FirstOrDefault(cd => cd.CartId.Equals(cartHeaderId) &&
                                                                                           cd.ProductId.Equals(productId) &&
                                                                                           cd.Color.Equals(color));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CartDetail GetCartDetailByCartHeaderId_ProductId(string cartHeaderId, string productId)
        {
            try
            {
                return _context.CartDetails.Include(cd => cd.Product).FirstOrDefault(cd => cd.CartId.Equals(cartHeaderId) &&
                                                                                  cd.ProductId.Equals(productId));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IList<CartDetail> GetCartDetailsByUserId(string userId)
        {
            try
            {
                return _context.CartDetails.Include(cd => cd.Product)
                                           .Where(cd => cd.Cart.CreatedBy == userId).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CartDetail GetCartDetailById(string cartDetailId)
        {
            try
            {
                return _context.CartDetails.FirstOrDefault(cd => cd.CartDetailId == cartDetailId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
