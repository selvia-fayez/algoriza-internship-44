using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;
using vezeeta.DTO;

namespace vezeeta.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            this._context = context;
        }
        //book a doctor
        [HttpPost("Booking/Add")] //api/Booking/Add
        public bool Book(BookingDTO bookingDTO)
        {
            if (ModelState.IsValid)
            {
                Booking booking = new Booking();
                booking.Doctor.Id = Convert.ToString(bookingDTO.DoctorId);
                booking.User.Id = Convert.ToString(bookingDTO.UserId);
                booking.Discount.Id = Convert.ToString(bookingDTO.DiscountId);
                booking.time = Convert.ToString(bookingDTO.time);
                booking.confirmed = bookingDTO.confirmed;

                _context.Bookings.Add(booking);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        //get patient bookings
        [HttpGet("Booking/PatientBookings/{Id}")] //api/Booking/PatientBookings
        public List<Booking> GetPatientBookings(string Id)
        {
            var result = _context.Bookings.Where(b => b.User.Id == Id);
            return result.ToList();
        }
        //cancel booking
        [HttpDelete("Booking/cancel/{Id}")] //api/Booking/cancel/id
        public bool CancelBooking(int Id)
        {
            Booking booking= _context.Bookings.Find(Id);
            if (booking == null)
            {
                return false;
            }
            else
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
                return true;
            }
        }
    }
}
