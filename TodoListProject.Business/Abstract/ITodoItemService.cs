using System;
using System.Collections.Generic;
using System.Text;
using TodoListProject.Entities.Concrete;

namespace TodoListProject.Business.Abstract {
    public interface ITodoItemService {
        List<TodoItem> GetAll();
        List<TodoItem> GetItemsBySearchText(string searchText);
        TodoItem GetById(int todoItemId);
        TodoItem GetUnCompletedById(int todoItemId);
        void Add(TodoItem todoItem);
        void Update(TodoItem todoItem);
        void Delete(TodoItem todoItem);
    }
}
