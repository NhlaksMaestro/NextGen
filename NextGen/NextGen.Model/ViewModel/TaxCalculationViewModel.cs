using NextGen.Model.Enum;
using NextGen.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Model.ViewModel
{
    public class TaxCalculationViewModel
    {
        public int? Id { get; set; }

        public string Email { get; set; }

        public string PostalCode { get; set; }

        public Decimal EarningPerMonth { get; set; }

        public Decimal EarningPerYear { get; set; }

        public TaxCalculationTypeEnum TaxCalculationType { get; set; }

        public Decimal CalculatedTax { get; set; }

        public string RatePercentage { get; set; }

        public List<PostalCode> PostalCodes { get; set; }

        public int RateId { get; set; }
    }
}
