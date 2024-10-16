using AccessLayerDLL.Interfaces;
using AccessLayerDLL.Models;
using BusinessLayer.DTOs.TaskListTemplateDTOs;
using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TaskListTemplateService : ITaskListTemplateService
    {
        private readonly ITaskListTemplateRepository _repository;

        public TaskListTemplateService(ITaskListTemplateRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskListTemplateDTO>> GetAllTemplatesAsync()
        {
            try
            {
                var templates = await _repository.GetAllTemplatesAsync();

                return templates.Select(x => new TaskListTemplateDTO
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    TempName = x.TempName
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<TaskListTemplateDTO> GetTemplateByIdAsync(int ID)
        {
            try
            {
                var template = await _repository.GetTemplateByIdAsync(ID);
                if (template == null)
                {
                    throw new Exception("Template not found");
                }

                return new TaskListTemplateDTO
                {
                    Id = template.Id,
                    CreatedAt = template.CreatedAt,
                    TempName = template.TempName
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<TaskListTemplateDTO> AddTemplateAsync(AddTaskListTemplateDTO addTaskListTemplateDTO)
        {
            try
            {
                var template = new TaskListTemplate
                {
                    TempName = addTaskListTemplateDTO.TempName
                };

                var newTemp = await _repository.AddTemplateAsync(template);

                return new TaskListTemplateDTO
                {
                    Id = newTemp.Id,
                    CreatedAt = newTemp.CreatedAt,
                    TempName = newTemp.TempName
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<TaskListTemplateDTO> UpdateTemplateAsync(UpdateTaskListTemplateDTO updateTaskListTemplateDTO)
        {
            try
            {
                var template = await _repository.GetTemplateByIdAsync(updateTaskListTemplateDTO.Id);
                if (template == null)
                {
                    throw new Exception("Template you are trying to update is not found");
                }

                template.TempName = updateTaskListTemplateDTO.TempName;

                var updated = await _repository.UpdateTemplateAsync(template);

                return new TaskListTemplateDTO
                {
                    Id = updated.Id,
                    CreatedAt = updated.CreatedAt,
                    TempName = updated.TempName
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> DeleteTemplateAsync(int ID)
        {
            try
            {
                var template = await _repository.GetTemplateByIdAsync(ID);
                if (template == null)
                {
                    throw new Exception("Template you are trying to delete do not exist");
                }

                await _repository.DeleteTemplateByIdAsync(ID);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
