using DTO;

namespace API_Organizacion.Interfaces
{
    public interface IEmpleadosUsuarios
    {
        List<DTOEmpleadosUsuariosVW> GetEmpleados();

        DTOEmpleadosUsuarios GetEmpleado(Guid id);

        string InsertEmpleado(DTOEmpleadosUsuarios empleado);

        string UpdateEmpleado(DTOEmpleadosUsuarios empleado);

        string DeleteEmpleado(Guid id);
    }
}
