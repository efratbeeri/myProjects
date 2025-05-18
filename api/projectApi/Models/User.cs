using System.ComponentModel.DataAnnotations;

namespace projectApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength (30)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string Role { get; set; }

    }
}
