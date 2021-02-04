using AutenticacionASP.Data;
using AutenticacionASP.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AutenticacionASP.Controllers
{

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> IndexAsync()
        {
            var estaAutenticado = User.Identity.IsAuthenticated;
            if (estaAutenticado)
            {
                var NombreUsuario = User.Identity.Name; // Obtener Nombre
                ClaimsPrincipal getUser = this.User;
                var id = getUser.FindFirst(ClaimTypes.NameIdentifier).Value; // Obtener id 

                var usuario = _context.Users.Where(x => x.Id == id).FirstOrDefault(); // Buscar usuario por id
                var emailConfirmado = usuario.EmailConfirmed;

                var usuario2 = _userManager.FindByIdAsync(id); // Buscar usuario por id

                var user = new IdentityUser(); // instancia un nuevo usuario
                user.UserName = "leander";
                user.Email = "leander@ejemplo.com";

                //var resultado = await _userManager.CreateAsync(user, "Contraseña1234"); // crea un nuevo usuario
                
            
            }
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
