using AccessLayerDLL.Interfaces;
using BusinessLayer.DTOs.TaskListTemplateDTOs;
using BusinessLayer.DTOs.TemplateGroupDTO;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndRazor.Pages.Templates
{
    [Authorize(Roles = "Admin")]
    public class TaskListTemplateModel : PageModel
    {
        private readonly ITaskListTemplateService _taskListTemplateService;

        public IEnumerable<TaskListTemplateDTO> Templates { get; set; }
        public string Message { get; private set; }


        public TaskListTemplateModel(ITaskListTemplateService taskListTemplateService)
        {
            _taskListTemplateService = taskListTemplateService;
            Message = string.Empty;
        }


        public async Task<IActionResult> OnGet()
        {
            try
            {
                Templates = await _taskListTemplateService.GetAllTemplatesAsync();
                if (Templates == null)
                {
                    Templates = new List<TaskListTemplateDTO>();
                }
                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while loading templates: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDelete(int ID)
        {
            try
            {
                await _taskListTemplateService.DeleteTemplateAsync(ID);
                if (Templates == null)
                {
                    Templates = new List<TaskListTemplateDTO>();
                }
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Message = $"Error deleting template: {ex.Message}";
                return Page();
            }
        }
    }
}
