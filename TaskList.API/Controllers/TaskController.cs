using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskList.API.Models;
using TaskList.API.ResourceParameters;
using TaskList.API.Services;
using TaskList.API.Views;

namespace TaskList.API.Controllers
{
    [ApiController]
    [Route("api/tasks/")]
    public class TaskController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskController(IUserRepository userRepository, ITaskRepository taskRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [HttpGet("{userId:Guid}/{taskId:Guid}")]
        public ActionResult<TaskDto> GetTask(Guid userId, Guid taskId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            Task task = _taskRepository.GetTaskForUser(userId, taskId);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TaskDto>(task));
        }

        [HttpGet("{userId:Guid}")]
        public ActionResult<List<TaskDto>> GetAllTasks(Guid userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            List<Task> tasksFromRepo = _taskRepository.GetAllTasksForUser(userId);
            if (tasksFromRepo == null)
            {
                return NotFound();
            }

            List<TaskDto> tasksToReturn = new List<TaskDto>();
            foreach (var task in tasksFromRepo)
            {
                tasksToReturn.Add(_mapper.Map<TaskDto>(task));
            }

            //return CreatedAtAction(nameof(gettas))
            return Ok(tasksToReturn);
        }

        [HttpPost]
        public ActionResult<TaskDto> CreateTask( 
            [FromBody] TaskResourceParameter taskParameter)
        {
            var userFromRepo = _userRepository.FindUser(taskParameter.UserId);
            if (userFromRepo == null)
            {
                return NotFound();
            }

            Task taskCreated = _taskRepository.AddTask(taskParameter.UserId, _mapper.Map<Task>(taskParameter));
            if (taskCreated == null)
            {
                return BadRequest();    // TODO: Think about what we should return
            }

            return CreatedAtAction(nameof(GetTask),
                new { userId = userFromRepo.Id, taskId = taskCreated.TaskId },
                _mapper.Map<TaskDto>(taskCreated));
        }

        [HttpPatch()]
        public ActionResult<TaskDto> UpdateTask(
            [FromBody] TaskResourceParameter taskParameter)
        {
            if (!_userRepository.UserExists(taskParameter.UserId))
            {
                return NotFound();
            }

            Task taskCreated = _taskRepository.UpdateTask(taskParameter.UserId, _mapper.Map<Task>(taskParameter));
            if (taskCreated == null)
            {
                return BadRequest();    // TODO: Think about what we should return
            }

            return Ok(_mapper.Map<TaskDto>(taskCreated));
        }

        [HttpDelete("{userId}/{taskId}")]
        public ActionResult DeleteTask(Guid userId, Guid taskId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            if (!_taskRepository.TaskExists(taskId))
            {
                return NotFound();
            }

            if (_taskRepository.RemoveTask(taskId))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
