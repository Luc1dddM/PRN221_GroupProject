using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;

namespace PRN221_GroupProject.Repository.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly Prn221GroupProjectContext _dbContext;
        public ProductRepository(Prn221GroupProjectContext Context)
        {
            _dbContext = Context;
        }
        public void Create(Product product, string user)
        {
            product.CreatedBy = user;
            product.UpdatedBy = user;
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void Disable(string productId, string user)
        {
            try
            {
                Product product = GetProductByID(productId);
                product.UpdatedBy = user;
                product.UpdatedAt = DateTime.Now;
                product.Status = false;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Enable(string productId, string user)
        {
            try
            {
                Product product = GetProductByID(productId);
                product.UpdatedBy = user;
                product.UpdatedAt = DateTime.Now;
                product.Status = true;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Product> GetAll()
        {
            try
            {
                return _dbContext.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Product GetProductByID(string productId)
        {
            try
            {
                return _dbContext.Products.FirstOrDefault(p => p.ProductId.Equals(productId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Product GetProductByIDInclude(string productId)
        {
            try
            {
                return _dbContext.Products.Include(p => p.ProductCategories).FirstOrDefault(p => p.ProductId.Equals(productId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProductListDTO GetListCustomer(string[] brandParam, string[] deviceParam, string Price1, string Price2, string searchterm, int pageNumberParam, int pageSizeParam)
        {
            //Get List from db
            var result = _dbContext.Products.Include(p => p.ProductCategories).Where(p => p.Status).ToList();

            //Call filter function 
            result = Filter(brandParam, deviceParam, Price1, Price2, result);
            result = Search(result, searchterm);


            //Calculate pagination
            var totalItems = result.Count();
            var TotalPages = (int)Math.Ceiling((double)totalItems / pageSizeParam);

            //Get final result base on page size and page number 
            result = result.OrderByDescending(e => e.Id)
                    .Skip((pageNumberParam - 1) * pageSizeParam)
                    .Take(pageSizeParam)
                    .ToList();

            return new ProductListDTO()
            {
                listProduct = result,
                totalPages = TotalPages
            };
        }

        public ProductListDTO GetList(string[] brandParam, string[] deviceParam, string Price1, string Price2, string searchterm, int pageNumberParam, int pageSizeParam)
        {
            //Get List from db
            var result = _dbContext.Products.Include(p => p.ProductCategories).ToList();

            //Call filter function 
            result = Filter(brandParam, deviceParam, Price1, Price2, result);
            result = Search(result, searchterm);

            //Calculate pagination
            var totalItems = result.Count();
            var TotalPages = (int)Math.Ceiling((double)totalItems / pageSizeParam);

            //Get final result base on page size and page number 
            result = result.OrderByDescending(e => e.Id)
                    .Skip((pageNumberParam - 1) * pageSizeParam)
                    .Take(pageSizeParam)
                    .ToList();

            return new ProductListDTO()
            {
                listProduct = result,
                totalPages = TotalPages
            };
        }

        private List<Product> Filter(string[] brand, string[] device, string Price1, string Price2, List<Product> list)
        {
            if (brand != null && brand.Length > 0)
            {
                list = list.Where(e => e.ProductCategories.Any(p => brand.Any(b => b.Equals(p.CategoryId)))).ToList();
            }

            if (device != null && device.Length > 0)
            {
                list = list.Where(e => e.ProductCategories.Any(p => device.Any(b => b.Equals(p.CategoryId)))).ToList();

            }
            if (!string.IsNullOrEmpty(Price1) && string.IsNullOrEmpty(Price2) && double.Parse(Price1) > 0)
            {
                list = list.Where(e => e.Price >= double.Parse(Price1)).ToList();
            }
            if (!string.IsNullOrEmpty(Price2) && !string.IsNullOrEmpty(Price1) && double.Parse(Price2) > 0 && double.Parse(Price2) > double.Parse(Price1) && double.Parse(Price1) > 0)
            {
                list = list.Where(e => e.Price >= double.Parse(Price1) && e.Price <= double.Parse(Price2)).ToList();
            }
            if (!string.IsNullOrEmpty(Price2) && string.IsNullOrEmpty(Price1) && double.Parse(Price2) > 0)
            {
                list = list.Where(e => e.Price <= double.Parse(Price2)).ToList();
            }

            return list;
        }

        private List<Product> Search(List<Product> list, string searchtearm)
        {
            if (!string.IsNullOrEmpty(searchtearm))
            {
                list = list.Where(p =>
                            p.Name.Contains(searchtearm, StringComparison.OrdinalIgnoreCase))
                            .ToList();
            }
            return list;
        }



        public void Update(Product product, string user)
        {
            try
            {
                Product newProduct = GetProductByID(product.ProductId);
                newProduct.Id = product.Id;
                newProduct.ProductId = product.ProductId;
                newProduct.Name = product.Name;
                newProduct.Description = product.Description;
                newProduct.Price = product.Price;
                newProduct.ImageUrl = product.ImageUrl;
                newProduct.Status = product.Status;
                newProduct.UpdatedBy = user;
                newProduct.UpdatedAt = DateTime.Now;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
