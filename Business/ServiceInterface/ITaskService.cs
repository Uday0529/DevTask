using DevTask2.Models.TaskModels;

namespace DevTask2.Business.ServiceInterface
{
    public interface ITaskService
    {
        Task<IEnumerable<ViewTaskModel>> GetAllTasks(string userId, CancellationToken cancellationToken);
        Task<ViewTaskModel> GetTaskById(string taskId, string userId, CancellationToken cancellationToken);
        Task<ViewTaskModel> AddTask(Add_TaskModel task, CancellationToken cancellationToken);
        Task<bool> UpdateTask(string id, string userId, Update_TaskModel task, CancellationToken cancellationToken);
        Task<bool> DeleteTask(string id, CancellationToken cancellationToken);
        Task<ViewTaskModel> GetTaskByTitle(string userId, string title, CancellationToken cancellationToken);


    }
}
