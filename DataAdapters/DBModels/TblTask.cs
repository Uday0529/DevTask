using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevTask2.DataAdapters.DBModels
{
    public class TblTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public TblUser Users { get; set; }

    }
}
