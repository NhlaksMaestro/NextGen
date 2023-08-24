
using NextGen.Model.Data;

namespace NextGen.Contract.Repository
{
    public interface IPostalCodeRepository : IRepository<PostalCode>
    {
        Task<PostalCode?> GetPostalCode(string postalCode);
    }
}
