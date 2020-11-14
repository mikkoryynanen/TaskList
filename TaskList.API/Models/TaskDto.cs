using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskList.API.Models;

namespace TaskList.API.Views
{
    public class TaskDto
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
    }
}
