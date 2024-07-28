using API_Organizacion.Interfaces;
using API_Organizacion.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Organizacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiariosController : ControllerBase
    {
        private readonly IBeneficiarios beneficiariosService;
        private readonly OrganizacionContext dbContext;

        public BeneficiariosController(IBeneficiarios beneficiariosService, OrganizacionContext dbContext)
        {
            this.beneficiariosService = beneficiariosService;
            this.dbContext = dbContext;
        }

        //GET: Beneficiarios list
        [HttpGet]
        [Route("GetBeneficiarios")]
        public IEnumerable<DTOBeneficiariosVW> GetBeneficiarios()
        {
            List<DTOBeneficiariosVW> beneficiariosList = new List<DTOBeneficiariosVW>();
            beneficiariosList = beneficiariosService.GetBeneficiarios();
            return beneficiariosList;
        }

        //GET: Beneficiario by id
        [HttpGet]
        [Route("GetBeneficiario/{id}")]
        public DTOBeneficiarios GetBeneficiario(Guid id)
        {
            DTOBeneficiarios beneficiario = new DTOBeneficiarios();
            beneficiario = beneficiariosService.GetBeneficiario(id);
            return beneficiario;
        }

        //POST: Insert Beneficiario
        [HttpPost]
        [Route("InsertBeneficiario")]
        public IActionResult InsertBeneficiario([FromBody] DTOBeneficiarios beneficiario)
        {
            string result = beneficiariosService.InsertBeneficiario(beneficiario);
            return Ok(new { result });
        }

        //PUT: Update Beneficiario
        [HttpPut]
        [Route("UpdateBeneficiario")]
        public IActionResult UpdateBeneficiario([FromBody] DTOBeneficiarios beneficiario)
        {
            string result = beneficiariosService.UpdateBeneficiario(beneficiario);
            return Ok(new { result });
        }

        //DELETE: Delete beneficiario
        [HttpDelete]
        [Route("DeleteBeneficiario/{id}")]
        public IActionResult DeleteBeneficiario(Guid id)
        {
            string result = beneficiariosService.DeleteBeneficiario(id);
            return Ok(new { result });
        }
    }
}
