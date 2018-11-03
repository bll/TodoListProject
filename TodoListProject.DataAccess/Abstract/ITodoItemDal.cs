using TodoListProject.Core.DataAccess;
using TodoListProject.Entities.Concrete;

namespace TodoListProject.DataAccess.Abstract {
    public interface ITodoItemDal : IEntityRepository<TodoItem> {
    }
}
