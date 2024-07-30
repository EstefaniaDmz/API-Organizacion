using DTO;

namespace API_Organizacion.Interfaces
{
    public interface ILogin
    {
        Guid Login(DTOUsuario usuario);
    }
}
