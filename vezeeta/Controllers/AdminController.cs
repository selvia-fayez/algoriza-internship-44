using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using vezeeta.DTO;

namespace vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> usermanager;

        public AdminController(UserManager<User> usermanager)
        {
            this.usermanager = usermanager;
        }

        //get number of doctors
        [HttpGet("Dashboard/DrCount")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> GetDoctorsCount()
        {
            var doctors = await usermanager.GetUsersInRoleAsync("Doctor");
            var count = doctors.ToList().Count();
            return count;
        }

        //get number of patients
        [HttpGet("Dashboard/PatientCount")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> GetPatientCount()
        {
            var patients = await usermanager.GetUsersInRoleAsync("Patient");
            var count = patients.ToList().Count();
            return count;
        }

        //get all doctors
        [Authorize(Roles = "Admin, Patient")]
        [HttpGet("Doctor/GetAll")] //api/Admin/GetAll
        public async Task<List<User>> GetAllDoctors()
        {
            var doctors = await usermanager.GetUsersInRoleAsync("Doctor");
            return doctors.ToList();              
        }
        
        //get doctor by id 
        [HttpGet("Doctor/GetDoctor/{id}")] //api/Admin/GetDoctor
        [Authorize(Roles = "Admin")]
        public async Task<User> GetDoctorById(string id)
        {
            var doctor = await usermanager.FindByIdAsync(id);
            return doctor;
        }

        //add doctor
        [HttpPost("Doctor/AddDr")] //api/Admin/AddDr
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDoctor(RegisterDoctorDto doctorDTO)
        {
            if (ModelState.IsValid)
            {
                Doctor doctor = new Doctor();

                doctor.FirstName = doctorDTO.FirstName;
                doctor.LastName = doctorDTO.LastName;
                doctor.Email = doctorDTO.Email;
                doctor.PhoneNumber = doctorDTO.Phone;
                doctor.Gender = doctorDTO.Gender;
                doctor.DateOfBirth = doctorDTO.DateOfBirth;
                doctor.Image = doctorDTO.Image;
                doctor.UserName = doctorDTO.Username;
                doctor.Price = doctorDTO.Price;
                doctor.Specialization.Id = doctorDTO.SpecilizationId;

                IdentityResult result = await usermanager.CreateAsync(doctor, doctorDTO.Password);
                await usermanager.AddToRoleAsync(doctor, "Doctor");
                if (result.Succeeded)
                {
                    return Ok("Doctor Added Successfully");
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

        //edit doctor
        [Authorize(Roles = "Admin")]
        [HttpPatch("Doctor/EditDr/{id}")] //api/Admin/EditDr/id
        public async Task<IActionResult> EditDoctor(string id,RegisterDoctorDto doctorDTO)
        { 
            Doctor doctor = (Doctor) await usermanager.FindByIdAsync(id);
            if(doctor == null)
            {
                return NotFound("Doctor Not found");
            }
            doctor.FirstName = doctorDTO.FirstName;
            doctor.LastName = doctorDTO.LastName;
            doctor.Email = doctorDTO.Email;
            doctor.PhoneNumber = doctorDTO.Phone;
            doctor.Gender = doctorDTO.Gender;
            doctor.DateOfBirth = doctorDTO.DateOfBirth;
            doctor.Image = doctorDTO.Image;
            doctor.UserName = doctorDTO.Username;
            doctor.Price = doctorDTO.Price;
            doctor.Specialization.Id = doctorDTO.SpecilizationId;

            IdentityResult result = await usermanager.UpdateAsync(doctor);
            if (result.Succeeded)
            {
                return Ok("Doctor Updated Successfully");
            }
            return BadRequest(result.Errors); 
        }

        //delete doctor by id 
        [Authorize(Roles = "Admin")]
        [HttpDelete("Doctor/DeleteDr/{id}")] //api/Admin/DeleteDoctor
        public async Task<IActionResult> DeleteDoctorById(string id)
        {
            var doctor = await usermanager.FindByIdAsync(id);
            if (doctor == null)
            {
                return NotFound("Doctor Not found");
            }
            else
            {
                var result = await usermanager.DeleteAsync(doctor);
                if (result.Succeeded)
                {
                    return Ok("Doctor Deleted Successfully");
                }
                return BadRequest(result.Errors);
            }
        }

        //get all patients
        [Authorize(Roles = "Admin")]
        [HttpGet("Patient/GetAll")] //api/Admin/GetAll
        public async Task<List<User>> GetAllPatients()
        {
            var patients = await usermanager.GetUsersInRoleAsync("Patient");
            return patients.ToList();
        }

        //get patient by id 
        [Authorize(Roles = "Admin")]
        [HttpGet("Patient/GetPatient/{id}")] //api/Admin/GetPatient
        public async Task<User> GetPatientById(string id)
        {
            var patient = await usermanager.FindByIdAsync(id);
            return patient;
        }
    }
}
