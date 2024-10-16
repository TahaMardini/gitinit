using AccessLayerDLL.Models;
using BusinessLayer.DTOs.GroupTaskDTOs;
using BusinessLayer.DTOs.TemplateGroupDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IGroupTaskService
    {
        Task<IEnumerable<GroupTaskDTO>> GetAllTasksAsync();
        Task<IEnumerable<GroupTaskDTO>> GetTasksByGroupIDAsync(int groupId);
        Task<GroupTaskDTO> GetTaskByIDAsync(int Id);
        Task<bool> CreateTaskAsync(AddGroupTaskDTO addGroupTaskDTO);
        Task<bool> UpdateTaskAsync(UpdateGroupTaskDTO updateGroupTaskDTO);
        Task<bool> DeleteTaskAsync(int Id);
        Task<TemplateGroupDTO> GetGroupByIdAsync(int Id);
    }
}
