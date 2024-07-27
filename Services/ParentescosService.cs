using API_Organizacion.Interfaces;
using API_Organizacion.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace API_Organizacion.Services
{
    public class ParentescosService : IParentescos
    {
        private readonly OrganizacionContext dbContext;

        public ParentescosService(OrganizacionContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<DTOParentescos> GetParentescos()
        {
            List<DTOParentescos> parentescosList = new List<DTOParentescos>();
            parentescosList = (from p in dbContext.Parentescos
                               select new DTOParentescos() { 
                                   idParentesco = p.idParentesco,
                                   descripcion = p.descripcion
                               }).ToList();
            return parentescosList;
        }

        public DTOParentescos GetParentesco(Guid id)
        {
            DTOParentescos parentesco = new DTOParentescos();
            parentesco = (from p in dbContext.Parentescos
                          where p.idParentesco == id
                          select new DTOParentescos()
                          {
                              idParentesco = p.idParentesco,
                              descripcion = p.descripcion
                          }).FirstOrDefault();
            return parentesco;
        }

        public string InsertParentesco(DTOParentescos parentesco)
        {
            try
            {
                Parentescos model = new Parentescos();
                model.descripcion = parentesco.descripcion;
                dbContext.Parentescos.Add(model);
                dbContext.SaveChanges();
                return "Parentesco insertado con éxito";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string UpdateParentesco(DTOParentescos parentesco)
        {
            try
            {
                Parentescos model = new Parentescos();
                model = (from p in dbContext.Parentescos
                         where p.idParentesco == parentesco.idParentesco
                         select p).FirstOrDefault();
                if (model == null)
                {
                    return "Error: No se encontró el objecto que se dese actualizar";
                }
                model.descripcion = parentesco.descripcion;

                dbContext.Entry(model).State = EntityState.Modified;
                dbContext.SaveChanges();
                return "Parentesco actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string DeleteParentesco(Guid id)
        {
            try
            {
                Parentescos parentesco = dbContext.Parentescos.FirstOrDefault(p => p.idParentesco == id);
                if (parentesco == null)
                {
                    return "Error: No se encontró el objeto que se desea eliminar";
                }

                dbContext.Parentescos.Remove(parentesco);
                dbContext.SaveChanges();
                return "Parentesco eliminado con éxito";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
