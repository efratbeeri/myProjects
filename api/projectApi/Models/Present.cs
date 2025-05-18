using System.ComponentModel.DataAnnotations;

namespace projectApi.Models
{
    public class Present
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public int Price { get; set; }

        public string Kategory { get; set; }
        public string Image { get; set; }
        public string Donorname { get; set; }
        public int DonorsId { get; set; }
        public Donor? Donor { get; set; }
        public bool IsDrawn { get; set; } = false;
        public string WinnerName { get; set; }

    }
}
