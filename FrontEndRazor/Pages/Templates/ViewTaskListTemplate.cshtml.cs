using BusinessLayer.DTOs.TaskListTemplateDTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndRazor.Pages.Templates
{
    [Authorize(Roles = "Admin")]
    public class ViewTaskListTemplateModel : PageModel
    {
        private readonly ITaskListTemplateService _taskListTemplateService;

        public TaskListTemplateDTO TaskListTemplateDTO { get; set; }
        public string Message { get; private set; }

        public ViewTaskListTemplateModel(ITaskListTemplateService taskListTemplateService)
        {
            _taskListTemplateService = taskListTemplateService;
            Message = string.Empty;
        }

        public async Task<IActionResult> OnGetAsync(int ID)
        {
            try
            {
                TaskListTemplateDTO = await _taskListTemplateService.GetTemplateByIdAsync(ID);

                if (TaskListTemplateDTO == null)
                {
                    Message = "Template not found.";
                    return Page();
                }

                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while retrieving the template: {ex.Message}";
                return Page();
            }
        }
    }
}
