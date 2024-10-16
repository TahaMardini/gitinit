using AccessLayerDLL.Data;
using AccessLayerDLL.Interfaces;
using AccessLayerDLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccessLayerDLL.Repositories
{
    public class TaskListTemplateRepository : ITaskListTemplateRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskListTemplateRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TaskListTemplate>> GetAllTemplatesAsync()
        {
            return await _context.TaskListTemplates.ToListAsync();
        }


        public async Task<TaskListTemplate> GetTemplateByIdAsync(int ID)
        {
            return await _context.TaskListTemplates.FindAsync(ID);
        }


        public async Task<TaskListTemplate> AddTemplateAsync(TaskListTemplate taskListTemplate)
        {
            await _context.TaskListTemplates.AddAsync(taskListTemplate);
            await _context.SaveChangesAsync();
            return taskListTemplate;
        }


        public async Task<TaskListTemplate> UpdateTemplateAsync(TaskListTemplate taskListTemplate)
        {
            var template = await _context.TaskListTemplates.FindAsync(taskListTemplate.Id);
            if (template == null)
            {
                return null;
            }

            template.TempName = taskListTemplate.TempName;

            await _context.SaveChangesAsync();
            return template;
        }


        public async Task<bool> DeleteTemplateByIdAsync(int ID)
        {
            var template = await _context.TaskListTemplates.FindAsync(ID);
            if (template == null)
            {
                return false;
            }
            
            _context.TaskListTemplates.Remove(template);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
