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
using NextGen.Contract.Repository;
using Moq;
using NextGen.Repository;
using NextGen.Model.Data;
using Microsoft.EntityFrameworkCore;

namespace NextGen.Test.Repository
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private UserRepository _userRepository;
        private ITestDataFactory _testDataFactory;

        [SetUp]
        public void Setup()
        {
            _testDataFactory = new TestDataFactory();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userRepository = new UserRepository((TaxCalculatorDbContext)_userRepositoryMock.Object);
        }

        [Test]
        public async Task GetUserByEmailAsync_ShouldReturnUserWhenExists()
        {
            string email = "email2@example.com";
            User expectedUser = new User()
            {
                Id = 1,
                Email = email
            };

            _userRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddUsers().AsQueryable());

            User? result = await _userRepository.Query().FirstOrDefaultAsync(f => f.Email.Equals(email));
            Assert.That(result, Is.EqualTo(expectedUser));
        }

        [Test]
        public async Task GetUserByEmailAsync_ShouldReturnNullWhenUserNotFound()
        {
            string email = "nonexistent@example.com";

            _userRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddUsers().AsQueryable());

            User? result = await _userRepository.Query().FirstOrDefaultAsync(f => f.Email.Equals(email));
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetUserByPostalCodeId_ShouldReturnUserWhenExists()
        {
            string postalCodeId = "7441";
            User expectedUser = new User()
            {
                Id = 1,
                Email = "user1@example.com",
                PostalCodeId = postalCodeId
            };

            _userRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddUsers().AsQueryable());

            User? result = await _userRepository.Query().FirstOrDefaultAsync(f => f.PostalCodeId == postalCodeId);
            Assert.That(result, Is.EqualTo(expectedUser));
        }

        [Test]
        public async Task GetUserByPostalCodeId_ShouldReturnNullWhenUserNotFound()
        {
            string postalCodeId = "nonexistent";

            _userRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddUsers().AsQueryable());

            User? result = await _userRepository.Query().FirstOrDefaultAsync(f => f.PostalCodeId == postalCodeId);
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetUserByIdAsync_ShouldReturnUserWhenExists()
        {
            int userId = 2;
            User expectedUser = new User()
            {
                Id = userId,
                Email = "user2@example.com"
            };

            _userRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddUsers().AsQueryable());

            User? result = await _userRepository.Query().FirstOrDefaultAsync(f => f.Id == userId);
            Assert.That(result, Is.EqualTo(expectedUser));
        }

        [Test]
        public async Task GetUserByEarningPerYear_ShouldReturnUserWhenExists()
        {
            int earningPerYear = 50000;
            User expectedUser = new User()
            {
                Id = 3,
                Email = "user3@example.com",
                EarningPerYear = earningPerYear
            };

            _userRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddUsers().AsQueryable());

            User? result = await _userRepository.Query().FirstOrDefaultAsync(f => f.EarningPerYear == earningPerYear);
            Assert.That(result, Is.EqualTo(expectedUser));
        }

        [Test]
        public async Task GetUserByRateId_ShouldReturnUserWhenExists()
        {
            int rateId = 2;
            User expectedUser = new User()
            {
                Id = 2,
                Email = "user2@example.com",
                RateId = rateId
            };

            _userRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddUsers().AsQueryable());

            User? result = await _userRepository.Query().FirstOrDefaultAsync(f => f.RateId == rateId);
            Assert.That(result, Is.EqualTo(expectedUser));
        }
    }
}
