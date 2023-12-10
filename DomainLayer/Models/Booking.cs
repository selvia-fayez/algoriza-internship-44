using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
   public class Booking
    {
        public string Id { get; set; }
        public bool confirmed { get; set; }
        public string time { get; set; }
        public Doctor Doctor { get; set; }
        public User User {  get; set; }
        public Discount Discount{  get; set; } 

    }
}
