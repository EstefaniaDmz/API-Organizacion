using API_Organizacion.Interfaces;
using API_Organizacion.Models;
using DTO;
using System.Security.Cryptography;
using System.Text;

namespace API_Organizacion.Services
{
    public class EmpleadosUsuariosService : IEmpleadosUsuarios
    {
        private readonly OrganizacionContext dbContext;

        public EmpleadosUsuariosService(OrganizacionContext dbContext) { 
            this.dbContext = dbContext;
        }

        public List<DTOEmpleadosUsuariosVW> GetEmpleados()
        {
            List<DTOEmpleadosUsuariosVW> empleadosList = new List<DTOEmpleadosUsuariosVW>();
            empleadosList = (from e in dbContext.Empleados
                             join u in dbContext.Usuarios on e.idEmpleado equals u.empleadoId
                             join p in dbContext.Puestos on e.puestoId equals p.idPuesto
                             select new DTOEmpleadosUsuariosVW() {
                             idEmpleado = e.idEmpleado,
                             nombreCompleto = e.apellidoPaterno + " " + e.apellidoMaterno + " " + e.nombre,
                             edad = e.edad,
                             fechaNacimiento = e.fechaNacimiento,
                             fotografia = e.fotografia,
                             puesto = p.descripcion,
                             salario = e.salario,
                             usuario = u.usuario
                             }).ToList();
            return empleadosList;
        }

        public DTOEmpleadosUsuarios GetEmpleado(Guid id)
        {
            DTOEmpleadosUsuarios empleado = new DTOEmpleadosUsuarios();
            empleado = (from e in dbContext.Empleados
                        join u in dbContext.Usuarios on e.idEmpleado equals u.empleadoId
                        where e.idEmpleado == id
                        select new DTOEmpleadosUsuarios() {
                            idEmpleado = e.idEmpleado,
                            nombre = e.nombre,
                            apellidoMaterno = e.apellidoMaterno,
                            apellidoPaterno = e.apellidoPaterno,
                            edad = e.edad,
                            fechaNacimiento = e.fechaNacimiento,
                            fotografia = e.fotografia,
                            puestoId = e.puestoId,
                            salario = e.salario,
                            usuario = u.usuario
                        }).FirstOrDefault();
            return empleado;
        }

        public string InsertEmpleado(DTOEmpleadosUsuarios empleado) {
            try
            {
                byte[] imageData;
                using (FileStream fs = new FileStream(empleado.fotografia, FileMode.Open, FileAccess.Read))
                {
                    using(BinaryReader br = new BinaryReader(fs))
                    {
                        imageData = br.ReadBytes((int)fs.Length);
                    }
                }
                string fotografiaEmpleado = Convert.ToBase64String(imageData);
                string claveEncriptada = Password(empleado.clave);
                    Empleados modelEmp = new Empleados();
                Usuarios modelUsu = new Usuarios();
                modelEmp.nombre = empleado.nombre;
                modelEmp.apellidoMaterno = empleado.apellidoMaterno;
                modelEmp.apellidoPaterno = empleado.apellidoPaterno;
                modelEmp.fechaNacimiento = empleado.fechaNacimiento;
                int edad = empleado.fechaNacimiento.Year - DateTime.Now.Year;
                if(empleado.fechaNacimiento < DateTime.Now)
                {
                    edad--;
                }
                modelEmp.edad = edad;
                modelEmp.fotografia = fotografiaEmpleado;
                modelEmp.salario = empleado.salario;
                modelEmp.puestoId = empleado.puestoId;
                dbContext.Empleados.Add(modelEmp);
                dbContext.SaveChanges();
                Guid idNew = modelEmp.idEmpleado;
                modelUsu.empleadoId = idNew;
                modelUsu.clave = claveEncriptada;
                modelUsu.usuario = empleado.usuario;
                dbContext.Usuarios.Add(modelUsu);
                dbContext.SaveChanges();
                return "Empleado registrado correctamente";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string UpdateEmpleado(DTOEmpleadosUsuarios empleado)
        {
            try
            {
                Empleados modelEmp = new Empleados();
                Usuarios modelUsu = new Usuarios();
                modelEmp = (from e in dbContext.Empleados
                            where e.idEmpleado == empleado.idEmpleado
                            select e).FirstOrDefault();
                modelUsu = (from u in dbContext.Usuarios
                            where u.empleadoId == empleado.idEmpleado
                            select u).FirstOrDefault();
                if(modelEmp == null || modelUsu == null)
                {
                    return "Error: no se encontró el objeto que se desea actualizar";
                }
                if (empleado.fotografia.Contains("/"))
                {
                    byte[] imageData;
                    using (FileStream fs = new FileStream(empleado.fotografia, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            imageData = br.ReadBytes((int)fs.Length);
                        }
                    }
                    string fotografiaEmpleado = Convert.ToBase64String(imageData);
                    empleado.fotografia = fotografiaEmpleado;
                }
                int edad = empleado.fechaNacimiento.Year - DateTime.Now.Year;
                if (empleado.fechaNacimiento < DateTime.Now)
                {
                    edad--;
                }
                modelEmp.nombre = empleado.nombre;
                modelEmp.apellidoMaterno = empleado.apellidoMaterno;
                modelEmp.apellidoPaterno = empleado.apellidoPaterno;
                modelEmp.fechaNacimiento = empleado.fechaNacimiento;
                modelEmp.edad = edad;
                modelEmp.fotografia = empleado.fotografia;
                modelEmp.salario = empleado.salario;
                modelEmp.puestoId = empleado.puestoId;
                dbContext.Entry(modelEmp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                modelUsu.usuario = empleado.usuario;
                if (!string.IsNullOrEmpty(empleado.clave))
                {
                    string claveEncriptada = Password(empleado.clave);
                    empleado.clave = claveEncriptada;
                }
                modelUsu.clave = empleado.clave;
                dbContext.Entry(modelUsu).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
                return "Empleado actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string DeleteEmpleado(Guid id)
        {
            try
            {
                Empleados empleado = dbContext.Empleados.FirstOrDefault(e => e.idEmpleado == id);
                Usuarios usuario = dbContext.Usuarios.FirstOrDefault(u => u.empleadoId == id);
                if(empleado == null || usuario == null)
                {
                    return "Error: No se encontró el objeto que se desea eliminar";
                }
                dbContext.Empleados.Remove(empleado);
                dbContext.Usuarios.Remove(usuario);
                dbContext.SaveChanges();
                return "Empleado eliminado con éxito";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
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
