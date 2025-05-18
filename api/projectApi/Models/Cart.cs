namespace projectApi.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool Final { get; set; } = false;
        public virtual ICollection<Purchases>? PurchasesList { get; set; } = new List<Purchases>();

    }
}
