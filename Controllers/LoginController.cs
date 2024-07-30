using API_Organizacion.Interfaces;
using API_Organizacion.Models;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API_Organizacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin loginService;
        private readonly OrganizacionContext dbContext;

        public LoginController(ILogin loginService, OrganizacionContext dbContext) { 
            this.loginService = loginService;
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(DTOUsuario usuario)
        {
            Guid idUsuario = loginService.Login(usuario);
            return Ok(new { idUsuario });
        }
    }
}
