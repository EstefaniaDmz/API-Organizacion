using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOBeneficiariosVW
    {
        public Guid idBeneficiario { get; set; }

        public string nombreCompleto { get; set; }

        public string parentesco { get; set; }

        public string empleado { get; set; }
    }
}
