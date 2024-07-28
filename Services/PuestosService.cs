using API_Organizacion.Interfaces;
using API_Organizacion.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace API_Organizacion.Services
{
    public class PuestosService : IPuestos
    {
        private readonly OrganizacionContext dbContext;

        public PuestosService(OrganizacionContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<DTOPuestos> GetPuestos()
        {
            List<DTOPuestos> puestosList = new List<DTOPuestos>();
            puestosList = (from p in dbContext.Puestos
                           select new DTOPuestos()
                           {
                               idPuesto = p.idPuesto,
                               descripcion = p.descripcion
                           }).ToList();
            return puestosList;
        }

        public DTOPuestos GetPuesto(Guid id)
        {
            DTOPuestos puesto = new DTOPuestos();
            puesto = (from p in dbContext.Puestos
                      where p.idPuesto == id
                      select new DTOPuestos()
                      {
                          idPuesto = p.idPuesto,
                          descripcion = p.descripcion
                      }).FirstOrDefault();
            return puesto;
        }

        public string InsertPuesto(DTOPuestos puesto)
        {
            try
            {
                Puestos model = new Puestos();
                model.descripcion = puesto.descripcion;
                dbContext.Puestos.Add(model);
                dbContext.SaveChanges();
                return "Puesto insertado con éxito";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string UpdatePuesto(DTOPuestos puesto)
        {
            try
            {
                Puestos model = new Puestos();
                model = (from p in dbContext.Puestos
                         where p.idPuesto == puesto.idPuesto
                         select p).FirstOrDefault();
                if (model == null)
                {
                    return "Error: No se encontró el objecto que se desea actualizar";
                }
                model.descripcion = puesto.descripcion;

                dbContext.Entry(model).State = EntityState.Modified;
                dbContext.SaveChanges();
                return "Puesto actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string DeletePuesto(Guid id)
        {
            try
            {
                Puestos puesto = dbContext.Puestos.FirstOrDefault(p => p.idPuesto == id);
                if (puesto == null)
                {
                    return "Error: No se encontró el objeto que se desea eliminar";
                }

                dbContext.Puestos.Remove(puesto);
                dbContext.SaveChanges();
                return "Puesto eliminado con éxito";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
