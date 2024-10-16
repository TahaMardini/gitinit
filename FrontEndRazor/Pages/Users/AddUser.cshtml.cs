using AccessLayerDLL.Models;
using BusinessLayer.DTOs.User;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace FrontEndRazor.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class AddUserModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public AddUserDTO AddUserDTO { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Message { get; private set; }

        public AddUserModel(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            Message = string.Empty;

            try
            {
                Roles = await _userService.GetAllRolesAsync();
                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while fetching roles: {ex.Message}";
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
                var result = await _userService.AddUserAsync(AddUserDTO);

                if (result)
                {
                    return RedirectToPage("./Users");
                }

                Message = "Failed to create user. Please try again.";
                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while creating the user: {ex.Message}";
                return Page();
            }
        }
    }
}
