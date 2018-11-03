using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListProject.WebAPI.Models
{
    public class TodoItemModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}
