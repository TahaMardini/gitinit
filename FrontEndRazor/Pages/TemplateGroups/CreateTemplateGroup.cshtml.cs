using BusinessLayer.DTOs.TemplateGroupDTO;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndRazor.Pages.TemplateGroups
{
    [Authorize(Roles = "Admin")]
    public class CreateTemplateGroupModel : PageModel
    {
        private readonly ITemplateGroupService _templateGroupService;


        [BindProperty]
        public AddTemplateGroupDTO AddTemplateGroupDTO { get; set; }
        [BindProperty]
        public int TemplateId { get; set; }
        [BindProperty]
        public string TemplateName { get; set; }
        public string Message { get; private set; }

        public CreateTemplateGroupModel(ITemplateGroupService templateGroupService)
        {
            _templateGroupService = templateGroupService;
            Message = string.Empty;
        }


        public void OnGet(int templateId, string tempName)
        {
            TemplateId = templateId;
            TemplateName = tempName;

            AddTemplateGroupDTO = new AddTemplateGroupDTO
            {
                TemplateID = templateId,
            };
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
                await _templateGroupService.CreateGroupAsync(AddTemplateGroupDTO);
                return RedirectToPage("/TemplateGroups/TemplateGroups", new { id = TemplateId, tempName = TemplateName });
            }
            catch (Exception ex)
            {
                Message = $"Error creating group: {ex.Message}";
                return Page();
            }
        }
    }
}
