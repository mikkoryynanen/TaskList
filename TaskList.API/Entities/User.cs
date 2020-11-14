using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TaskList.API.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
    }
}
