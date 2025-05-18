using System.ComponentModel.DataAnnotations;

namespace projectApi.Models.DTO
{
    public class DonorDto
    {
        
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }

    }
}
