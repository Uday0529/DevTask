namespace DevTask2.DataAdapters.DBModels
{
    public class TblUser
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public List<TblTask>? Tasks { get; set; }
    }
}
