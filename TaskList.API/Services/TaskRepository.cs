using System;
using System.Collections.Generic;
using System.Linq;
using TaskList.API.Controllers;
using TaskList.API.Models;

namespace TaskList.API.Services
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _appDbContext;

        public TaskRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task AddTask(Guid userId, Task task)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                Task taskToCreate = new Task
                {
                    TaskId = Guid.NewGuid(),
                    UserId = userId,
                    Description = task.Description
                };

                _appDbContext.Tasks.Add(taskToCreate);
                _appDbContext.SaveChanges();

                return taskToCreate;
            }

            return null;
        }

        public Task UpdateTask(Guid userId, Task task)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                var taskFromRepo = _appDbContext.Tasks.FirstOrDefault(x => x.TaskId == task.TaskId);
                if (taskFromRepo != null)
                {
                    Task updatedTask = new Task
                    {
                        // ==== These should always stay the same when updating ===
                        TaskId = taskFromRepo.TaskId,
                        UserId = taskFromRepo.UserId,
                        // ========================================================

                        Description = task.Description
                    };

                    _appDbContext.Tasks.Update(taskFromRepo);
                    _appDbContext.SaveChanges();

                    return updatedTask;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public List<Task> GetAllTasksForUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return _appDbContext.Tasks.Where(x => x.UserId == userId).ToList();
        }

        public Task GetTaskForUser(Guid userId, Guid taskId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (taskId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(taskId));
            }


            var taskFromRepo = _appDbContext.Tasks.FirstOrDefault(x => x.UserId == userId && x.TaskId == taskId);
            if (taskFromRepo == null)
            {
                return null;
            }

            return taskFromRepo;
        }

        public bool TaskExists(Guid taskId)
        {
            return _appDbContext.Tasks.Any(x => x.TaskId == taskId);
        }

        public bool RemoveTask(Guid taskId)
        {
            var task = _appDbContext.Tasks.FirstOrDefault(x => x.TaskId == taskId);
            if (task != null)
            {
                _appDbContext.Tasks.Remove(task);
                _appDbContext.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
