using BusinessLayer.DTOs.TaskListTemplateDTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndRazor.Pages.Templates
{
    [Authorize(Roles = "Admin")]
    public class UpdateTaskListTemplateModel : PageModel
    {
        private readonly ITaskListTemplateService _taskListTemplateService;

        [BindProperty]
        public UpdateTaskListTemplateDTO UpdateTaskListTemplateDTO { get; set; }
        public string Message { get; private set; }

        public UpdateTaskListTemplateModel(ITaskListTemplateService taskListTemplateService)
        {
            _taskListTemplateService = taskListTemplateService;
            Message = string.Empty;
        }

        public async Task<IActionResult> OnGetAsync(int ID)
        {
            try
            {
                var template = await _taskListTemplateService.GetTemplateByIdAsync(ID);

                if (template == null)
                {
                    Message = "Template not found.";
                    return Page();
                }

                UpdateTaskListTemplateDTO = new UpdateTaskListTemplateDTO
                {
                    Id = template.Id,
                    TempName = template.TempName
                };

                return Page();
            }
            catch (Exception ex)
            {
                Message = $"An error occurred while retrieving the template: {ex.Message}";
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
                await _taskListTemplateService.UpdateTemplateAsync(UpdateTaskListTemplateDTO);
                return RedirectToPage("/Templates/TaskListTemplate");
            }
            catch (Exception ex)
            {
                Message = $"Error updating template: {ex.Message}";
                return Page();
            }
        }
    }
}
