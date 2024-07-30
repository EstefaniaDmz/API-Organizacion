using API_Organizacion.Interfaces;
using API_Organizacion.Models;
using DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_Organizacion.Services
{
    public class BeneficiariosService : IBeneficiarios
    {
        private readonly OrganizacionContext dbContext;

        public BeneficiariosService(OrganizacionContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<DTOBeneficiariosVW> GetBeneficiarios()
        {
            List<DTOBeneficiariosVW> beneficiariosList = new List<DTOBeneficiariosVW>();
            beneficiariosList = (from b in dbContext.Beneficiarios
                                 join e in dbContext.Empleados on b.empleadoId equals e.idEmpleado
                                 join p in dbContext.Parentescos on b.parentescoId equals p.idParentesco
                                 select new DTOBeneficiariosVW()
                                 {
                                     idBeneficiario = b.idBeneficiario,
                                     nombreCompleto = b.apellidoPaterno + " " + b.apellidoMaterno + " " + b.nombre,
                                     empleado = e.apellidoPaterno + " " + e.apellidoMaterno + " " + e.nombre,
                                     parentesco = p.descripcion
                                 }).ToList();
            return beneficiariosList;
        }

        public DTOBeneficiarios GetBeneficiario(Guid id)
        {
            DTOBeneficiarios beneficiario = new DTOBeneficiarios();
            beneficiario = (from b in dbContext.Beneficiarios
                            where b.idBeneficiario == id
                            select new DTOBeneficiarios()
                            {
                                idBeneficiario = b.idBeneficiario,
                                nombre = b.nombre,
                                apellidoMaterno = b.apellidoMaterno,
                                apellidoPaterno = b.apellidoPaterno,
                                empleadoId = b.empleadoId,
                                parentescoId = b.parentescoId
                            }).FirstOrDefault();
            return beneficiario;
        }

        public string InsertBeneficiario(DTOBeneficiarios beneficiario)
        {
            try
            {
                Beneficiarios model = new Beneficiarios()
                {
                    nombre = beneficiario.nombre,
                    apellidoMaterno = beneficiario.apellidoMaterno,
                    apellidoPaterno = beneficiario.apellidoPaterno,
                    empleadoId = beneficiario.empleadoId,
                    parentescoId = beneficiario.parentescoId
                };
                dbContext.Beneficiarios.Add(model);
                dbContext.SaveChanges();
                return "Beneficiario insertado con éxito";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string UpdateBeneficiario(DTOBeneficiarios beneficiaro)
        {
            try
            {
                Beneficiarios model = new Beneficiarios();
                model = (from b in dbContext.Beneficiarios
                         where b.idBeneficiario == beneficiaro.idBeneficiario
                         select b).FirstOrDefault();
                if (model == null)
                {
                    return "Error: No se encontró el objecto que se desea actualizar";
                }
                model.nombre = beneficiaro.nombre;
                model.apellidoMaterno = beneficiaro.apellidoMaterno;
                model.apellidoPaterno = beneficiaro.apellidoPaterno;
                model.empleadoId = beneficiaro.empleadoId;
                model.parentescoId = beneficiaro.parentescoId;
                model.idBeneficiario = beneficiaro.idBeneficiario;
                dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbContext.SaveChanges();
                return "Beneficiario actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string DeleteBeneficiario(Guid id)
        {
            try
            {
                Beneficiarios beneficiario = dbContext.Beneficiarios.FirstOrDefault(b => b.idBeneficiario == id);
                if(beneficiario == null)
                {
                    return "Error: no se encontró el objeto que se desea eliminar";
                }
                dbContext.Beneficiarios.Remove(beneficiario);
                dbContext.SaveChanges();
                return "Beneficiario eliminado con éxito";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
