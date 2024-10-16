using BusinessLayer.DTOs.User;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEndRazor.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        private readonly IUserService _userService;

        public List<UserDTO> Users { get; set; } = new List<UserDTO>();
        public string Message { get; private set; }

        public UsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync()
        {

            Message = string.Empty;

            try
            {
                Users = (await _userService.GetAllUsersAsync()).ToList();
                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while fetching the user list: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    Message = "Invalid user ID. Unable to delete the user.";
                    return Page();
                }

                var result = await _userService.DeleteUserAsync(id);
                if (result)
                {
                    return RedirectToPage();
                }

                Message = "Failed to delete the user. Please try again later.";
                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while deleting the user: {ex.Message}";
                return Page();
            }
        }
    }
}
