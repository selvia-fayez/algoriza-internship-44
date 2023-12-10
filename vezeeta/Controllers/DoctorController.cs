using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;

namespace vezeeta.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            this._context = context;
        }
        //get doctor bookings
        [HttpGet("Booking/DoctorBookings/{Id}")] //api/Booking/DoctorBookings
        public List<Booking> GetDoctorBookings(string Id)
        {
            var result = _context.Bookings.Where(b => b.Doctor.Id == Id);
            return result.ToList();
        }
        //confirm checkup
        [HttpPatch("Booking/confirm/{Id}")] //api/Booking/confirm/id
        public bool ConfirmCheckup(int Id)
        {
            Booking booking = _context.Bookings.Find(Id);
            if (booking == null)
            {
                return false;
            }
            else
            {
                booking.confirmed = true;
                _context.SaveChanges();
                return true;
            }
        }

    }
}
