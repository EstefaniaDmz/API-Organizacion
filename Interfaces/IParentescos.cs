using DTO;

namespace API_Organizacion.Interfaces
{
    public interface IParentescos
    {
        List<DTOParentescos> GetParentescos();

        DTOParentescos GetParentesco(Guid id);

        string InsertParentesco(DTOParentescos parentesco);

        string UpdateParentesco(DTOParentescos parentesco);

        string DeleteParentesco(Guid id);
    }
}
