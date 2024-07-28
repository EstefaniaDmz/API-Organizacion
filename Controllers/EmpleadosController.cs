using API_Organizacion.Interfaces;
using API_Organizacion.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Organizacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadosUsuarios empleadosService;
        private readonly OrganizacionContext dbContext;

        public EmpleadosController(IEmpleadosUsuarios empleadosService, OrganizacionContext dbContext)
        {
            this.empleadosService = empleadosService;
            this.dbContext = dbContext;
        }

        //GET: Empleados list
        [HttpGet]
        [Route("GetEmpleados")]
        public IEnumerable<DTOEmpleadosUsuariosVW> GetEmpleados()
        {
            List<DTOEmpleadosUsuariosVW> empleadosList = new List<DTOEmpleadosUsuariosVW>();
            empleadosList = empleadosService.GetEmpleados();
            return empleadosList;
        }

        //GET: Empleado by id
        [HttpGet]
        [Route("GetEmpleado/{id}")]
        public DTOEmpleadosUsuarios GetEmpleado(Guid id)
        {
            DTOEmpleadosUsuarios empleado = new DTOEmpleadosUsuarios();
            empleado = empleadosService.GetEmpleado(id);
            return empleado;
        }

        //POST: Insert empleado
        [HttpPost]
        [Route("InsertEmpleado")]
        public IActionResult InsertEmpleado([FromBody] DTOEmpleadosUsuarios empleado)
        {
            string result = empleadosService.InsertEmpleado(empleado);
            return Ok(new { result });
        }

        //PUT: Update empleado
        [HttpPut]
        [Route("UpdateEmpleado")]
        public IActionResult UpdateEmpleado([FromBody] DTOEmpleadosUsuarios empleado)
        {
            string result = empleadosService.UpdateEmpleado(empleado);
            return Ok(new { result });
        }

        //DELETE: Delete empleado
        [HttpDelete]
        [Route("DeleteEmpleado/{id}")]
        public IActionResult DeleteEmpleado(Guid id)
        {
            string result = empleadosService.DeleteEmpleado(id);
            return Ok(new { result });
        }
    }
}
