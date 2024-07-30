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

        //GET IMAGE
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetImage(Guid id)
        {
            var empleado = GetEmpleado(id);
            byte[] imageData = Convert.FromBase64String(empleado.fotografia);
            string contentType = GetContentType(imageData);
            return File(imageData, contentType);
        }

        //UploadImage
        [HttpPost]
        [Route("UploadImage")]
        public IActionResult UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ImagenesApi");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    using (var stream = new FileStream(filePath + $"\\{file.FileName}", FileMode.Create))
                    {
                        file.CopyTo(stream);

                    }

                    string fotografia = filePath + $"\\{file.FileName}";
                    return Ok(new { fotografia });
                }
                return BadRequest("Empty");
            }
            catch (Exception ex)
            {

                return BadRequest("Error: " + ex.Message);
            }
        }

        #region Aux
        private string GetContentType(byte[] imageData)
        {
            if (imageData.Length >= 4)
            {
                var header = BitConverter.ToString(imageData, 0, 4);
                if (header.StartsWith("FF-D8", StringComparison.OrdinalIgnoreCase))
                    return "image/jpeg";
                if (header.StartsWith("89-50-4E-47", StringComparison.OrdinalIgnoreCase))
                    return "image/png";
                if (header.StartsWith("47-49-46-38", StringComparison.OrdinalIgnoreCase))
                    return "image/gif";
                if (header.StartsWith("42-4D", StringComparison.OrdinalIgnoreCase))
                    return "image/bmp";
            }
            return null;
        }
        #endregion
    }
}
