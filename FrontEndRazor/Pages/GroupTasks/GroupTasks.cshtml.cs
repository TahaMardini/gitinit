using BusinessLayer.DTOs.GroupTaskDTOs;
using BusinessLayer.DTOs.TemplateGroupDTO;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndRazor.Pages.GroupTasks
{
    [Authorize(Roles = "Admin")]
    public class GroupTasksModel : PageModel
    {
        private readonly IGroupTaskService _groupTaskService;

        public IEnumerable<GroupTaskDTO> GroupTasks { get; set; }

        [BindProperty]
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; }

        public GroupTasksModel(IGroupTaskService groupTaskService)
        {
            _groupTaskService = groupTaskService;
            Message = string.Empty;
        }

        public async Task<IActionResult> OnGetAsync(int groupId)
        {
            var group = await _groupTaskService.GetGroupByIdAsync(groupId);
            if (group == null)
            {
                Message = "Group not found";
                return Page();
            }

            GroupID = groupId;
            GroupName = group.GroupName;
            CreatedAt = group.CreatedAt;


            GroupTasks = await _groupTaskService.GetTasksByGroupIDAsync(groupId);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int taskId)
        {
            try
            {
                if (GroupID == 0)
                {
                    Message = "GroupID is not properly set.";
                    return Page();
                }

                await _groupTaskService.DeleteTaskAsync(taskId);
                if (GroupTasks == null)
                {
                    GroupTasks = new List<GroupTaskDTO>();
                }

                return RedirectToPage(new { groupId = GroupID });
            }
            catch (Exception ex)
            {
                Message = $"Error deleting task: {ex.Message}";
                return Page();
            }
        }
    }
}
