﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PRN221_GroupProject.DTO;
using PRN221_GroupProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN221_GroupProject.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<PagedResultDTO<UserListDTO>> GetUsersAsync(string[] statusesParam, string[] rolesParam, string searchTerm, int pageNumber, int pageSize)
        {
            var query = _userManager.Users.AsQueryable();

            //Call filter function 
            query = Filter(statusesParam, rolesParam, query);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u =>
                u.Name.Contains(searchTerm) ||
                u.Email.Contains(searchTerm) ||
                u.PhoneNumber.Contains(searchTerm) ||
                (searchTerm.ToLower() == "active" && u.Status) ||
                (searchTerm.ToLower() == "inactive" && !u.Status));
            }

            var users = await query.ToListAsync();

            // Calculate total items
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Get paged users
            var pagedUsers = await query.Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

            var usersWithRoles = new List<UserListDTO>();

            // Retrieve roles for each user
            foreach (var user in pagedUsers)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                usersWithRoles.Add(new UserListDTO { User = user, Roles = userRoles.ToList() });
            }

            return new PagedResultDTO<UserListDTO>
            {
                Users = usersWithRoles,
                totalPages = totalPages
            };
        }

        private IQueryable<ApplicationUser> Filter(string[] statuses, string[] roles, IQueryable<ApplicationUser> query)
        {
            if (statuses != null && statuses.Length > 0)
            {
                var activeStatuses = statuses.Contains("active");
                var inactiveStatuses = statuses.Contains("inactive");

                query = query.Where(u =>
                    (u.Status && activeStatuses) || (!u.Status && inactiveStatuses)
                );
            }

            if (roles != null && roles.Any())
            {
                var userIdsInRoles = GetUserIdsInRolesAsync(roles).Result;
                query = query.Where(u => userIdsInRoles.Contains(u.Id));
            }

            return query;
        }

        private async Task<List<string>> GetUserIdsInRolesAsync(string[] roles)
        {
            var userIdsInRoles = new List<string>();

            foreach (var role in roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role);
                userIdsInRoles.AddRange(usersInRole.Select(u => u.Id));
            }

            return userIdsInRoles;
        }

        public async Task<ApplicationUser> FindUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> CreateUserAsync(Create.InputModel input)
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
                if (!await _roleManager.RoleExistsAsync(input.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(input.Role));
                }

                await _userManager.AddToRoleAsync(user, input.Role);
            }

            return result;
        }

        public async Task<string> GetUserRoleAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault(); // Assuming user has only one role
        }

        public async Task<IdentityResult> EditUserAsync(string id, EditModel.InputModel input)
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

            if (result.Succeeded && !string.IsNullOrEmpty(input.Role))
            {
                // If the role has been changed, update it
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles.ToArray());

                if (!await _roleManager.RoleExistsAsync(input.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(input.Role));
                }

                await _userManager.AddToRoleAsync(user, input.Role);
            }

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
    }

}

