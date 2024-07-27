using API_Organizacion.Interfaces;
using API_Organizacion.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Organizacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentescosController : ControllerBase
    {
        private readonly IParentescos parentescosService;
        private readonly OrganizacionContext dbContext;

        public ParentescosController(IParentescos parentescosService, OrganizacionContext dbContext)
        {
            this.parentescosService = parentescosService;
            this.dbContext = dbContext;
        }

        //GET: Parentescos list
        [HttpGet]
        [Route("GetParentescos")]
        public IEnumerable<DTOParentescos> GetParentescos()
        {
            List<DTOParentescos> parentescosList = new List<DTOParentescos>();
            parentescosList = parentescosService.GetParentescos();
            return parentescosList;
        }

        //GET: Parentesco by id
        [HttpGet]
        [Route("GetParentesco/{id}")]
        public DTOParentescos GetParentesco(Guid id)
        {
            DTOParentescos puesto = new DTOParentescos();
            puesto = parentescosService.GetParentesco(id);
            return puesto;
        }

        //POST: Insert Parentesco
        [HttpPost]
        [Route("InsertParentesco")]
        public IActionResult InsertParentesco([FromBody] DTOParentescos puesto)
        {
            string result = parentescosService.InsertParentesco(puesto);
            return Ok(new { result });
        }

        //PUT: Update Parentesco
        [HttpPut]
        [Route("UpdateParentesco")]
        public IActionResult UpdateParentesco([FromBody] DTOParentescos puesto)
        {
            string result = parentescosService.UpdateParentesco(puesto);
            return Ok(new { result });
        }

        //DELETE: Delete puesto
        [HttpDelete]
        [Route("DeleteParentesco/{id}")]
        public IActionResult DeleteParentesco(Guid id)
        {
            string result = parentescosService.DeleteParentesco(id);
            return Ok(new { result });
        }
    }
}
