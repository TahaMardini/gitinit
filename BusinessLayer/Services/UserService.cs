using AccessLayerDLL.Interfaces;
using AccessLayerDLL.Models;
using BusinessLayer.DTOs.User;
using BusinessLayer.DTOs.UserDTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        var userDTO = new List<UserDTO>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var roleName = roles.FirstOrDefault();

            userDTO.Add(new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = roleName
            });
        }

        return userDTO;
    }


    public async Task<UserDTO> GetUserByIdAsync(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            return null;
        }

        var roles = await _userManager.GetRolesAsync(user);
        var roleName = roles.FirstOrDefault();

        var userDTO = new UserDTO
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = roleName,
        };

        return userDTO;
    }


    public async Task<bool> AddUserAsync(AddUserDTO addUserDTO)
    {
        var user = new ApplicationUser 
        {
            UserName = addUserDTO.Username,
            Email = addUserDTO.Email,
            PhoneNumber = addUserDTO.PhoneNumber
        };

        var createdUser = await _userRepository.AddUserAsync(user, addUserDTO.Password);

        if (createdUser == null)
        {
            Console.WriteLine("User creation failed.");
            return false;
        }

        var role = addUserDTO.Role ?? "User";
        var roleResult = await _userManager.AddToRoleAsync(createdUser, role);

        if (!roleResult.Succeeded)
        {
            foreach (var error in roleResult.Errors)
            {
                Console.WriteLine($"Role assignment failed: {error.Description}");
            }
        }

        return roleResult.Succeeded;
    }


    public async Task<bool> UpdateUserAsync(UpdateUserDTO updateUserDTO)
    {
        var user = await _userRepository.GetUserByIdAsync(updateUserDTO.Id);
        if (user == null)
        {
            return false;
        }

        user.UserName = updateUserDTO.Username;
        user.Email = updateUserDTO.Email;
        user.PhoneNumber = updateUserDTO.PhoneNumber;

        var updated = await _userRepository.UpdateUserAsync(user);
        if(!updated)
        {
            return false;
        }

        var role = await _userRepository.GetUserRoleAsync(updateUserDTO.Id);
        if(role != updateUserDTO.Role)
        {
                await _userManager.RemoveFromRolesAsync(user, new List<string> { role });
                await _userManager.AddToRoleAsync(user, updateUserDTO.Role);
        }

        return true;
    }

    public async Task<string> GetUserRoleAsync(string userId)
    {
        return await _userRepository.GetUserRoleAsync(userId);
    }

    public async Task<IEnumerable<string>> GetAllRolesAsync()
    {
        return await _userRepository.GetAllRolesAsync();
    }


    public async Task<bool> DeleteUserAsync(string userId)
    {
        return await _userRepository.DeleteUserAsync(userId);
    }


    public async Task<string> LoginUserAsync(LoginDTO loginDTO)
    {
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
        {
            return null;
        }

        var roles = await _userManager.GetRolesAsync(user);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("username", user.UserName),
            new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? string.Empty)
        }),
            Expires = DateTime.Now.AddDays(7),
            Issuer = _configuration["JwtSettings:Issuer"],
            Audience = _configuration["JwtSettings:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO)
    {
        var user = await _userRepository.GetUserByIdAsync(changePasswordDTO.UserId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);
        return result;
    }
}
