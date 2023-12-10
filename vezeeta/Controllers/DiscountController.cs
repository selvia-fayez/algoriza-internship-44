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
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private ApplicationDbContext _context;

        public DiscountController(ApplicationDbContext context)
        {
            this._context = context;
        }
        //add discount
        [HttpPost("Discount/Add")] //api/Discount/Add
        public bool AddDiscount(Discount discountData)
        {
            if (ModelState.IsValid)
            {
                _context.Discounts.Add(discountData);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        //edit discount
        [HttpPatch("Discount/Edit/{Id}")] //api/Discount/Edit
        public bool EditDiscount(int Id,Discount discountData)
        {
            Discount discount = _context.Discounts.Find(Id);
            if (discount == null)
            {
                return false;
            }
            else
            {
               discount.DiscountCode = discountData.DiscountCode;
               discount.NoRequests= discountData.NoRequests;
               discount.Value= discountData.Value;
               discount.DiscountType = discountData.DiscountType;
                _context.SaveChanges();
                return true;
            }
        }
        //delete discount
        [HttpDelete("Discount/Delete/{Id}")] //api/Discount/Delete
        public bool DeleteDiscount(int Id)
        {
            Discount discount = _context.Discounts.Find(Id);
            if (discount == null)
            {
                return false;
            }
            else
            {
                _context.Discounts.Remove(discount);
                _context.SaveChanges();
                return true;
            }
        }
        //Deactivate discount
        [HttpPatch("Discount/Deactivate/{Id}")] //api/Discount/Deactivate
        public bool DeactivateDiscount(int Id)
        {
            Discount discount = _context.Discounts.Find(Id);
            if (discount == null)
            {
                return false;
            }
            else
            {
                discount.isActive = false;
                _context.SaveChanges();
                return true;
            }
        }
    }
}
