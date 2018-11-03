using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TodoListProject.Business.Abstract;
using TodoListProject.Entities.Concrete;
using TodoListProject.MvcWebUI.Models;
using TodoListProject.MvcWebUI.Models.PageModel;
using TodoListProject.MvcWebUI.Models.ViewModel;
namespace TodoListProject.MvcWebUI.Controllers {
    public class HomeController : Controller {

        private ITodoItemService _todoItemService;
        private IMapper _mapper;
        public HomeController(ITodoItemService todoItemService, IMapper mapper) {
            _todoItemService = todoItemService;
            _mapper = mapper;
        }

        public IActionResult Index() {

            var todoList = _todoItemService.GetAll();


            TodoListPageModel viewModel = new TodoListPageModel {
                CompletedTodoItems = _mapper.Map<List<TodoItemViewModel>>(todoList.Where(t => t.IsComplete).OrderByDescending(t => t.CompleteDate)),
                UnCompletedTodoItems = _mapper.Map<List<TodoItemViewModel>>(todoList.Where(t => !t.IsComplete))

            };
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(TodoItemViewModel model) {

            if (ModelState.IsValid) {
                _todoItemService.Add(_mapper.Map<TodoItem>(model));
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int Id) {

            if (Id == default(int)) {
                return NotFound();
            }

            var todoItem = _todoItemService.GetUnCompletedById(Id);

            if (todoItem == default(TodoItem)) {
                return NotFound();
            }

            TodoUpdatePageModel viewModel = new TodoUpdatePageModel {
                TodoItem = _mapper.Map<TodoItemViewModel>(todoItem)
            };

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Update(TodoItemViewModel model) {
            if (ModelState.IsValid) {
                _todoItemService.Update(_mapper.Map<TodoItem>(model));
            }

            return RedirectToAction("Index");
        }

        public IActionResult Complete(int Id) {

            if (Id == default(int)) {
                return NotFound();
            }

            var todoItem = _todoItemService.GetUnCompletedById(Id);

            if (todoItem == default(TodoItem)) {
                return NotFound();
            }

            todoItem.IsComplete = true;

            _todoItemService.Update(todoItem);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id) {

            if (Id == default(int)) {
                return NotFound();
            }

            var todoItem = _todoItemService.GetById(Id);

            if (todoItem == default(TodoItem)) {
                return NotFound();
            }

            _todoItemService.Delete(todoItem);

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Search(SearchViewModel model) {
            if (!ModelState.IsValid) {
                return RedirectToAction("Index");
            }

            var todoList = _todoItemService.GetItemsBySearchText(model.SearchText);

            SearchListPageModel viewModel = new SearchListPageModel {
                TodoItems = _mapper.Map<List<TodoItemViewModel>>(todoList)
            };
            return View(viewModel);

        }

        public IActionResult Detail(int Id) {

            if (Id == default(int)) {
                return NotFound();
            }

            var todoItem = _todoItemService.GetById(Id);

            if (todoItem == default(TodoItem)) {
                return NotFound();
            }

            TodoDetailPageModel viewModel = new TodoDetailPageModel {
                TodoItem = _mapper.Map<TodoItemViewModel>(todoItem)
            };

            return View(viewModel);
        }
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
