using BusinessLayer.DTOs.GroupTaskDTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndRazor.Pages.GroupTasks
{
    [Authorize(Roles = "Admin")]
    public class ViewTaskModel : PageModel
    {
        private readonly IGroupTaskService _groupTaskService;

        public GroupTaskDTO GroupTaskDTO { get; set; }
        public int GroupID { get; set; }
        public string Message { get; set; }

        public ViewTaskModel(IGroupTaskService groupTaskService)
        {
            _groupTaskService = groupTaskService;
            Message = string.Empty;
        }

        public async Task<IActionResult> OnGetAsync(int taskId)
        {
            GroupTaskDTO = await _groupTaskService.GetTaskByIDAsync(taskId);
            if (GroupTaskDTO == null)
            {
                Message = "Task not found.";
                return Page();
            }

            GroupID = GroupTaskDTO.GroupID;

            return Page();
        }
    }
}
