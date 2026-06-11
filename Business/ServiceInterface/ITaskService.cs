using DevTask2.Models.TaskModels;

namespace DevTask2.Business.ServiceInterface
{
    public interface ITaskService
    {
        Task<IEnumerable<ViewTaskModel>> GetAllTasks(CancellationToken cancellationToken);
        Task<ViewTaskModel> GetTaskById(string taskId, CancellationToken cancellationToken);
        Task<ViewTaskModel> AddTask(Add_TaskModel task, CancellationToken cancellationToken);
        Task<bool> UpdateTask(string id , Update_TaskModel task, CancellationToken cancellationToken);
        Task<bool> DeleteTask(string id , CancellationToken cancellationToken);
        Task<ViewTaskModel> GetTaskByTitle(string title, CancellationToken cancellationToken);

    }
}
