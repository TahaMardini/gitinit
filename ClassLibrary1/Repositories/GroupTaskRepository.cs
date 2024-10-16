using AccessLayerDLL.Data;
using AccessLayerDLL.Interfaces;
using AccessLayerDLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLayerDLL.Repositories
{
    public class GroupTaskRepository : IGroupTaskRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupTask>> GetAllTasksAsync()
        {
            return await _context.GroupTasks.ToListAsync();
        }

        public async Task<IEnumerable<GroupTask>> GetTasksByGroupIDAsync(int groupId)
        {
            return await _context.GroupTasks.Where(t => t.GroupID == groupId).ToListAsync();
        }

        public async Task<GroupTask> GetTaskByIDAsync(int Id)
        {
            return await _context.GroupTasks.FindAsync(Id);
        }

        public async Task<bool> CreateTaskAsync(GroupTask groupTask)
        {
            await _context.GroupTasks.AddAsync(groupTask);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateTaskAsync(GroupTask groupTask)
        {
            var task = await _context.GroupTasks.FindAsync(groupTask.Id);
            if (task == null)
            {
                return false;
            }

            task.TaskName = groupTask.TaskName;
            task.TaskDescription = groupTask.TaskDescription;
            task.DependancyTaskID = groupTask.DependancyTaskID;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTaskAsync(int Id)
        {
            var task = await _context.GroupTasks.FindAsync(Id);
            if(task == null)
            {
                return false;
            }

            _context.GroupTasks.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
