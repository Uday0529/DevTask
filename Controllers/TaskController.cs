using DevTask2.Business.ServiceInterface;
using DevTask2.Models.TaskModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevTask2.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class TaskController : ControllerBase
    {

        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        }

        //GET: api/tasks
        [HttpGet]
        public Task<IEnumerable<ViewTaskModel>> GetAllTask(CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            return _taskService.GetAllTasks(userId, cancellationToken);
        }

        //GET: api/tasks/{id}
        [HttpGet("{id}")]
        public Task<ViewTaskModel> GetTaskById([FromRoute] string id, CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            return _taskService.GetTaskById(id, userId, cancellationToken);
        }

        //POST: api/tasks
        [HttpPost]
        public Task<ViewTaskModel> AddTask([FromBody] Add_TaskModel addTask, CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            return _taskService.AddTask(userId, addTask, cancellationToken);
        }

        //PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public Task<bool> UpdateTaskById([FromRoute] string id, [FromBody] Update_TaskModel updateTask, CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            return _taskService.UpdateTask(id, userId, updateTask, cancellationToken);
        }

        //DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public Task<bool> DeleteTaskById([FromRoute] string id, CancellationToken cancellationToken)
        {
            return _taskService.DeleteTask(id, cancellationToken);
        }

        //GET: api/tasks/search?title=...
        [HttpGet("search")]
        public async Task<ViewTaskModel> GetTaskByTitle([FromQuery] string title, CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();

            var result = await _taskService.GetTaskByTitle(userId, title, cancellationToken);
            return result;
        }
    }
}
