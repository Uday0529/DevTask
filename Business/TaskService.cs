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

        public async Task<ViewTaskModel> AddTask(Add_TaskModel task, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(task);
            var mapTask = _mapper.Map<TblTask>(task);
            var addData = await _adapter.AddAsync(mapTask, cancellationToken);
            var mapView = _mapper.Map<ViewTaskModel>(addData);
            return mapView;
        }

        public Task<bool> DeleteTask(string id, CancellationToken cancellationToken)
        {
            return id == null ? throw new ArgumentNullException(nameof(id)) : _adapter.DeleteEntity(id, cancellationToken);
        }

        public async Task<IEnumerable<ViewTaskModel>> GetAllTasks(CancellationToken cancellationToken)
        {
            var tblTask = await _adapter.GetAllAsync(cancellationToken);
            var mapTask = _mapper.Map<IEnumerable<ViewTaskModel>>(tblTask);
            return mapTask;
        }

        public async Task<ViewTaskModel> GetTaskById(string? taskId, CancellationToken cancellationToken)
        {
            var tblTask = taskId == null ? throw new ArgumentNullException(nameof(taskId)) : await _adapter.GetById(taskId, cancellationToken);
            var mapTask = _mapper.Map<ViewTaskModel>(tblTask);
            return mapTask;
        }
        public async Task<bool> UpdateTask(string id, Update_TaskModel task, CancellationToken cancellationToken)
        {
            if (id != task.Id) throw new InvalidCastException (nameof(id));

            var getTask = await _adapter.GetById(id, cancellationToken);
            var mapTask = _mapper.Map(task, getTask);
            mapTask.CompletedAt = mapTask.IsCompleted == true ?  DateTime.UtcNow : null;
            return await _adapter.UpdateAsync(mapTask, cancellationToken);
        }


        public async Task<ViewTaskModel> GetTaskByTitle(string title, CancellationToken cancellationToken)
        {
            var tbltasks = await _adapter.GetAllAsync(cancellationToken);
            var findTask = tbltasks.FirstOrDefault(t => t.Title == title)?? throw new KeyNotFoundException (nameof(title));
            var mapView = _mapper.Map<ViewTaskModel>(findTask);
            return mapView;
        }

       
    }
}
