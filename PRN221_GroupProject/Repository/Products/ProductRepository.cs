using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.DTO.product;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Categories;
using PRN221_GroupProject.Repository.File;
using PRN221_GroupProject.Repository.ProductCategories;
using PRN221_GroupProject.Repository.Users;
using System.Data;

namespace PRN221_GroupProject.Repository.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly Prn221GroupProjectContext _dbContext;
        private readonly IProductCategorieRepository _productCategoriesRepository;
        private readonly ICategoryRepository _categoryRepository;
        public IUserRepository _userRepo;
        private readonly IFileUploadRepository _fileUploadRepository;
        public ProductRepository(Prn221GroupProjectContext Context,
            IProductCategorieRepository productCategorieRepository,
            IFileUploadRepository fileUploadRepository,
            ICategoryRepository categoryRepository,
            IUserRepository userRepository)
        {
            _dbContext = Context;
            _productCategoriesRepository = productCategorieRepository;
            _fileUploadRepository = fileUploadRepository;
            _categoryRepository = categoryRepository;
            _userRepo = userRepository;
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

        public ProductListDTO GetListCustomer(string[] colorParam, string[] brandParam, string[] deviceParam, string Price1, string Price2, string searchterm, int pageNumberParam, int pageSizeParam)
        {
            //Get List from db
            var result = _dbContext.Products.Include(p => p.ProductCategories).ThenInclude(p => p.Category).Where(p => p.Status && p.ProductCategories.Any(pc => pc.Category.Type.Equals("Color") && pc.Status)).ToList();

            //Call filter function 
            result = Filter(colorParam, brandParam, deviceParam, Price1, Price2, result);
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

        public ProductListDTO GetList(string[] colorParam, string[] brandParam, string[] deviceParam, string Price1, string Price2, string searchterm, int pageNumberParam, int pageSizeParam)
        {
            //Get List from db
            var result = _dbContext.Products.Include(p => p.ProductCategories).ToList();

            //Call filter function 
            result = Filter(colorParam, brandParam, deviceParam, Price1, Price2, result);
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

        private List<Product> Filter(string[] colorParam, string[] brand, string[] device, string Price1, string Price2, List<Product> list)
        {
            if (brand != null && brand.Length > 0)
            {
                list = list.Where(e => e.ProductCategories.Any(p => brand.Any(b => b.Equals(p.CategoryId)))).ToList();
            }

            if (device != null && device.Length > 0)
            {
                list = list.Where(e => e.ProductCategories.Any(p => device.Any(b => b.Equals(p.CategoryId)))).ToList();

            }

            if (colorParam != null && colorParam.Length > 0)
            {
                list = list.Where(e => e.ProductCategories.Any(p => colorParam.Any(b => b.Equals(p.CategoryId)))).ToList();

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

        public async Task ImportProducts(IFormFile excelFile, string user)
        {
            try
            {
                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\uploads\\";

                var filePath = Path.Combine(uploadsFolder, excelFile.Name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await excelFile.CopyToAsync(stream);
                }



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

                                Product s = new Product()
                                {
                                    Name = reader.GetValue(0).ToString() ?? "Error Name!",
                                    Price = double.Parse(reader.GetValue(1).ToString() ?? "0"),
                                    Description = reader.GetValue(2).ToString() ?? string.Empty,
                                    ImageUrl = reader.GetValue(3).ToString() ?? string.Empty,
                                    Status = bool.Parse(reader.GetValue(4).ToString() ?? "False"),
                                };
                                Create(s, user);
                                var brand = reader.GetValue(5).ToString() ?? "Error Brand!";
                                var device = reader.GetValue(6).ToString() ?? "Error Device!";
                                var color = reader.GetValue(7).ToString() ?? "Error Color!";
                                var quantity = int.Parse(reader.GetValue(8).ToString() ?? "0");
                                _productCategoriesRepository.CreateProductCategories(_categoryRepository.GetCategoryByName(brand).CategoryId, _categoryRepository.GetCategoryByName(device).CategoryId, _categoryRepository.GetCategoryByName(color).CategoryId, s.ProductId, quantity, s.Status, user);


                            }
                        } while (reader.NextResult());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<byte[]> ExportProductsFilter(string[] colorParam, string[] brandParam, string[] deviceParam, string Price1, string Price2, string searchterm, int pageNumberParam, int pageSizeParam)
        {
            try
            {
                //Get List from db
                var result = await _dbContext.Products.Include(p => p.ProductCategories).ToListAsync();

                //Call filter function 
                result = Filter(colorParam, brandParam, deviceParam, Price1, Price2, result);
                result = Search(result, searchterm);

                DataTable dt = new DataTable();
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Price", typeof(double));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Image Base64", typeof(string));
                dt.Columns.Add("Status", typeof(bool));
                dt.Columns.Add("Brand", typeof(string));
                dt.Columns.Add("Device", typeof(string));
                dt.Columns.Add("Color", typeof(string));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("Created By", typeof(string));
                dt.Columns.Add("Created Date", typeof(string));
                dt.Columns.Add("Updated By", typeof(string));
                dt.Columns.Add("Updated Date", typeof(string));

                foreach (var product in result)
                {
                    foreach (var item in _categoryRepository.GetChoosedColors(product))
                    {
                        DataRow row = dt.NewRow();
                        row[0] = product.Name;
                        row[1] = product.Price;
                        row[2] = product.Description;
                        row[3] = product.ImageUrl;
                        row[4] = product.Status;

                        row[5] = _categoryRepository.GetBrandsByProduct(product).Name;
                        row[6] = _categoryRepository.GetDevicesByProduct(product).Name;
                        row[7] = item.Name;
                        row[8] = _productCategoriesRepository.GetProductCategoriesByCategoryAndProductID(item.CategoryId, product.ProductId).Quantity;

                        row[9] = await _userRepo.GetUserNameById(item.CreatedBy);
                        row[10] = item.CreatedAt;
                        row[11] = await _userRepo.GetUserNameById(item.UpdatedBy);
                        row[12] = item.UpdatedAt;
                        dt.Rows.Add(row);
                    }

                }

                var memory = new MemoryStream();
                using (var excel = new ExcelPackage(memory))
                {
                    var worksheet = excel.Workbook.Worksheets.Add("Sheet1");

                    worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                    worksheet.Cells["A1:AN1"].Style.Font.Bold = true;
                    worksheet.DefaultRowHeight = 25;


                    return excel.GetAsByteArray();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
