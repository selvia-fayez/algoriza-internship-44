using DomainLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace vezeeta.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username{ get; set; }
        public Gender Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Image { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
