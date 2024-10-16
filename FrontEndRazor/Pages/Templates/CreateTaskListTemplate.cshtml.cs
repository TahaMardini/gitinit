using BusinessLayer.DTOs.TaskListTemplateDTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndRazor.Pages.Templates
{
    [Authorize(Roles = "Admin")]
    public class CreateTaskListTemplateModel : PageModel
    {
        private readonly ITaskListTemplateService _taskListTemplateService;

        [BindProperty]
        public AddTaskListTemplateDTO AddTaskListTemplateDTO { get; set; }
        public string Message { get; private set; }

        public CreateTaskListTemplateModel(ITaskListTemplateService taskListTemplateService)
        {
            _taskListTemplateService = taskListTemplateService;
            Message = string.Empty;
        }

        public void OnGet()
        {

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
                await _taskListTemplateService.AddTemplateAsync(AddTaskListTemplateDTO);
                return RedirectToPage("/Templates/TaskListTemplate");
            }
            catch (Exception ex)
            {
                Message = $"Error creating template: {ex.Message}";
                return Page();
            }
        }
    }
}
