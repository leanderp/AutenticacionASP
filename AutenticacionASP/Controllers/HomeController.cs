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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            #region
            //Manejando roles
            //if (User.Identity.IsAuthenticated)
            //{
            //    ClaimsPrincipal getUser = this.User;
            //    var id = getUser.FindFirst(ClaimTypes.NameIdentifier).Value; // Busca el id
            //    var usuario = await _userManager.FindByIdAsync(id); // trae el usuario

            //    //Crear rol
            //    bool x = await _roleManager.RoleExistsAsync("Admin");
            //    if (!x)
            //    {
            //        var rolAdmin = new IdentityRole();
            //        rolAdmin.Name = "Admin";
            //        await _roleManager.CreateAsync(rolAdmin);
            //    }

            //bool y = await _roleManager.RoleExistsAsync("ApruebaPrestamos");
            //if (!y)
            //{
            //    var rolAdmin = new IdentityRole();
            //    rolAdmin.Name = "ApruebaPrestamos";
            //    await _roleManager.CreateAsync(rolAdmin);
            //}

            //Agregar usuario al rol
            //var agregarRol = await _userManager.AddToRoleAsync(usuario, "Admin");
            //var agregarRol2 = await _userManager.AddToRoleAsync(usuario, "ApruebaPrestamos");

            // Usuario esta en rol?
            //var usuarioEsEnRol = await _userManager.IsInRoleAsync(usuario, "Admin"); //true
            //var usuarioEsEnRol2 = await _userManager.IsInRoleAsync(usuario, "ApruebaPrestamos"); //false
            //var usuarioEsEnRol3 = await _userManager.IsInRoleAsync(usuario, "Vendedores"); //false

            // Roles del usuario
            //var roles = await _userManager.GetRolesAsync(usuario);

            // Remover a usuario de Rol
            //var removerRol = await _userManager.RemoveFromRoleAsync(usuario, "Admin");

            // Borrar rol
            //var rolVendedor = await _roleManager.FindByNameAsync("ApruebaPrestamos");
            //await _roleManager.DeleteAsync(rolVendedor);
            //}
            #endregion
            #region
            // Manejando los Usuarios
            //var estaAutenticado = User.Identity.IsAuthenticated;
            //if (estaAutenticado)
            //{
            //    var NombreUsuario = User.Identity.Name; // Obtener Nombre
            //    ClaimsPrincipal getUser = this.User;
            //    var id = getUser.FindFirst(ClaimTypes.NameIdentifier).Value; // Obtener id 

            //    var usuario = _context.Users.Where(x => x.Id == id).FirstOrDefault(); // Buscar usuario por id
            //    var emailConfirmado = usuario.EmailConfirmed;

            //    var usuario2 = _userManager.FindByIdAsync(id); // Buscar usuario por id

            //    var user = new IdentityUser(); // instancia un nuevo usuario
            //    user.UserName = "leander";
            //    user.Email = "leander@ejemplo.com";

            //    //var resultado = await _userManager.CreateAsync(user, "Contraseña1234"); // crea un nuevo usuario
            //}
            #endregion
            return View();
        }
        [Authorize(Roles = "Admin")]
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
