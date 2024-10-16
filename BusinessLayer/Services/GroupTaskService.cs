using AccessLayerDLL.Interfaces;
using AccessLayerDLL.Models;
using BusinessLayer.DTOs.GroupTaskDTOs;
using BusinessLayer.DTOs.TemplateGroupDTO;
using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class GroupTaskService : IGroupTaskService
    {
        private readonly IGroupTaskRepository _repository;
        private readonly ITemplateGroupService _groupService;

        public GroupTaskService(IGroupTaskRepository groupTaskRepository, ITemplateGroupService groupService)
        {
            _repository = groupTaskRepository;
            _groupService = groupService;
        }

        public async Task<IEnumerable<GroupTaskDTO>> GetAllTasksAsync()
        {
            try
            {
                var tasks = await _repository.GetAllTasksAsync();
                if (tasks == null)
                {
                    throw new Exception("No tasks available yet.");
                }

                var taskDTOs = new List<GroupTaskDTO>();

                foreach (var task in tasks)
                {
                    var taskDTO = new GroupTaskDTO
                    {
                        Id = task.Id,
                        CreatedAt = task.CreatedAt,
                        TaskName = task.TaskName,
                        TaskDescription = task.TaskDescription,
                        GroupID = task.GroupID,
                        DependancyTaskID = task.DependancyTaskID,
                    };

                    var group = await GetGroupByIdAsync(task.GroupID);
                    if (group != null)
                    {
                        taskDTO.GroupName = group.GroupName;
                    }

                    if (task.DependancyTaskID.HasValue)
                    {
                        var dependancyTask = await _repository.GetTaskByIDAsync(task.DependancyTaskID.Value);
                        taskDTO.DependancyTaskName = dependancyTask.TaskName;
                    }
                    
                    taskDTOs.Add(taskDTO);
                }
                return taskDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving tasks: ", ex);
            }
        }

        public async Task<IEnumerable<GroupTaskDTO>> GetTasksByGroupIDAsync(int groupId)
        {
            try
            {
                var tasks = await _repository.GetTasksByGroupIDAsync(groupId);
                if (tasks == null)
                {
                    throw new Exception("There are no tasks from this group yet.");
                }

                var taskDTOs = new List<GroupTaskDTO>();

                foreach (var task in tasks)
                {
                    var taskDTO = new GroupTaskDTO
                    {
                        Id = task.Id,
                        CreatedAt = task.CreatedAt,
                        TaskName = task.TaskName,
                        TaskDescription = task.TaskDescription,
                        GroupID = task.GroupID,
                        DependancyTaskID = task.DependancyTaskID,
                    };

                    var group = await GetGroupByIdAsync(task.GroupID);
                    taskDTO.GroupName = group.GroupName;

                    if (task.DependancyTaskID.HasValue)
                    {
                        var dependancyTask = await _repository.GetTaskByIDAsync(task.DependancyTaskID.Value);
                        taskDTO.DependancyTaskName = dependancyTask.TaskName;
                    }

                    taskDTOs.Add(taskDTO);
                }

                return taskDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving tasks of group with ID: {groupId}", ex);
            }
        }

        public async Task<GroupTaskDTO> GetTaskByIDAsync(int Id)
        {
            try
            {
                var task = await _repository.GetTaskByIDAsync(Id);
                if (task == null)
                {
                    throw new Exception("Task not found.");
                }

                var taskDTO = new GroupTaskDTO
                {
                    Id = task.Id,
                    CreatedAt = task.CreatedAt,
                    TaskName = task.TaskName,
                    TaskDescription = task.TaskDescription,
                    GroupID = task.GroupID,
                    DependancyTaskID = task.DependancyTaskID,
                };

                var group = await GetGroupByIdAsync(task.GroupID);
                taskDTO.GroupName = group.GroupName;

                if (task.DependancyTaskID.HasValue)
                {
                    var dependancyTask = await _repository.GetTaskByIDAsync(task.DependancyTaskID.Value);
                    taskDTO.DependancyTaskName = dependancyTask.TaskName;
                }

                return taskDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving task with ID: {Id}", ex);
            }
        }

        public async Task<bool> CreateTaskAsync(AddGroupTaskDTO addGroupTaskDTO)
        {
            try
            {
                var groupTask = new GroupTask
                {
                    TaskName = addGroupTaskDTO.TaskName,
                    TaskDescription = addGroupTaskDTO.TaskDescription,
                    GroupID = addGroupTaskDTO.GroupID,
                    DependancyTaskID = addGroupTaskDTO.DependancyTaskID
                };

                return await _repository.CreateTaskAsync(groupTask);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating task: ", ex);
            }
        }

        public async Task<bool> UpdateTaskAsync(UpdateGroupTaskDTO updateGroupTaskDTO)
        {
            try
            {
                var task = await _repository.GetTaskByIDAsync(updateGroupTaskDTO.Id);
                if (task == null)
                {
                    throw new Exception("Task not available");
                }

                var taskGroup = new GroupTask
                {
                    Id = updateGroupTaskDTO.Id,
                    TaskName = updateGroupTaskDTO.TaskName,
                    TaskDescription = updateGroupTaskDTO.TaskDescription,
                    DependancyTaskID = updateGroupTaskDTO.DependancyTaskID
                };

                return await _repository.UpdateTaskAsync(taskGroup);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update task with id: {updateGroupTaskDTO.Id}", ex);
            }
        }

        public async Task<bool> DeleteTaskAsync(int Id)
        {
            try
            {
                var task = await _repository.GetTaskByIDAsync(Id);
                if (task == null)
                {
                    throw new Exception("Task not found");
                }

                return await _repository.DeleteTaskAsync(Id);
            }
            catch (Exception ex)
            {

                throw new Exception($"Error deleting task with Id: {Id}", ex);
            }
        }

        public async Task<TemplateGroupDTO> GetGroupByIdAsync(int Id)
        {
            return await _groupService.GetGroupByIdAsync(Id);
        }
    }
}
