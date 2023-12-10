using DomainLayer.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class User : IdentityUser
    {
        public string Id{ get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public Gender Gender{ get; set; }
        public string DateOfBirth{ get; set; }
        public string Image{ get; set; }
        
    }
}
