namespace projectApi.Models
{
    public class Purchases
    {
        public int Id { get; set; }


        public int PresentID { get; set; }
        public Present? Present { get; set; }

        public int CartId { get; set; }
        public Cart? Cart { get; set; }       
        public int Amount { get; set; }

    }
}
