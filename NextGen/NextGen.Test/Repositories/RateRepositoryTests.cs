using Microsoft.EntityFrameworkCore;
using Moq;
using NextGen.Contract.Repository;
using NextGen.Model.Data;
using NextGen.Repository;
using NextGen.Test.SetUp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Test.Repository
{
    [TestFixture]
    public class RateRepositoryTests
    {
        private Mock<IRateRepository> _rateRepositoryMock;
        private RateRepository _rateRepository;
        private ITestDataFactory _testDataFactory;

        [SetUp]
        public void Setup()
        {
            _testDataFactory = new TestDataFactory();
            _rateRepositoryMock = new Mock<IRateRepository>();
            _rateRepository = new RateRepository((TaxCalculatorDbContext)_rateRepositoryMock.Object);
        }

        [Test]
        public async Task GetRateById_ShouldReturnRateWhenExists()
        {
            int rateId = 1;
            Rate expectedRate = new Rate()
            {
                Id = rateId,
                RateValue = 10,
                From = 0,
                To = "8350"
            };

            _rateRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddRates().AsQueryable());

            Rate? result = await _rateRepository.Query().FirstOrDefaultAsync(r => r.Id == rateId);
            Assert.That(result, Is.EqualTo(expectedRate));
        }

        [Test]
        public async Task GetRateById_ShouldReturnNullWhenRateNotFound()
        {
            int rateId = 100;

            _rateRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddRates().AsQueryable());

            Rate? result = await _rateRepository.Query().FirstOrDefaultAsync(r => r.Id == rateId);
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetRateByValue_ShouldReturnRateWhenExists()
        {
            int rateValue = 10;
            Rate expectedRate = new Rate()
            {
                Id = 1,
                RateValue = rateValue,
                From = 0,
                To = "8350"
            };

            _rateRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddRates().AsQueryable());

            Rate? result = await _rateRepository.Query().FirstOrDefaultAsync(r => r.RateValue == rateValue);
            Assert.That(result, Is.EqualTo(expectedRate));
        }

        [Test]
        public async Task GetRateByValue_ShouldReturnNullWhenRateNotFound()
        {
            int rateValue = 999;

            _rateRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddRates().AsQueryable());

            Rate? result = await _rateRepository.Query().FirstOrDefaultAsync(r => r.RateValue == rateValue);
            Assert.IsNull(result);
        }
    }
}
