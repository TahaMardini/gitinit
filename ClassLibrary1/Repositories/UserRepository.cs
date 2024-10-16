using AccessLayerDLL.Data;
using AccessLayerDLL.Interfaces;
using AccessLayerDLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{

    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public UserRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _context = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
    {
        return await _userManager.Users
            .Select(user => new ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

            })
            .ToListAsync();
    }

    public async Task<ApplicationUser> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }


    public async Task<ApplicationUser> AddUserAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded ? user : null;
    }


    public async Task<bool> UpdateUserAsync(ApplicationUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<string> GetUserRoleAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var roles = await _userManager.GetRolesAsync(user);
        return roles.FirstOrDefault();
    }

    public async Task<IEnumerable<string>> GetAllRolesAsync()
    {
        var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        return roles;
    }


    public async Task<bool> DeleteUserAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return false;

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }
}
