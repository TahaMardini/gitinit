using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLayer.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BusinessLayer.DTOs.User;

namespace FrontEndRazor.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public LoginDTO LoginDTO { get; set; }

        public string Message { get; private set; }

        public void OnGet()
        {
            Message = string.Empty;
        }

        public async Task<IActionResult> OnPost()
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
                var token = await _userService.LoginUserAsync(LoginDTO);
                if (!string.IsNullOrEmpty(token))
                {
                    HttpContext.Session.SetString("JWToken", token);

                    var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                    if (jwtToken != null)
                    {
                        var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");

                        if (roleClaim != null && roleClaim.Value == "Admin")
                        {
                            return RedirectToPage("/Templates/TaskListTemplate");
                        }
                        else
                        {
                            Message = "You are logged in as a regular user.";
                            return Page();
                        }
                    }
                }

                Message = "Invalid email or password. Please try again.";
                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while trying to log in: {ex.Message}";
                return Page();
            }
        }
    }
}
