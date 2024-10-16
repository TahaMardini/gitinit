using AccessLayerDLL.Models;
using BusinessLayer.DTOs.TemplateGroupDTO;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndRazor.Pages.TemplateGroups
{
    [Authorize(Roles = "Admin")]
    public class TemplateGroupsModel : PageModel
    {
        private readonly ITemplateGroupService _templateGroupService;

        [BindProperty]
        public int TemplateID { get; set; }

        [BindProperty]
        public string TemplateName { get; set; }
        public IEnumerable<TemplateGroupDTO> TemplateGroups { get; set; }
        public string Message { get; private set; }

        public TemplateGroupsModel(ITemplateGroupService templateGroupService)
        {
            _templateGroupService = templateGroupService;
            Message = string.Empty;
        }

        public async Task<IActionResult> OnGetAsync(int Id, string tempName)
        {
            try
            {
                TemplateID = Id;
                TemplateName = tempName;
                TemplateGroups = (await _templateGroupService.GetGroupsByTemplateIdAsync(TemplateID)).ToList();

                if (TemplateGroups == null)
                {
                    TemplateGroups = new List<TemplateGroupDTO>();
                }

                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while loading groups: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                await _templateGroupService.DeleteGroupAsync(id);

                if (TemplateGroups == null)
                {
                    TemplateGroups = new List<TemplateGroupDTO>();
                }

                return RedirectToPage(new { id = TemplateID, tempName = TemplateName });
            }
            catch (Exception ex)
            {
                Message = $"Error deleting group: {ex.Message}";
                return Page();
            }
        }
    }
}
