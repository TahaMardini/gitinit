using AccessLayerDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLayerDLL.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<ApplicationUser> AddUserAsync(ApplicationUser user, string password);
        Task<bool> UpdateUserAsync(ApplicationUser user);
        Task<string> GetUserRoleAsync(string userId);
        Task<IEnumerable<string>> GetAllRolesAsync();
        Task<bool> DeleteUserAsync(string id);
    }
}
