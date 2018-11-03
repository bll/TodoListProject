using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListProject.Core.Caching {
    public static class CacheKeys {
        public const string AllTodos = "cachekey_alltodos";
        public const string TodoById = "cachekey_todo_{0}";
        public const string TodoBySearchText = "cachekey_todo_searchtext_{0}";
        public const string UnCompletedTodoById = "cachekey_todo_uncomplated_{0}";

    }
}
