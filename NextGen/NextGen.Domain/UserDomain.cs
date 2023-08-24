using NextGen.Contract.Domain;
using NextGen.Contract.Repository;
using NextGen.Model.Data;
using NextGen.Model.ViewModel;
using NextGen.Repository;

namespace NextGen.Domain
{
    public class UserDomain : IUserDomain
    {
        private readonly IUserRepository _userRepo;

        public UserDomain(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<int> SaveUserInfo(TaxCalculationViewModel user)
        {
            User modelToSave = new User()
            {
                Email = user.Email,
                EarningPerMonth = user.EarningPerMonth,
                EarningPerYear = user.EarningPerYear,
                PostalCodeId = user.PostalCode,
                RatePercentage = user.RatePercentage,
                CreatedDate = DateTime.Now
            };
            if (user.RateId > 0)
                modelToSave.RateId = new int?(user.RateId);
            await _userRepo.AddAsync<User>(modelToSave);
            await _userRepo.SaveChangesAsync();
            int id = modelToSave.Id;
            return id;
        }
    }
}