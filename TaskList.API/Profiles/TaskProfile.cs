using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskList.API.Models;
using TaskList.API.ResourceParameters;
using TaskList.API.Views;

namespace TaskList.API.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskDto>();
            CreateMap<TaskDto, Task>();
            CreateMap<TaskResourceParameter, Task>();
        }
    }
}
