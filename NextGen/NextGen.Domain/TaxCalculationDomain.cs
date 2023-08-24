using Microsoft.EntityFrameworkCore;
using NextGen.Contract.Domain;
using NextGen.Contract.Repository;
using NextGen.Model.Data;
using NextGen.Model.Enum;

namespace NextGen.Domain
{
    public class TaxCalculationDomain : ITaxCalculationDomain
    {
        private readonly IRateRepository _rateRepo;
        private readonly IPostalCodeRepository _postalCodeRepo;

        public TaxCalculationDomain(IPostalCodeRepository postalCodeRepo, IRateRepository rateRepo)
        {
            _postalCodeRepo = postalCodeRepo;
            _rateRepo = rateRepo;
        }

        public async Task<(decimal, int, decimal)> CalculateProgressiveTaxAnnual(decimal annualIncome)
        {
            Rate? userRate = await _rateRepo.GetUserRateForIncome(annualIncome);
            decimal totalTax = 0M;
            if (userRate == null)
                return (totalTax, 0, 0M);
            totalTax = annualIncome * userRate.RateValue / 100M;
            return (totalTax, userRate.Id, userRate.RateValue);
        }

        public (decimal, decimal) CalculateFlatValueTax(decimal annualIncome) => annualIncome < 200000M ? (10000M, 0M) : (0.05M * annualIncome, 5M);

        public (decimal, decimal) CalculateFlatRateTax(decimal annualIncome) => (0.175M * annualIncome, 17.5M);

        public async Task<(decimal, int)> CalculateProgressiveTaxMonthly(decimal annualIncome)
        {
            List<Rate> taxRanges = await _rateRepo.Query().OrderBy(r => r.From).ToListAsync();
            Rate? userRate = await _rateRepo.GetUserRateForIncome(annualIncome);
            decimal totalTax = 0M;
            decimal remainingIncome = annualIncome;
            if (userRate == null)
                return (0M, 0);
            foreach (Rate rate in taxRanges)
            {
                Rate taxRange = rate;
                int rangeTo = int.Parse(taxRange.To.Split(' ')[0]);
                decimal taxableIncomeInCurrentRange = Math.Min(remainingIncome, (rangeTo - taxRange.From));
                decimal taxInCurrentRange = taxableIncomeInCurrentRange * taxRange.RateValue / 100M;
                totalTax += taxInCurrentRange;
                remainingIncome -= taxableIncomeInCurrentRange;
                if (remainingIncome <= 0M)
                    break;
            }
            return (totalTax * userRate.RateValue, userRate.Id);
        }

        public async Task<TaxCalculationTypeEnum> GetLookupTaxCalculationType(string postalCode)
        {
            PostalCode? lookupPostalCode = await _postalCodeRepo.GetPostalCode(postalCode);
            TaxCalculationTypeEnum taxCalculationType;
            if (lookupPostalCode == null || !Enum.TryParse(lookupPostalCode.TaxCalculationType.Replace(" ", ""), out taxCalculationType))
                throw new Exception();
            TaxCalculationTypeEnum calculationTypeEnum = taxCalculationType;
            return calculationTypeEnum;
        }

        public async Task<List<PostalCode>> GetPostalCodes()
        {
            List<PostalCode> listAsync = await _postalCodeRepo.Query().ToListAsync();
            return listAsync;
        }
    }
}
