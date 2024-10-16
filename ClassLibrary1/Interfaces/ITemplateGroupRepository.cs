using AccessLayerDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLayerDLL.Interfaces
{
    public interface ITemplateGroupRepository
    {
        Task<IEnumerable<TemplateGroup>> GetGroupsAsync();
        Task<IEnumerable<TemplateGroup>> GetGroupsByTemplateIdAsync(int TemplateId);
        Task<TemplateGroup> GetGroupByIdAsync(int Id);
        Task<bool> CreateGroupAsync(TemplateGroup templateGroup);
        Task<bool> UpdateGroupAsync(TemplateGroup templateGroup);
        Task<bool> DeleteGroupAsync(int Id);
    }
}
