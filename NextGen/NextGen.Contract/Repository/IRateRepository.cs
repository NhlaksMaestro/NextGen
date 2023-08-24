using NextGen.Model.Data;

namespace NextGen.Contract.Repository
{
    public interface IRateRepository : IRepository<Rate>
    {
        Task<Rate?> GetUserRateForIncome(decimal annualIncome);
    }
}
