using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOBeneficiarios
    {
        public Guid idBeneficiario { get; set; }

        public string nombre { get; set; }

        public string apellidoPaterno { get; set; }

        public string apellidoMaterno { get; set; }

        public Guid parentescoId { get; set; }

        public Guid empleadoId { get; set; }
    }
}
