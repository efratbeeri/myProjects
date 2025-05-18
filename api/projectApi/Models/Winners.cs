namespace projectApi.Models
{
    public class Winners
    {
        public int Id { get; set; }
        public int PresentID { get; set; }
        public Present? Present { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }



    }
}
