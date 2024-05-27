using System.ComponentModel.DataAnnotations;

namespace FreeCourse.IdentityServer.Dtos
{
    public class SignUpDto
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
    }
}
