using BusinessLayer.DTOs.User;
using BusinessLayer.DTOs.UserDTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndRazor.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class ChangePasswordModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public ChangePasswordDTO ChangePasswordDTO { get; set; }
        public string Message { get; private set; }

        public ChangePasswordModel(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<IActionResult> OnGetAsync(string id)
        {
            Message = string.Empty;

            try
            {
                ChangePasswordDTO = new ChangePasswordDTO
                {
                    UserId = id,
                    CurrentPassword = string.Empty,
                    NewPassword = string.Empty,
                };
                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(string id)
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
                var result = await _userService.ChangePasswordAsync(ChangePasswordDTO);

                if (result.Succeeded)
                {
                    return RedirectToPage("./Users");
                }

                else
                {
                    Message = "Changing password failed: ";

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Message += $"{modelError.ErrorMessage}";
                    }

                    return Page();
                }
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while changing the password: {ex.Message}";
                return Page();
            }
        }
    }
}
