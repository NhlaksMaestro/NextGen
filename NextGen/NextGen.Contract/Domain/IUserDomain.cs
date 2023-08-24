using NextGen.Model.ViewModel;

namespace NextGen.Contract.Domain
{
    public interface IUserDomain
    {
        Task<int> SaveUserInfo(TaxCalculationViewModel user);
    }
}
