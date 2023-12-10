using DomainLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RepositoryLayer
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions options) : base(options) {
        }
        public virtual DbSet<User> users{get; set; }
        public virtual DbSet<Doctor> Doctors {get; set;}
        public virtual DbSet<Booking> Bookings{get; set;}
        public virtual DbSet<Specialization> Specializations{ get; set; }
        public virtual DbSet<Discount> Discounts{ get; set; }
        public virtual DbSet<Day> Days{ get; set; }
        public virtual DbSet<TimeSlot> TimeSlots{ get; set; }



    }
}
