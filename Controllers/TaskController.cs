using DevTask2.Business.ServiceInterface;
using DevTask2.Models.TaskModels;
using Microsoft.AspNetCore.Mvc;

namespace DevTask2.Controllers
{
    [Route("api/Task")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("Get/{UserId}")]
        public Task<IEnumerable<ViewTaskModel>> GetAllTask([FromRoute] string UserId, CancellationToken cancellationToken)
        {
            return _taskService.GetAllTasks(UserId, cancellationToken);
        }

        [HttpGet("Get/{UserId}/{Id}")]
        public Task<ViewTaskModel> GetTaskById([FromRoute] string Id, [FromRoute] string UserId, CancellationToken cancellationToken)
        {
            return _taskService.GetTaskById(Id, UserId, cancellationToken);
        }
        [HttpPost("Add/")]
        public Task<ViewTaskModel> AddTask([FromBody] Add_TaskModel addTask, CancellationToken cancellationToken)
        {
            return _taskService.AddTask(addTask, cancellationToken);
        }
        [HttpPut("Update/{Id}/{UserId}")]
        public Task<bool> UpdateTaskById([FromRoute] string Id, string UserId, [FromBody] Update_TaskModel updateTask, CancellationToken cancellationToken)
        {
            return _taskService.UpdateTask(Id, UserId, updateTask, cancellationToken);
        }
        [HttpDelete("Delete/{Id}")]
        public Task<bool> DeleteTaskById([FromRoute] string Id, CancellationToken cancellationToken)
        {
            return _taskService.DeleteTask(Id, cancellationToken);
        }
        [HttpGet("FindByTitle/{UserId}/{Title}")]
        public Task<ViewTaskModel> GetTaskByTitle([FromRoute] string UserId, string Title, CancellationToken cancellationToken)
        {
            return _taskService.GetTaskByTitle(UserId, Title, cancellationToken);
        }
    }
}
