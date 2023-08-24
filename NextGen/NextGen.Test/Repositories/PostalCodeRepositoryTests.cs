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
using NextGen.Contract.Repository;
using NextGen.Repository;
using NextGen.Model.Data;
using Microsoft.EntityFrameworkCore;

namespace NextGen.Test.Repository
{
    public class PostalCodeRepositoryTests
    {
        private Mock<IPostalCodeRepository> _postalCodeRepositoryMock;
        private PostalCodeRepository _postalCodeRepository;
        private ITestDataFactory _testDataFactory;

        [SetUp]
        public void Setup()
        {
            _testDataFactory = new TestDataFactory();
            _postalCodeRepositoryMock = new Mock<IPostalCodeRepository>();
            _postalCodeRepository = new PostalCodeRepository((TaxCalculatorDbContext)_postalCodeRepositoryMock.Object);
        }

        [Test]
        public async Task GetPostalCodeById_ShouldReturnPostalCodeWhenExists()
        {
            string postalCodeId = "7441";
            PostalCode expectedPostalCode = new PostalCode()
            {
                PostalCodeValue = postalCodeId,
                TaxCalculationType = "Progressive"
            };

            var repo = _postalCodeRepositoryMock.Object;
            _postalCodeRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddPostalCodes().AsQueryable());

            PostalCode? result = await _postalCodeRepository.Query()
                .FirstOrDefaultAsync(pc => pc.PostalCodeValue == postalCodeId);

            Assert.That(result, Is.EqualTo(expectedPostalCode));
        }

        [Test]
        public async Task GetPostalCodeById_ShouldReturnNullWhenPostalCodeNotFound()
        {
            string postalCodeId = "9999";

            var repo = _postalCodeRepositoryMock.Object;
            _postalCodeRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddPostalCodes().AsQueryable());

            PostalCode? result = await _postalCodeRepository.Query()
                .FirstOrDefaultAsync(pc => pc.PostalCodeValue == postalCodeId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetPostalCodeByType_ShouldReturnPostalCodeWhenExists()
        {
            string postalCodeType = "Flat Value";
            PostalCode expectedPostalCode = new PostalCode()
            {
                PostalCodeValue = "A100",
                TaxCalculationType = postalCodeType
            };

            var repo = _postalCodeRepositoryMock.Object;
            _postalCodeRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddPostalCodes().AsQueryable());

            PostalCode? result = await _postalCodeRepository.Query()
                .FirstOrDefaultAsync(pc => pc.TaxCalculationType == postalCodeType);

            Assert.That(result, Is.EqualTo(expectedPostalCode));
        }

        [Test]
        public async Task GetPostalCodeByType_ShouldReturnNullWhenPostalCodeNotFound()
        {
            string postalCodeType = "Nonexistent Type";

            var repo = _postalCodeRepositoryMock.Object;
            _postalCodeRepositoryMock.Setup(repo => repo.Query()).Returns(_testDataFactory.AddPostalCodes().AsQueryable());

            PostalCode? result = await _postalCodeRepository.Query()
                .FirstOrDefaultAsync(pc => pc.TaxCalculationType == postalCodeType);

            Assert.IsNull(result);
        }
    }

}
