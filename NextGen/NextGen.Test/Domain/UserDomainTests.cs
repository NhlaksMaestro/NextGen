using NextGen.Test.SetUp;
using NUnit.Framework.Constraints;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NextGen.Domain;
using NextGen.Contract.Domain;
using NextGen.Contract.Repository;
using NextGen.Repository;
using NextGen.Model.ViewModel;
using NextGen.Model.Data;

namespace NextGen.Test.Domain
{
    public class UserDomainTests
    {
        private Mock<IUserDomain> _userDomainMock;
        private UserDomain _userDomain;
        private ITestDataFactory _testDataFactory;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<TaxCalculatorDbContext> _dbContextMock;

        [SetUp]
        public void Setup()
        {
            _testDataFactory = new TestDataFactory();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userDomainMock = new Mock<IUserDomain>();
            _userDomain = new UserDomain(_userRepositoryMock.Object);
        }

        [Test]
        public async Task SaveUserInfo_ShouldAddUserAndReturnUserId()
        {
            TaxCalculationViewModel taxCalculationViewModel = new TaxCalculationViewModel()
            {
                Email = "user@example.com",
                EarningPerMonth = 5000M,
                EarningPerYear = 60000M,
                PostalCode = "7441",
                RatePercentage = "15%",
                RateId = 2
            };

            var repo = _userRepositoryMock.Object;
            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .Callback<User>(user => user.Id = 1);

            int result = await _userDomain.SaveUserInfo(taxCalculationViewModel);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task SaveUserInfo_ShouldSetRateIdToNullWhenRateIdNotProvided()
        {
            TaxCalculationViewModel taxCalculationViewModel = new TaxCalculationViewModel()
            {
                Email = "user@example.com",
                EarningPerMonth = 5000M,
                EarningPerYear = 60000M,
                PostalCode = "7441",
                RatePercentage = "15%"
            };

            int num = await _userDomain.SaveUserInfo(taxCalculationViewModel);

            _userRepositoryMock.Verify(repo => repo.AddAsync(It.Is<User>(user => user.RateId == null)));
        }

        [Test]
        public async Task SaveUserInfo_ShouldAddUserWithRateId()
        {
            TaxCalculationViewModel taxCalculationViewModel = new TaxCalculationViewModel()
            {
                Email = "user@example.com",
                EarningPerMonth = 5000M,
                EarningPerYear = 60000M,
                PostalCode = "7441",
                RatePercentage = "15%",
                RateId = 2
            };

            var repo = _userRepositoryMock.Object;
            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .Callback<User>(user => user.Id = 1);

            int num = await _userDomain.SaveUserInfo(taxCalculationViewModel);

            _userRepositoryMock.Verify(repo => repo.AddAsync<User>(It.Is<User>(user => user.RateId == 2)));
        }

        [Test]
        public async Task SaveUserInfo_ShouldCallSaveChangesAsync()
        {
            TaxCalculationViewModel taxCalculationViewModel = new TaxCalculationViewModel()
            {
                Email = "user@example.com",
                EarningPerMonth = 5000M,
                EarningPerYear = 60000M,
                PostalCode = "7441",
                RatePercentage = "15%",
                RateId = 2
            };

            var repo = _userRepositoryMock.Object;
            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .Callback<User>(user => user.Id = 1);

            int num = await _userDomain.SaveUserInfo(taxCalculationViewModel);

            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync());
        }

        [Test]
        public async Task SaveUserInfo_ShouldReturnUserId()
        {
            TaxCalculationViewModel taxCalculationViewModel = new TaxCalculationViewModel()
            {
                Email = "user@example.com",
                EarningPerMonth = 5000M,
                EarningPerYear = 60000M,
                PostalCode = "7441",
                RatePercentage = "15%",
                RateId = 2
            };

            var repo = _userRepositoryMock.Object;
            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .Callback<User>(user => user.Id = 1);

            int result = await _userDomain.SaveUserInfo(taxCalculationViewModel);

            Assert.That(result, Is.EqualTo(1));
        }
    }
}
