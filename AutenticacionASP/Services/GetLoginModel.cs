using System.ComponentModel.DataAnnotations;

namespace AutenticacionASP.Services
{
    public class GetLogin
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string UserName { get; set; }
    }
}
