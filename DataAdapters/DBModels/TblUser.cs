using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevTask2.DataAdapters.DBModels
{
    public class TblUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string username { get; set; }
        public string password { get; set; }
        public List<TblTask>? Tasks { get; set; }
    }
}
