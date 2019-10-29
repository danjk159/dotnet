using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Result;
using Microsoft.Extensions.Logging;

namespace TodoApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase {
        private readonly TodoContext _context;
        private readonly ILogger _logger;
        public TodoController (TodoContext context, ILogger<TodoController> logger) {
            _context = context;
            if (_context.TodoItems.Count () == 0) {
                _context.TodoItems.Add (new TodoItem { Name = "Item1" });
                _context.SaveChanges ();
            }
            this._logger = logger;
        }
        // GET: api/Todo 获取操作, 返回200
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems () {
            _logger.LogError ("Error-----------11111111111111111");
            return await _context.TodoItems.ToListAsync ();
        }

        // GET: api/Todo/5 
        [HttpGet ("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem (long id) {
            var todoItem = await _context.TodoItems.FindAsync (id);
            if (todoItem == null) {
                return NotFound ();
            }
            return todoItem;
        }

        [HttpGet ("result/{id}")]
        public async Task<ActionResult<BaseResult>> GetTodoItem2 (long id) {
            var todoItem = await _context.TodoItems.FindAsync (id);
            if (todoItem == null) {
                return BaseResult.getErrorResult ();
            }
            return BaseResult.getSuccessResult (todoItem);
        }

        /// <summary>
        /// Creates a TodoItem.POST: api/Todo  传输操作,返回201
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem (TodoItem item) {
            _context.TodoItems.Add (item);
            await _context.SaveChangesAsync ();
            return CreatedAtAction (nameof (GetTodoItem), new { id = item.Id }, item);
        }
        // PUT: api/Todo/5  修改操作,返回205
        [HttpPut ("{id}")]
        public async Task<IActionResult> PutTodoItem (long id, TodoItem item) {
            if (id != item.Id) {
                return BadRequest ();
            }
            _context.Entry (item).State = EntityState.Modified;
            await _context.SaveChangesAsync ();
            return NoContent ();
        }
        /// <summary>
        /// DELETE: api/Todo/5 .删除操作,返回204
        /// </summary>
        /// <param name="id"></param> 
        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteTodoItem (long id) {
            var todoItem = await _context.TodoItems.FindAsync (id);
            if (todoItem == null) {
                return NotFound ();
            }
            _context.TodoItems.Remove (todoItem);
            await _context.SaveChangesAsync ();
            return NoContent ();
        }
    }
}