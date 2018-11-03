using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListProject.Business.Abstract;
using TodoListProject.Entities.Concrete;
using TodoListProject.WebAPI.Models;

namespace TodoListProject.WebAPI.Controllers {

    [Route("api/[controller]")]
    public class TodoController : Controller {
        private ITodoItemService _todoItemService;
        private IMapper _mapper;
        public TodoController(ITodoItemService todoItemService, IMapper mapper) {
            _todoItemService = todoItemService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Get() {
            var todoList = _mapper.Map<List<TodoItemModel>>(_todoItemService.GetAll());
            return Ok(todoList);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int Id) {
            if (Id == default(int)) {
                return NotFound();
            }

            var todoItem = _todoItemService.GetById(Id);

            if (todoItem == default(TodoItem)) {
                return NotFound();
            }
            var returnItem = _mapper.Map<TodoItemModel>(todoItem);
            return Ok(returnItem);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoItemModel model) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            try {
                _todoItemService.Add(_mapper.Map<TodoItem>(model));
                return Ok(model);
            }
            catch {

            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Put([FromBody] TodoItemModel model) {

            if (!ModelState.IsValid) {
                return BadRequest();
            }

            try {
                _todoItemService.Update(_mapper.Map<TodoItem>(model));
                return Ok(model);
            }
            catch {

            }

            return BadRequest();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id) {

            if (Id == default(int)) {
                return NotFound();
            }

            var todoItem = _todoItemService.GetById(Id);

            if (todoItem == default(TodoItem)) {
                return NotFound();
            }

            try {
                _todoItemService.Delete(todoItem);
                return Ok();
            }
            catch {

            }

            return BadRequest();
        }

        [HttpPost]
        [Route("~/api/todo/complete/{Id:int}")]

        public IActionResult Complete(int Id) {

            if (Id == default(int)) {
                return NotFound();
            }

            var todoItem = _todoItemService.GetUnCompletedById(Id);

            if (todoItem == default(TodoItem)) {
                return NotFound();
            }

            try {
                todoItem.IsComplete = true;

                _todoItemService.Update(todoItem);
                return Ok();

            }
            catch {}

            return BadRequest();
        }
    }
}