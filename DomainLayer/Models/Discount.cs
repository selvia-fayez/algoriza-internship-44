using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Discount
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public bool isActive{ get; set; }
        [Required]
        public DiscountType DiscountType{ get; set; }
        [Required]
        public string DiscountCode{ get; set; }
        [Required]
        public int Value { get; set; }
        [Required]
        public int NoRequests { get; set; }

    }
}
