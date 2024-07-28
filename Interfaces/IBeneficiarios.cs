using DTO;

namespace API_Organizacion.Interfaces
{
    public interface IBeneficiarios
    {
        List<DTOBeneficiariosVW> GetBeneficiarios();

        DTOBeneficiarios GetBeneficiario(Guid id);

        string InsertBeneficiario(DTOBeneficiarios beneficiario);

        string UpdateBeneficiario(DTOBeneficiarios beneficiario);

        string DeleteBeneficiario(Guid id);
    }
}
