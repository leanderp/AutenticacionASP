using AutenticacionASP.Data;
using AutenticacionASP.Models;
using AutenticacionASP.Models.ServiceMessage;
using AutenticacionASP.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace AutenticacionASP.Controllers
{

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        const string SessionCode = "_Name";
        const string SessionEmail = "_Email";
        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            #region
            //var message = "password";
            //var salt = Salt.Create();

            //// Act  
            //var hash = Hash.Create(message, salt);
            //var validacion = Hash.Validate(message, salt, hash);
            #endregion


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

        public IActionResult LoginSMS()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginSMS(GetLogin loginUser)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Users.Where(x => x.Email == loginUser.UserName).FirstOrDefault();
                if (usuario != null)
                {
                    if (usuario.PhoneNumber != null)
                    {
                        Random random = new Random();
                        var telefono = usuario.PhoneNumber;
                        var code = random.Next(100000, 999999);
                        TwilioHelper twilio = new TwilioHelper();
                        twilio.SendSMSMessage(telefono, code.ToString());
                        HttpContext.Session.SetString(SessionEmail, usuario.Email);
                        HttpContext.Session.SetString(SessionCode, code.ToString());
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.Mensaje = "No es posible que se logue por este medio. Tiene que registrar numero de telefono para habilitar esta opcion";
                        return View();
                    }
                }   
            }
            ViewBag.Mensaje = "Usuario invalido";
            return View();
        }


        public IActionResult Login()
        {
            var email = HttpContext.Session.GetString(SessionEmail);
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(AutenticacionASP.Services.LoginModel loginUser)
        {
            if (ModelState.IsValid)
            {
                var code = HttpContext.Session.GetString(SessionCode);
                var email = HttpContext.Session.GetString(SessionEmail);
                if (email != loginUser.Email)
                {
                    ViewBag.ErrorEmail = "Email incorrecto";
                    return View();
                }
                if (code != loginUser.Code)
                {
                    ViewBag.ErrorCodigo = "Codigo de autenticacion invalido";
                    return View();
                }

                var usuario = _context.Users.Where(x => x.Email == loginUser.Email).FirstOrDefault();
                if (usuario!= null)
                {
                    var login = await _signInManager.PasswordSignInAsync(usuario, loginUser.Password, true, false);
                    if (login.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = "Contraseña invalida";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = "Usuario invalido";
                    return View();
                }
                
            }
            return View();
        }

    }
}
