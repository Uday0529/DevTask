namespace DevTask2.Models.TaskModels
{
    public class Update_TaskModel
    {
        public string Id { get; set; }  
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
