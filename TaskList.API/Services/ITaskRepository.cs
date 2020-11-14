using System;
using System.Collections.Generic;
using System.Linq;
using TaskList.API.Models;

namespace TaskList.API.Services
{
    public interface ITaskRepository
    {
        Task AddTask(Guid userId, Task task);
        Task UpdateTask(Guid userId, Task task);
        bool TaskExists(Guid taskId);
        bool RemoveTask(Guid taskId);
        public Task GetTaskForUser(Guid userId, Guid taskId);
        public List<Task> GetAllTasksForUser(Guid userId);
    }
}
