using API_Organizacion.Interfaces;
using API_Organizacion.Models;
using DTO;
using System.Security.Cryptography;
using System.Text;

namespace API_Organizacion.Services
{
    public class LoginService : ILogin
    {
        private readonly OrganizacionContext dbContext;

        public LoginService(OrganizacionContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Guid Login(DTOUsuario usuario)
        {
            DTOUsuario log = new DTOUsuario();
            string clave = Password(usuario.clave);
            log = (from u in dbContext.Usuarios
                   where u.usuario == usuario.usuario
                   && u.clave == clave
                   select new DTOUsuario()
                   {
                       idUsuario = u.idUsuario,
                       usuario = u.usuario
                   }).FirstOrDefault();
            if (log == null)
            {
                return Guid.Empty;
            }
            return log.idUsuario;
        }

        #region Aux
        private static string Password(string rawData)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        #endregion
    }
}
