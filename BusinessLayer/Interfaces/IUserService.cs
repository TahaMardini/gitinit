using AccessLayerDLL.Models;
using BusinessLayer.DTOs.User;
using BusinessLayer.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(string userId);
        Task<bool> AddUserAsync(AddUserDTO addUserDTO);
        Task<bool> UpdateUserAsync(UpdateUserDTO updateUserDTO);
        Task<string> GetUserRoleAsync(string userId);
        Task<IEnumerable<string>> GetAllRolesAsync();
        Task<bool> DeleteUserAsync(string userId);
        Task<string> LoginUserAsync(LoginDTO loginDTO);
        Task<IdentityResult> ChangePasswordAsync (ChangePasswordDTO changePasswordDTO);
    }
}
