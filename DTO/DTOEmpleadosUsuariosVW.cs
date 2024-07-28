using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOEmpleadosUsuariosVW
    {
        #region Empleado
        public Guid idEmpleado { get; set; }

        public string nombreCompleto { get; set; }

        public DateTime fechaNacimiento { get; set; }

        public int? edad { get; set; }

        public string fotografia { get; set; }

        public double salario { get; set; }

        public string puesto { get; set; }
        #endregion

        #region Usuario
        public string usuario { get; set; }
        #endregion
    }
}
