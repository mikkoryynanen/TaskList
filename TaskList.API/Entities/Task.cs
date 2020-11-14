using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskList.API.Models
{
    public class Task 
    {
        [Key]
        public Guid TaskId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(512)]
        public string Description { get; set; }
    }
}
