using DevTask2.Models.TaskModels;
using Microsoft.AspNetCore.Mvc;

namespace DevTask2.Models.UserModels
{
    public class ViewUserModel
    {
        public string UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; }    
        public List<ViewTaskModel?> Tasks { get; set; }
    }
}
