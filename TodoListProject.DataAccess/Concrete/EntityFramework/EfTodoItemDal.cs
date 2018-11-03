using System;
using System.Collections.Generic;
using System.Text;
using TodoListProject.Core.DataAccess.EntityFramework;
using TodoListProject.DataAccess.Abstract;
using TodoListProject.Entities.Concrete;

namespace TodoListProject.DataAccess.Concrete.EntityFramework {
    public class EfTodoItemDal : EfEntityRepositoryBase<TodoItem, TodoContext>, ITodoItemDal {
    }
}
