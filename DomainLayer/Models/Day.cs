using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Day
    {
        public string Id { get; set; }
        public Days day { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
    }
}
