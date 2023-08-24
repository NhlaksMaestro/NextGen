using NextGen.Model.Data;
using NextGen.Model.Enum;

namespace NextGen.Contract.Domain
{
    public interface ITaxCalculationDomain
    {
        (decimal, decimal) CalculateFlatRateTax(decimal annualIncome);

        (decimal, decimal) CalculateFlatValueTax(decimal annualIncome);

        Task<(decimal, int)> CalculateProgressiveTaxMonthly(decimal annualIncome);

        Task<(decimal, int, decimal)> CalculateProgressiveTaxAnnual(decimal annualIncome);

        Task<TaxCalculationTypeEnum> GetLookupTaxCalculationType(string postalCode);

        Task<List<PostalCode>> GetPostalCodes();
    }
}
