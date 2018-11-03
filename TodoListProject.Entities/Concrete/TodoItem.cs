using System;
using TodoListProject.Core.Entities;

namespace TodoListProject.Entities.Concrete {
    public class TodoItem : IEntity {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? CompleteDate { get; set; }
    }

}
