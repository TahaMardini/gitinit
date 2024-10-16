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
    public class TemplateGroupRepository : ITemplateGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public TemplateGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TemplateGroup>> GetGroupsAsync()
        {
            return await _context.TemplateGroups.ToListAsync();
        }

        public async Task<IEnumerable<TemplateGroup>> GetGroupsByTemplateIdAsync(int TemplateId)
        {
            return await _context.TemplateGroups.Where(g => g.TemplateID == TemplateId).ToListAsync();
        }

        public async Task<TemplateGroup> GetGroupByIdAsync(int Id)
        {
            return await _context.TemplateGroups.FindAsync(Id);
        }

        public async Task<bool> CreateGroupAsync(TemplateGroup templateGroup)
        {
            await _context.TemplateGroups.AddAsync(templateGroup);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateGroupAsync(TemplateGroup templateGroup)
        {
            var group = await _context.TemplateGroups.FindAsync(templateGroup.Id);
            if (group == null)
            {
                return false;
            }

            group.GroupName = templateGroup.GroupName;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGroupAsync(int Id)
        {
            var group = await _context.TemplateGroups.FindAsync(Id);
            if (group == null)
            {
                return false;
            }

            _context.TemplateGroups.Remove(group);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
