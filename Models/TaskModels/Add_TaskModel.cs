using System.ComponentModel.DataAnnotations;

namespace DevTask2.Models.TaskModels
{
    public class Add_TaskModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? UserId {  get; set; } 
    }
}
