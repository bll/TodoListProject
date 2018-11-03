using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListProject.MvcWebUI.Models.ViewModel;

namespace TodoListProject.MvcWebUI.Models.PageModel
{
    public class TodoListPageModel {
        public List<TodoItemViewModel> CompletedTodoItems { get; set; }
        public List<TodoItemViewModel> UnCompletedTodoItems { get; set; }
        public TodoItemViewModel TodoItem { get; set; }
        public SearchViewModel Search { get; set; }
    }
}
