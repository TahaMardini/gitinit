using AccessLayerDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLayerDLL.Interfaces
{
    public interface ITaskListTemplateRepository
    {
        Task<IEnumerable<TaskListTemplate>> GetAllTemplatesAsync();
        Task<TaskListTemplate> GetTemplateByIdAsync(int ID);
        Task<TaskListTemplate> AddTemplateAsync(TaskListTemplate taskListTemplate);
        Task<TaskListTemplate> UpdateTemplateAsync(TaskListTemplate taskListTemplate);
        Task<bool> DeleteTemplateByIdAsync(int ID);
    }
}
