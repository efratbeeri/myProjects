using System.ComponentModel.DataAnnotations;

namespace projectApi.Models
{
    public class Donor
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }

        public virtual ICollection<Present>? Donations { get; set; } = new List<Present>();
    }
}
