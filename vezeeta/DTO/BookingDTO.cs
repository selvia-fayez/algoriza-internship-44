using DomainLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace vezeeta.DTO
{
    public class BookingDTO
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public bool confirmed { get; set; }
        [Required]
        public string time { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int DiscountId { get; set; }
    }
}
