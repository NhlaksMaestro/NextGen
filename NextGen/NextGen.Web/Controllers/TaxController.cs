using Microsoft.AspNetCore.Mvc;
using NextGen.Contract.Domain;
using NextGen.Model.Data;
using NextGen.Model.Enum;
using NextGen.Model.ViewModel;
using System.Runtime.CompilerServices;

namespace NextGen.Web.Controllers
{
    public class TaxController : BaseController
    {
        private readonly ITaxCalculationDomain _taxCalculationDomain;
        private readonly IUserDomain _userDomain;

        public TaxController(
          ITaxCalculationDomain taxCalculationDomain,
          IUserDomain userDomain,
          ILogger<BaseController> logger)
          : base(logger)
        {
            _taxCalculationDomain = taxCalculationDomain;
            _userDomain = userDomain;
        }

        public async Task<IActionResult> Index()
        {
            var calculationViewModel = new TaxCalculationViewModel(); 
            List<PostalCode> postalCodeList = await _taxCalculationDomain.GetPostalCodes();
            calculationViewModel.PostalCodes = postalCodeList;
            return View(calculationViewModel); 
        }

        [HttpPost]
        public async Task<IActionResult> CalculateTax(TaxCalculationViewModel viewModel)
        {
            viewModel.TaxCalculationType = await _taxCalculationDomain.GetLookupTaxCalculationType(viewModel.PostalCode.Trim());
            if (viewModel.TaxCalculationType == TaxCalculationTypeEnum.Progressive)
            {
                (decimal, int, decimal) calculatedTax = await _taxCalculationDomain.CalculateProgressiveTaxAnnual(viewModel.EarningPerYear);
                viewModel.RateId = calculatedTax.Item2;
                viewModel.CalculatedTax = calculatedTax.Item1;
                viewModel.RatePercentage = $"{calculatedTax.Item3}";
                viewModel.Id = await _userDomain.SaveUserInfo(viewModel);
                return PartialView("_TaxResultPartial", viewModel);
            }
            else if (viewModel.TaxCalculationType == TaxCalculationTypeEnum.FlatRate)
            {
                (decimal, decimal) Results = _taxCalculationDomain.CalculateFlatRateTax(viewModel.EarningPerYear);
                viewModel.CalculatedTax = Results.Item1;
                viewModel.RatePercentage = $"{Results.Item2}";
                viewModel.Id = await _userDomain.SaveUserInfo(viewModel);
                return PartialView("_TaxResultPartial", viewModel);
            }
            else if (viewModel.TaxCalculationType != TaxCalculationTypeEnum.FlatValue)
            {
                viewModel.RateId = 0;
                (decimal, decimal) Results = _taxCalculationDomain.CalculateFlatRateTax(viewModel.EarningPerYear);
                viewModel.CalculatedTax = Results.Item1;
                viewModel.RatePercentage = $"{Results.Item2}";
                viewModel.Id = await _userDomain.SaveUserInfo(viewModel);
                return View("Index", viewModel);
            }
            return PartialView("_TaxResultPartial", viewModel);
        }
    }
}
