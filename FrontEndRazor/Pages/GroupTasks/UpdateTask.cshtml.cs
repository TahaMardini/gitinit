using BusinessLayer.DTOs.GroupTaskDTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndRazor.Pages.GroupTasks
{
    public class UpdateTaskModel : PageModel
    {
        private readonly IGroupTaskService _groupTaskService;

        [BindProperty]
        public UpdateGroupTaskDTO UpdateGroupTaskDTO { get; set; }

        public IEnumerable<SelectListItem> DependancyTasks { get; set; }

        [BindProperty(SupportsGet = true)]
        public int GroupID { get; set; }

        [BindProperty(SupportsGet = true)]
        public int TaskID { get; set; }
        public string Message { get; set; }

        public UpdateTaskModel(IGroupTaskService groupTaskService)
        {
            _groupTaskService = groupTaskService;
            Message = string.Empty;
        }

        public async Task<IActionResult> OnGetAsync(int taskId)
        {
            TaskID = taskId;

            var task = await _groupTaskService.GetTaskByIDAsync(taskId);
            if (task == null)
            {
                Message = "Task not found or was deleted";
                return RedirectToPage("/GroupTasks/GroupTasks", new { groupId = GroupID });
            }

            GroupID = task.GroupID;

            UpdateGroupTaskDTO = new UpdateGroupTaskDTO
            {
                Id = task.Id,
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                DependancyTaskID = task.DependancyTaskID,
            };

            var tasks = await _groupTaskService.GetTasksByGroupIDAsync(task.GroupID);
            DependancyTasks = tasks
                .Where(t => t.Id !=  TaskID)
                .Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.TaskName
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var tasks = await _groupTaskService.GetTasksByGroupIDAsync(GroupID);
                DependancyTasks = tasks
                    .Where(t=> t.Id != TaskID)
                    .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.TaskName
                }).ToList();

                return Page();
            }

            await _groupTaskService.UpdateTaskAsync(UpdateGroupTaskDTO);
            return RedirectToPage("/GroupTasks/GroupTasks", new { groupId = GroupID });
        }
    }
}
