using Moq;
using NextGen.Contract.Repository;
using NextGen.Domain;
using NextGen.Model.Data;
using NextGen.Test.SetUp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Test.Domain
{
    public class TaxCalculationDomaintTests
    {
        private TaxCalculationDomain _taxCalculationDomain;
        private ITestDataFactory _testDataFactory;
        private Mock<IRateRepository> _rateRepositoryMock;
        private Mock<IPostalCodeRepository> _postalCodeMock;

        [SetUp]
        public void Setup()
        {
            _testDataFactory = new TestDataFactory();
            _rateRepositoryMock = new Mock<IRateRepository>();
            _postalCodeMock = new Mock<IPostalCodeRepository>();
            _taxCalculationDomain = new TaxCalculationDomain(_postalCodeMock.Object, _rateRepositoryMock.Object);
        }

        [Test]
        public async Task CalculateProgressiveTaxAnnual_ShouldReturnTotalTaxAndRateInfo()
        {
            decimal annualIncome = 60000M;
            Rate userRate = new Rate()
            {
                Id = 1,
                RateValue = 15
            };

            _rateRepositoryMock.Setup(repo => repo.GetUserRateForIncome(annualIncome)).ReturnsAsync(userRate);

            (decimal, int, decimal) valueTuple = await _taxCalculationDomain.CalculateProgressiveTaxAnnual(annualIncome);

            Assert.That(valueTuple.Item1, Is.EqualTo(9000M));
            Assert.That(valueTuple.Item2, Is.EqualTo(1));
            Assert.That(valueTuple.Item3, Is.EqualTo(15));
        }

        [Test]
        public async Task CalculateProgressiveTaxAnnual_ShouldReturnZeroTaxAndZeroRateInfo_WhenUserRateNotFound()
        {
            decimal annualIncome = 60000M;

            _rateRepositoryMock.Setup(repo => repo.GetUserRateForIncome(annualIncome)).ReturnsAsync((Rate?)null);

            (decimal, int, decimal) valueTuple = await _taxCalculationDomain.CalculateProgressiveTaxAnnual(annualIncome);

            Assert.That(valueTuple.Item1, Is.EqualTo(0));
            Assert.That(valueTuple.Item2, Is.EqualTo(0));
            Assert.That(valueTuple.Item3, Is.EqualTo(0));
        }

        [Test]
        public void CalculateFlatValueTax_ShouldReturnTaxAndRateInfoBasedOnIncome()
        {
            (decimal, decimal) flatValueTax = _taxCalculationDomain.CalculateFlatValueTax(150000M);

            Assert.That(flatValueTax.Item1, Is.EqualTo(10000M));
            Assert.That(flatValueTax.Item2, Is.EqualTo(0));
        }
    }
}
