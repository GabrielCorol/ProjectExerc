using Microsoft.AspNetCore.Mvc;
using ProjectExerc.Models;
using ProjectExerc.Repositorio;

namespace ProjectExerc.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepositorio _loginRepositorio;
        public LoginController(LoginRepositorio loginRepositorio)
        {
            _loginRepositorio = loginRepositorio;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var login = _loginRepositorio.ObterUsuario(email);
            if(login != null && login.Senha == senha)
            {
                return RedirectToAction("Index", "Login");
            }
            ModelState.AddModelError("", "Email ou senha incorretos!");
            return View();
        }
    }
}
