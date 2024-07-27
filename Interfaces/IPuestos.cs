using DTO;

namespace API_Organizacion.Interfaces
{
    public interface IPuestos
    {
        List<DTOPuestos> GetPuestos();

        DTOPuestos GetPuesto(Guid id);

        string InsertPuesto(DTOPuestos puesto);

        string UpdatePuesto(DTOPuestos puesto);

        string DeletePuesto(Guid id);

    }
}
