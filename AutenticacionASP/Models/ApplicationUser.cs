using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionASP.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(120)]
        public string LugarDeNacimiento { get; set; }
    }
}
