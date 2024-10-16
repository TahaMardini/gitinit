using AccessLayerDLL.Interfaces;
using BusinessLayer.DTOs.GroupTaskDTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace FrontEndRazor.Pages.GroupTasks
{
    [Authorize(Roles = "Admin")]
    public class CreateTaskModel : PageModel
    {
        private readonly IGroupTaskService _groupTaskService;

        [BindProperty]
        public AddGroupTaskDTO AddGroupTaskDTO { get; set; } = new AddGroupTaskDTO();
        public IEnumerable<SelectListItem> DependancyTasks { get; set; } = new List<SelectListItem>();

        [BindProperty(SupportsGet = true)]
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string Message { get; set; }

        public CreateTaskModel(IGroupTaskService groupTaskService)
        {
            _groupTaskService = groupTaskService;
            Message = string.Empty;
        }

        public async Task<IActionResult> OnGetAsync(int groupId, string groupName)
        {
            GroupID = groupId;
            GroupName = groupName;

            var tasks = await _groupTaskService.GetTasksByGroupIDAsync(groupId);

            DependancyTasks = tasks.Select(t => new SelectListItem
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

                DependancyTasks = tasks.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.TaskName
                }).ToList();

                return Page();
            }

            AddGroupTaskDTO.GroupID = GroupID;

            await _groupTaskService.CreateTaskAsync(AddGroupTaskDTO);
            return RedirectToPage("/GroupTasks/GroupTasks", new { groupId = AddGroupTaskDTO.GroupID });
        }
    }
}
