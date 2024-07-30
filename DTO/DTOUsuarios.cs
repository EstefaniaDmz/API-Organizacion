using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOUsuario
    {
        public Guid idUsuario { get; set; }

        public string usuario { get; set; }

        public string clave { get; set; }
    }
}
