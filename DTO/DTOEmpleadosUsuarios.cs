using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOEmpleadosUsuarios
    {
        #region Empleado
        public Guid idEmpleado { get; set; }

        public string nombre { get; set; }

        public string apellidoPaterno { get; set; }

        public string apellidoMaterno { get; set; }

        public DateTime fechaNacimiento { get; set; }

        public int? edad { get; set; }

        public string fotografia { get; set; }

        public double salario { get; set; }

        public Guid puestoId { get; set; }
        #endregion

        #region Usuario
        public string usuario { get; set; }

        public string clave { get; set; }
        #endregion
    }
}
