﻿using ExcelDataReader;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.DTO.category;
using PRN221_GroupProject.Models;
using PRN221_GroupProject.Repository.Products;
using PRN221_GroupProject.Repository.Users;
using System.Data;

namespace PRN221_GroupProject.Repository.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Prn221GroupProjectContext _dbContext;
        public IProductRepository _productsRepository;
        public IUserRepository _userRepo;


        public CategoryRepository(Prn221GroupProjectContext Context, IProductRepository productRepository, IUserRepository userRepository)
        {
            _dbContext = Context;
            _productsRepository = productRepository;
            _userRepo = userRepository;
        }

        public void Create(Category category, string user)
        {
            try
            {
                category.CreatedBy = user;
                category.UpdatedBy = user;
                category.UpdatedAt = DateTime.Now;
                category.CreatedAt = DateTime.Now;
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetCategories()
        {
            try
            {
                return _dbContext.Categories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Category GetCategoryByID(string categoryId)
        {
            try
            {
                return _dbContext.Categories.FirstOrDefault(c => c.CategoryId.Equals(categoryId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void update(Category category, string user)
        {
            try
            {
                var newCategory = GetCategoryByID(category.CategoryId);
                newCategory.Name = category.Name;
                newCategory.Type = category.Type;
                newCategory.UpdatedBy = user;
                newCategory.UpdatedAt = DateTime.Now;
                newCategory.Status = category.Status;
                _dbContext.SaveChanges();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetColors(Product Product)
        {
            try
            {
                List<ProductCategory> productCategories = Product.ProductCategories.ToList();
                List<Category> categories = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => c.Type.Equals("Color")).ToList();
                categories = categories.Where(c => !c.ProductCategories.Any(p => p.ProductId.Equals(Product.ProductId))).ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetChoosedColors(Product Product)
        {
            try
            {
                List<ProductCategory> productCategories = Product.ProductCategories.ToList();
                List<Category> categories = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => c.Type.Equals("Color")).ToList();
                categories = categories.Where(c => c.ProductCategories.Any(p => p.ProductId.Equals(Product.ProductId))).ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetDevicesByProduct(Product Product)
        {
            try
            {

                List<Category> categories = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => !c.Type.Equals("Color") && !c.Type.Equals("Brand")).ToList();
                categories = categories.Where(c => !c.ProductCategories.Any(p => p.ProductId.Equals(Product.ProductId))).ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetBrandsByProduct(Product Product)
        {
            try
            {

                List<Category> categories = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => !c.Type.Equals("Color") && !c.Type.Equals("Device")).ToList();
                categories = categories.Where(c => !c.ProductCategories.Any(p => p.ProductId.Equals(Product.ProductId))).ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public List<Category> GetChoosedCategoriesByProduct(Product Product)
        {
            try
            {

                List<Category> categories = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => !c.Type.Equals("Color")).ToList();
                categories = categories.Where(c => c.ProductCategories.Any(p => p.ProductId.Equals(Product.ProductId))).ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetBrands()
        {
            try
            {
                return _dbContext.Categories.Where(c => c.Type.Equals("Brand") && c.Status).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetDevices()
        {
            try
            {
                return _dbContext.Categories.Where(c => c.Type.Equals("Device") && c.Status).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Category> GetColors()
        {
            try
            {
                return _dbContext.Categories.Where(c => c.Type.Equals("Color") && c.Status).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CategoryListDTO GetList(string[] statusesParam, string[] TypeParam, string searchterm, string sortBy, string sortOrder, int pageNumberParam, int pageSizeParam)
        {
            //Get List from db
            var result = _dbContext.Categories.ToList();

            //Call filter function 
            result = Filter(statusesParam, TypeParam, result);
            result = Search(result, searchterm);
            result = Sort(sortBy, sortOrder, result);


            //Calculate pagination
            var totalItems = result.Count();
            var TotalPages = (int)Math.Floor((double)totalItems / pageSizeParam);

            //Get final result base on page size and page number 
            result = result.Skip((pageNumberParam - 1) * pageSizeParam)
                    .Take(pageSizeParam)
                    .ToList();

            return new CategoryListDTO()
            {
                listCategory = result,
                totalPages = TotalPages
            };
        }

        private List<Category> Filter(string[] statuses, string[] types, List<Category> list)
        {
            if (types != null && types.Length > 0)
            {
                list = list.Where(e => types.Contains(e.Type)).ToList();
            }

            if (statuses != null && statuses.Length > 0)
            {
                list = list.Where(e => statuses.Contains(e.Status.ToString())).ToList();
            }

            return list;
        }

        private List<Category> Search(List<Category> list, string searchtearm)
        {
            if (!string.IsNullOrEmpty(searchtearm))
            {
                list = list.Where(p =>
                            p.Name.Contains(searchtearm, StringComparison.OrdinalIgnoreCase))
                            .ToList();
            }
            return list;
        }
        private List<Category> Sort(string sortBy, string sortOrder, List<Category> list)
        {
            switch (sortBy)
            {
                case "name":
                    list = sortOrder == "asc" ? list.OrderBy(e => e.Name).ToList() : list.OrderByDescending(e => e.Name).ToList();
                    break;
                case "status":
                    list = sortOrder == "asc" ? list.OrderBy(e => e.Status).ToList() : list.OrderByDescending(e => e.Status).ToList();
                    break;
                case "type":
                    list = sortOrder == "asc" ? list.OrderBy(e => e.Type).ToList() : list.OrderByDescending(e => e.Type).ToList();
                    break;
                case "createdBy":
                    list = sortOrder == "asc" ? list.OrderBy(e => e.CreatedBy).ToList() : list.OrderByDescending(e => e.CreatedBy).ToList();
                    break;
                case "createdAt":
                    list = sortOrder == "asc" ? list.OrderBy(e => e.CreatedAt).ToList() : list.OrderByDescending(e => e.CreatedAt).ToList();
                    break;
                default:
                    list = list.OrderByDescending(e => e.Id).ToList();
                    break;
            }
            return list;
        }

        public bool haveDevice(Product Product)
        {
            try
            {
                List<Category> tmp = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => !c.Type.Equals("Color") && !c.Type.Equals("Brand")).ToList();
                List<Category> categories = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => !c.Type.Equals("Color") && !c.Type.Equals("Brand")).ToList();
                categories = categories.Where(c => !c.ProductCategories.Any(p => p.ProductId.Equals(Product.ProductId))).ToList();
                return tmp.Count > categories.Count ? false : true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool haveBrand(Product Product)
        {
            try
            {
                List<Category> tmp = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => !c.Type.Equals("Color") && !c.Type.Equals("Device")).ToList();
                List<Category> categories = _dbContext.Categories.Include(c => c.ProductCategories).Where(c => !c.Type.Equals("Color") && !c.Type.Equals("Device")).ToList();
                categories = categories.Where(c => !c.ProductCategories.Any(p => p.ProductId.Equals(Product.ProductId))).ToList();
                return tmp.Count > categories.Count ? false : true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ImportCategories(IFormFile excelFile, string user)
        {
            try
            {
                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\uploads\\";

                var filePath = Path.Combine(uploadsFolder, excelFile.Name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await excelFile.CopyToAsync(stream);
                }


                List<Category> Categories = new List<Category>();
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

                                Category s = new Category()
                                {
                                    Name = reader.GetValue(0).ToString() ?? "Error Name!",
                                    Type = reader.GetValue(1).ToString() ?? "Error Type!",
                                    Status = bool.Parse(reader.GetValue(2).ToString() ?? "False"),
                                    CreatedBy = user,
                                    CreatedAt = DateTime.Now,
                                    UpdatedBy = user,
                                    UpdatedAt = DateTime.Now
                                };
                                Categories.Add(s);
                            }
                        } while (reader.NextResult());
                    }
                }
                await _dbContext.Categories.AddRangeAsync(Categories);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<byte[]> ExportCategoriesFilter(string[] statusesParam, string[] TypeParam, string searchterm, int pageNumberParam, int pageSizeParam)
        {
            try
            {
                //Get List from db
                var result = await _dbContext.Categories.ToListAsync();

                //Call filter function 
                result = Filter(statusesParam, TypeParam, result);
                result = Search(result, searchterm);

                DataTable dt = new DataTable();
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Type", typeof(string));
                dt.Columns.Add("Status", typeof(bool));
                dt.Columns.Add("Created By", typeof(string));
                dt.Columns.Add("Created Date", typeof(string));
                dt.Columns.Add("Updated By", typeof(string));
                dt.Columns.Add("Updated Date", typeof(string));

                foreach (var item in result)
                {
                    DataRow row = dt.NewRow();
                    row[0] = item.Name;
                    row[1] = item.Type;
                    row[2] = item.Status;
                    row[3] = await _userRepo.GetUserNameById(item.CreatedBy);
                    row[4] = item.CreatedAt;
                    row[5] = await _userRepo.GetUserNameById(item.UpdatedBy);
                    row[6] = item.UpdatedAt;
                    dt.Rows.Add(row);
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
