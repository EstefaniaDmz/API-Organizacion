using API_Organizacion.Interfaces;
using API_Organizacion.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Organizacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuestosController : ControllerBase
    {
        private readonly IPuestos puestosService;
        private readonly OrganizacionContext dbContext;
        
        public PuestosController(IPuestos puestosService, OrganizacionContext dbContext) {
            this.puestosService = puestosService;
            this.dbContext = dbContext;
        }

        //GET: Puestos list
        [HttpGet]
        [Route("GetPuestos")]
        public IEnumerable<DTOPuestos> GetPuestos()
        {
            List<DTOPuestos> puestosList = new List<DTOPuestos>();
            puestosList = puestosService.GetPuestos();
            return puestosList;
        }

        //GET: Puesto by id
        [HttpGet]
        [Route("GetPuesto/{id}")]
        public DTOPuestos GetPuesto(Guid id)
        {
            DTOPuestos puesto = new DTOPuestos();
            puesto = puestosService.GetPuesto(id);
            return puesto;
        }

        //POST: Insert Puesto
        [HttpPost]
        [Route("InsertPuesto")]
        public IActionResult InsertPuesto([FromBody] DTOPuestos puesto)
        {
            string result = puestosService.InsertPuesto(puesto);
            return Ok(new { result });
        }

        //PUT: Update Puesto
        [HttpPut]
        [Route("UpdatePuesto")]
        public IActionResult UpdatePuesto([FromBody] DTOPuestos puesto)
        {
            string result = puestosService.UpdatePuesto(puesto);
            return Ok(new { result });
        }

        //DELETE: Delete puesto
        [HttpDelete]
        [Route("DeletePuesto/{id}")]
        public IActionResult DeletePuesto(Guid id)
        {
            string result = puestosService.DeletePuesto(id);
            return Ok(new { result });
        }
    }
}
