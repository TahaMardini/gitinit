using BusinessLayer.DTOs.TemplateGroupDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ITemplateGroupService
    {
        Task<IEnumerable<TemplateGroupDTO>> GetGroupsAsync();
        Task<IEnumerable<TemplateGroupDTO>> GetGroupsByTemplateIdAsync(int TemplateId);
        Task<TemplateGroupDTO> GetGroupByIdAsync(int Id);
        Task<bool> CreateGroupAsync(AddTemplateGroupDTO addTemplateGroupDTO);
        Task<bool> UpdateGroupAsync(UpdateTemplateGroupDTO updateTemplateGroupDTO);
        Task<bool> DeleteGroupAsync(int Id);
    }
}
