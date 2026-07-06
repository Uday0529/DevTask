using DevTask2.Business.ServiceInterface;
using DevTask2.DataAdapters.IDataAdapter;
using DevTask2.Mapping_Repository.Mapper;
using DevTask2.Models.TaskModels;
using AutoMapper;
using DevTask2.DataAdapters.DBModels;

namespace DevTask2.Business
{
    public class TaskService : ITaskService
    {
        private readonly ITaskDataAdapter _adapter;
        private readonly IMapper _mapper;

        public TaskService(ITaskDataAdapter adapter, IMapper mapper)
        {
            _adapter = adapter;
            _mapper = mapper;
        }

        public async Task<ViewTaskModel> AddTask(string userId, Add_TaskModel task, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(task);
            var mapTask = _mapper.Map<TblTask>(task);
            mapTask.UserId = Convert.ToInt32(userId);
            var addData = await _adapter.AddAsync(mapTask, cancellationToken);
            var mapView = _mapper.Map<ViewTaskModel>(addData);
            return mapView;
        }

        public Task<bool> DeleteTask(string id, CancellationToken cancellationToken)
        {
            return id == null ? throw new ArgumentNullException(nameof(id)) : _adapter.DeleteEntity(id, cancellationToken);
        }

        public async Task<IEnumerable<ViewTaskModel>> GetAllTasks(string userId, CancellationToken cancellationToken)
        {
            string property = "UserId";
            var tblTask = await _adapter.GetAllAsync(property, userId, cancellationToken);
            var mapTask = _mapper.Map<IEnumerable<ViewTaskModel>>(tblTask);
            return mapTask;
        }

        public async Task<ViewTaskModel> GetTaskById(string? taskId, string userId, CancellationToken cancellationToken)
        {
            var tblTask = taskId == null ? throw new ArgumentNullException(nameof(taskId)) : await _adapter.GetById(taskId, userId, cancellationToken);
            var mapTask = _mapper.Map<ViewTaskModel>(tblTask);
            return mapTask;
        }
        public async Task<bool> UpdateTask(string id, string userId, Update_TaskModel task, CancellationToken cancellationToken)
        {
            if (id != task.Id) throw new InvalidCastException(nameof(id));

            var getTask = await _adapter.GetById(id, userId, cancellationToken);
            var mapTask = _mapper.Map(task, getTask);
            mapTask.CompletedAt = mapTask.IsCompleted == true ? DateTime.UtcNow : null;
            return await _adapter.UpdateAsync(mapTask, cancellationToken);
        }


        public async Task<ViewTaskModel> GetTaskByTitle(string userId, string title, CancellationToken cancellationToken)
        {

            var result = await _adapter.GetValueByTwoProperty("UserId", Int32.Parse(userId), "Title", title, cancellationToken);
            var mapResult = _mapper.Map<ViewTaskModel>(result);
            return mapResult;
        }


    }
}
