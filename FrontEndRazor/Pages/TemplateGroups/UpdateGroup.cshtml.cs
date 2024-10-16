using BusinessLayer.DTOs.TemplateGroupDTO;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndRazor.Pages.TemplateGroups
{
    [Authorize(Roles = "Admin")]
    public class UpdateGroupModel : PageModel
    {
        private readonly ITemplateGroupService _templateGroupService;

        [BindProperty]
        public UpdateTemplateGroupDTO updateTemplateGroupDTO { get; set; }
        [BindProperty]
        public int TemplateID { get; set; }
        [BindProperty]
        public string TempName { get; set; }
        public string Message { get; private set; }

        public UpdateGroupModel(ITemplateGroupService templateGroupService)
        {
            _templateGroupService = templateGroupService;
            Message = string.Empty;
        }


        public async Task<IActionResult> OnGetAsync(int Id, int templateId, string tempName)
        {
            try
            {
                TemplateID = templateId;
                TempName = tempName;

                var group = await _templateGroupService.GetGroupByIdAsync(Id);
                if (group == null)
                {
                    Message = "Group not found.";
                    return Page();
                }

                updateTemplateGroupDTO = new UpdateTemplateGroupDTO
                {
                    Id = group.Id,
                    GroupName = group.GroupName,
                    TemplateID = group.TemplateID,
                };

                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while retrieving the groups: {ex.Message}";
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
                await _templateGroupService.UpdateGroupAsync(updateTemplateGroupDTO);
                return RedirectToPage("/TemplateGroups/TemplateGroups", new { id = TemplateID, tempName = TempName});
            }
            catch (Exception ex)
            {
                Message = $"Error updating group: {ex.Message}";
                return Page();
            }
        }
    }
}
