using BusinessLayer.DTOs.TaskListTemplateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ITaskListTemplateService
    {
        Task<IEnumerable<TaskListTemplateDTO>> GetAllTemplatesAsync();
        Task<TaskListTemplateDTO> GetTemplateByIdAsync(int ID);
        Task<TaskListTemplateDTO> AddTemplateAsync(AddTaskListTemplateDTO addTaskListTemplateDTO);
        Task<TaskListTemplateDTO> UpdateTemplateAsync(UpdateTaskListTemplateDTO updateTaskListTemplateDTO);
        Task<bool> DeleteTemplateAsync(int ID);
    }
}
