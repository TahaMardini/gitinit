using AccessLayerDLL.Models;
using BusinessLayer.DTOs.User;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndRazor.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class UpdateUserModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public UpdateUserDTO UpdateUserDTO { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Message { get; private set; }

        public UpdateUserModel(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<IActionResult> OnGetAsync(string id)
        {
            Message = string.Empty;

            try
            {
                if (id == null)
                {
                    Message = "An error occurred: User ID is missing. Please try again later.";
                    return Page();
                }

                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    Message = "User not found. Please try again.";
                    return Page();
                }

                UpdateUserDTO = new UpdateUserDTO
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role,
                };

                Roles = await _userService.GetAllRolesAsync();

                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while fetching the user data: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = "There are validation errors. Please correct them and try again.\n";

                foreach (var state in ModelState)
                {
                    var fieldErrors = state.Value.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    if (fieldErrors.Any())
                    {
                        Message += $"{state.Key}: {string.Join(", ", fieldErrors)}\n";
                    }
                }

                return Page();
            }

            try
            {
                var result = await _userService.UpdateUserAsync(UpdateUserDTO);

                if (result)
                {
                    return RedirectToPage("./Users");
                }

                Message = "Failed to update user. Please try again later.";
                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while updating the user: {ex.Message}";
                return Page();
            }
        }
    }
}
