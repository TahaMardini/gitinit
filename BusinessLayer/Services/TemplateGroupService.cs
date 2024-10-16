using AccessLayerDLL.Interfaces;
using AccessLayerDLL.Models;
using BusinessLayer.DTOs.TemplateGroupDTO;
using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TemplateGroupService : ITemplateGroupService
    {
        private readonly ITemplateGroupRepository _repository;
        private readonly ITaskListTemplateService _templateService;

        public TemplateGroupService(ITemplateGroupRepository repository, ITaskListTemplateService taskListTemplateService)
        {
            _repository = repository;
            _templateService = taskListTemplateService;
        }

        public async Task<IEnumerable<TemplateGroupDTO>> GetGroupsAsync()
        {
            try
            {
                var groups = await _repository.GetGroupsAsync();
                if (groups == null)
                {
                    throw new Exception("Error retrieving groups.");
                }

                var groupDTOs = new List<TemplateGroupDTO>();

                foreach(var g in groups)
                {
                    var template = await _templateService.GetTemplateByIdAsync(g.TemplateID);

                    string tempName = template.TempName;

                    groupDTOs.Add(new TemplateGroupDTO
                    {
                        Id = g.Id,
                        CreatedAt = g.CreatedAt,
                        GroupName = g.GroupName,
                        TemplateID = g.TemplateID,
                        TemplateName = tempName
                    });
                }

                return groupDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving groups.", ex);
            }
        }

        public async Task<IEnumerable<TemplateGroupDTO>> GetGroupsByTemplateIdAsync(int templateId)
        {
            try
            {
                var groups = await _repository.GetGroupsByTemplateIdAsync(templateId);
                if (groups == null)
                {
                    throw new Exception("Error retrieving template groups.");
                }

                var groupDTOs = new List<TemplateGroupDTO>();

                foreach (var g in groups)
                {
                    var template = await _templateService.GetTemplateByIdAsync(g.TemplateID);
                    string tempName = template.TempName;

                    groupDTOs.Add(new TemplateGroupDTO
                    {
                        Id = g.Id,
                        CreatedAt = g.CreatedAt,
                        GroupName = g.GroupName,
                        TemplateID = g.TemplateID,
                        TemplateName = tempName
                    });
                }

                return groupDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving template groups for TemplateId: {templateId}", ex);
            }
        }

        public async Task<TemplateGroupDTO> GetGroupByIdAsync(int Id)
        {
            try
            {
                var group = await _repository.GetGroupByIdAsync(Id);
                if (group == null)
                {
                    throw new Exception("Error retrieving template group.");
                }

                var template = await _templateService.GetTemplateByIdAsync(group.TemplateID);
                string tempName = template.TempName;

                var groupDTO = new TemplateGroupDTO
                {
                    Id = group.Id,
                    CreatedAt = group.CreatedAt,
                    GroupName = group.GroupName,
                    TemplateID = group.TemplateID,
                    TemplateName = tempName
                };

                return groupDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving template group with Id: {Id}", ex);
            }
        }

        public async Task<bool> CreateGroupAsync(AddTemplateGroupDTO addTemplateGroupDTO)
        {
            try
            {
                var templateGroup = new TemplateGroup
                {
                    GroupName = addTemplateGroupDTO.GroupName,
                    TemplateID = addTemplateGroupDTO.TemplateID,
                };

                return await _repository.CreateGroupAsync(templateGroup);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating template group.", ex);
            }
        }

        public async Task<bool> UpdateGroupAsync(UpdateTemplateGroupDTO updateTemplateGroupDTO)
        {
            try
            {
                var group = await _repository.GetGroupByIdAsync(updateTemplateGroupDTO.Id);
                if (group == null)
                {
                    throw new Exception("Template you are trying to update is not found");
                }

                group.GroupName = updateTemplateGroupDTO.GroupName;
                return await _repository.UpdateGroupAsync(group);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating template group with Id: {updateTemplateGroupDTO.Id}", ex);
            }
        }

        public async Task<bool> DeleteGroupAsync(int Id)
        {
            try
            {
                var group = await _repository.GetGroupByIdAsync(Id);
                if (group == null)
                {
                    throw new Exception("Group you are trying to delete do not exist");
                }

                return await _repository.DeleteGroupAsync(Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting template group with Id: {Id}", ex);
            }
        }
    }
}
