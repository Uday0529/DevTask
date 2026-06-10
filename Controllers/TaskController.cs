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

        [HttpGet]
        public Task<IEnumerable<ViewTaskModel>> GetAllTask(CancellationToken cancellationToken)
        {
            return _taskService.GetAllTasks(cancellationToken);
        }

        [HttpGet("Get/{Id}")]
        public Task<ViewTaskModel> GetTaskById([FromRoute] string Id,   CancellationToken cancellationToken)
        {
            return _taskService.GetTaskById(Id, cancellationToken);
        }
        [HttpPost("Add")]
        public Task<ViewTaskModel> AddTask([FromBody] Add_TaskModel addTask, CancellationToken cancellationToken) 
        {
             return _taskService.AddTask(addTask, cancellationToken);
        }
        [HttpPut("Update/{Id}")]
        public Task<bool> UpdateTaskById([FromRoute] string Id, [FromBody] Update_TaskModel updateTask, CancellationToken cancellationToken)
        {
            return _taskService.UpdateTask(Id, updateTask, cancellationToken);
        }
        [HttpDelete("Delete/{Id}")]
        public Task<bool> DeleteTaskById([FromRoute] string Id, CancellationToken cancellationToken)
        {
            return _taskService.DeleteTask(Id, cancellationToken);
        }

    }
}
