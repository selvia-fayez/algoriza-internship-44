using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Doctor : User
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public Specialization Specialization { get; set; }
        public List<Day> AvailableTimes { get; set; }

    }
}
