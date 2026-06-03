namespace DevTask2.Models.TaskModels
{
    public class Get_TaskModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
