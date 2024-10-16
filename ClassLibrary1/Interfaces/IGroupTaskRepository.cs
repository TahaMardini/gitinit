using AccessLayerDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLayerDLL.Interfaces
{
    public interface IGroupTaskRepository
    {
        Task<IEnumerable<GroupTask>> GetAllTasksAsync();
        Task<IEnumerable<GroupTask>> GetTasksByGroupIDAsync(int groupId);
        Task<GroupTask> GetTaskByIDAsync(int Id);
        Task<bool> CreateTaskAsync(GroupTask groupTask);
        Task<bool> UpdateTaskAsync(GroupTask groupTask);
        Task<bool> DeleteTaskAsync(int Id);
    }
}
