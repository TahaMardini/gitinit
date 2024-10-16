using BusinessLayer.DTOs.TemplateGroupDTO;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace FrontEndRazor.Pages.TemplateGroups
{
    [Authorize(Roles = "Admin")]
    public class ViewGroupModel : PageModel
    {
        private readonly ITemplateGroupService _templateGroupService;

        public TemplateGroupDTO TemplateGroupDTO { get; set; }
        public string Message { get; private set; }

        public ViewGroupModel(ITemplateGroupService templateGroupService)
        {
            _templateGroupService = templateGroupService;
            Message = string.Empty;
        }

        public async Task<IActionResult> OnGetAsync(int Id)
        {
            try
            {
                TemplateGroupDTO = await _templateGroupService.GetGroupByIdAsync(Id);
                if (TemplateGroupDTO == null)
                {
                    Message = "Group not found.";
                    return Page();
                }

                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while retrieving the group: {ex.Message}";
                return Page();
            }
        }
    }
}
