using ExcelDataReader;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PRN221_GroupProject.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly Prn221GroupProjectContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(Prn221GroupProjectContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<PagedResultDTO<UserListDTO>> GetUsers(string[] statusesParam, string[] rolesParam, string searchTerm, int pageNumber, int pageSize)
        {
            var query = _userManager.Users.AsQueryable();

            //Call filter function 
            query = Filter(statusesParam, query);
            query = Search(query, searchTerm);

            /*if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u =>
                u.Name.Contains(searchTerm) ||
                u.Email.Contains(searchTerm) ||
                u.PhoneNumber.Contains(searchTerm) ||
                (searchTerm.ToLower() == "active" && u.Status) ||
                (searchTerm.ToLower() == "inactive" && !u.Status));
            }

            var users = await query.ToListAsync();*/

            // Calculate total items
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            //Get final result base on page size and page number 
            var pagedUsersQuery = query.OrderByDescending(u => u.Id)
                                       .Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize);

            var pagedUsers = await pagedUsersQuery.ToListAsync();

            var usersWithRoles = new List<UserListDTO>();

            // Retrieve roles for each user
            foreach (var user in pagedUsers)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("customer"))
                {
                    usersWithRoles.Add(new UserListDTO { User = user, Roles = userRoles.ToList() });
                }
            }

            return new PagedResultDTO<UserListDTO>
            {
                Users = usersWithRoles,
                totalPages = totalPages
            };
        }

        private IQueryable<ApplicationUser> Search(IQueryable<ApplicationUser> list, string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                list = list.Where(u =>
                    u.Name.Contains(searchTerm) ||
                    u.Email.Contains(searchTerm) ||
                    u.PhoneNumber.Contains(searchTerm) ||
                    (searchTerm.ToLower() == "active" && u.Status) ||
                    (searchTerm.ToLower() == "inactive" && !u.Status));
            }
            return list;
        }

        private IQueryable<ApplicationUser> Filter(string[] statuses, IQueryable<ApplicationUser> query)
        {
            if (statuses != null && statuses.Length > 0)
            {
                var activeStatuses = statuses.Contains("active");
                var inactiveStatuses = statuses.Contains("inactive");

                query = query.Where(u =>
                    (u.Status && activeStatuses) || (!u.Status && inactiveStatuses)
                );
            }

            /*if (roles != null && roles.Any())
            {
                var userIdsInRoles = GetUserIdsInRolesAsync(roles).Result;
                query = query.Where(u => userIdsInRoles.Contains(u.Id));
            }
*/
            return query;
        }


        public async Task<ApplicationUser> FindUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> DeleteUser(ApplicationUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> CreateUser(Create.InputModel input)
        {
            var user = new ApplicationUser
            {
                Name = input.Name,
                UserName = input.Email,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                Status = input.Status,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, input.Password);

            if (result.Succeeded)
            {
                var role = "customer";
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                await _userManager.AddToRoleAsync(user, role);
            }

            return result;
        }


        public async Task<IdentityResult> EditUser(string id, EditModel.InputModel input)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                // Handle case where user is not found
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            user.Name = input.Name;
            user.PhoneNumber = input.PhoneNumber;
            user.Status = input.Status;

            var result = await _userManager.UpdateAsync(user);

            return result;
        }

        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<string> GetUserNameById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user?.Name ?? "";
        }


        public async Task ImportUsers(IFormFile excelFile)
        {
            try
            {
                /*var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }*/

                var filePath = Path.Combine(excelFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await excelFile.CopyToAsync(stream);
                }

                List<ApplicationUser> users = new List<ApplicationUser>();
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
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

                                var user = new ApplicationUser
                                {
                                    Name = reader.GetValue(0)?.ToString() ?? "Error Name!",
                                    Email = reader.GetValue(1)?.ToString() ?? "Error Email!",
                                    PhoneNumber = reader.GetValue(2)?.ToString() ?? "Error PhoneNumber!",
                                    Status = bool.Parse(reader.GetValue(3)?.ToString() ?? "False"),
                                    UserName = reader.GetValue(1)?.ToString() ?? "Error UserName!",
                                    EmailConfirmed = true
                                };
                                users.Add(user);
                            }
                        } while (reader.NextResult());

                    }
                }
                foreach (var user in users)
                {
                    var result = await _userManager.CreateAsync(user, "@Admin123");
                    if (result.Succeeded)
                    {
                        var role = "customer";
                        if (!await _roleManager.RoleExistsAsync(role))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(role));
                        }
                        await _userManager.AddToRoleAsync(user, role);
                      /*  _dbContext.Users.Add(user);*/
                    }
                }
                await _dbContext.SaveChangesAsync();
            }

            catch (DbUpdateException ex)
            {
                // Log chi tiết lỗi liên quan đến Entity Framework
                throw new Exception($"Lỗi khi lưu thay đổi vào cơ sở dữ liệu: {ex.Message}", ex);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<byte[]> ExportUsers(string[] statusesParam, string searchTerm, int pageNumber, int pageSize)
        {
            try
            {
                var query = _userManager.Users.AsQueryable();
                query = Filter(statusesParam, query);
                query = Search(query, searchTerm);

                var users = await query.ToListAsync();

                DataTable dt = new DataTable();
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("PhoneNumber", typeof(string));
                dt.Columns.Add("Status", typeof(bool));

                foreach (var user in users)
                {
                    DataRow row = dt.NewRow();
                    row["Name"] = user.Name;
                    row["Email"] = user.Email;
                    row["PhoneNumber"] = user.PhoneNumber;
                    row["Status"] = user.Status;
                    dt.Rows.Add(row);
                }

                using (var memory = new MemoryStream())
                {
                    using (var excel = new ExcelPackage(memory))
                    {
                        var worksheet = excel.Workbook.Worksheets.Add("Users");
                        worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                        worksheet.Cells["A1:D1"].Style.Font.Bold = true;
                        worksheet.DefaultRowHeight = 25;

                        return excel.GetAsByteArray();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }

}

