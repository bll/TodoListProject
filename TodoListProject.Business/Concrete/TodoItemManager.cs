using System;
using System.Collections.Generic;
using System.Text;
using TodoListProject.Business.Abstract;
using TodoListProject.Core.Caching;
using TodoListProject.Core.Caching.Microsoft;
using TodoListProject.DataAccess.Abstract;
using TodoListProject.Entities.Concrete;

namespace TodoListProject.Business.Concrete {
    public class TodoItemManager : ITodoItemService {
        private ITodoItemDal _todoItemDal;
        private ICacheManager _cacheManager;
        public TodoItemManager(ITodoItemDal todoItemDal, ICacheManager cacheManager) {
            _todoItemDal = todoItemDal;
            _cacheManager = cacheManager;
        }

        public void Add(TodoItem todoItem) {
            _todoItemDal.Add(todoItem);
            _cacheManager.Remove(CacheKeys.AllTodos);
        }

        public void Delete(TodoItem todoItem) {
            _todoItemDal.Delete(todoItem);
            DeleteAllCache(todoItem.Id);
        }

        public List<TodoItem> GetAll() {
            var response = _cacheManager.Get<List<TodoItem>>(CacheKeys.AllTodos);
            if (response != null && response != default(List<TodoItem>)) {
                return response;
            }

            response = _todoItemDal.GetList();
            _cacheManager.Add(CacheKeys.AllTodos, response, 60);
            return response;
        }

        public TodoItem GetById(int todoItemId) {
            var response = _cacheManager.Get<TodoItem>(string.Format(CacheKeys.TodoById, todoItemId));
            if (response != null && response != default(TodoItem)) {
                return response;
            }

            response = _todoItemDal.Get(t => t.Id == todoItemId);
            _cacheManager.Add(CacheKeys.TodoById, response, 60);
            return response;
        }

        public List<TodoItem> GetItemsBySearchText(string searchText) {
            var searchKey = searchText.ToLower().Trim().Replace(" ", "_");
            var response = _cacheManager.Get<List<TodoItem>>(string.Format(CacheKeys.TodoBySearchText, searchKey));
            if (response != null && response != default(List<TodoItem>)) {
                return response;
            }

            response = _todoItemDal.GetList(s => s.Title.Contains(searchText));
            _cacheManager.Add(CacheKeys.TodoBySearchText, response, 60);

            return response;
        }

        public TodoItem GetUnCompletedById(int todoItemId) {

            var response = _cacheManager.Get<TodoItem>(string.Format(CacheKeys.UnCompletedTodoById, todoItemId));
            if (response != null && response != default(TodoItem)) {
                return response;
            }
            response = _todoItemDal.Get(t => t.Id == todoItemId && !t.IsComplete);
            _cacheManager.Add(CacheKeys.UnCompletedTodoById, response, 60);

            return response;
        }

        public void Update(TodoItem todoItem) {
            if (todoItem.IsComplete) {
                todoItem.CompleteDate = DateTime.Now;
            }

            _todoItemDal.Update(todoItem);
            DeleteAllCache(todoItem.Id);
        }

        private void DeleteAllCache(int? Id = null) {
            _cacheManager.Remove(CacheKeys.AllTodos);

            if (Id != null) {
                _cacheManager.Remove(string.Format(CacheKeys.UnCompletedTodoById, Id));
                _cacheManager.Remove(string.Format(CacheKeys.TodoById, Id));
            }

        }
    }
}
