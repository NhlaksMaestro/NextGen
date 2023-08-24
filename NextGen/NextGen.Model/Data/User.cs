using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Model.Data
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public Decimal EarningPerMonth { get; set; }

        public Decimal? EarningPerYear { get; set; }

        public string RatePercentage { get; set; }

        public int? RateId { get; set; }

        public string PostalCodeId { get; set; }

        [ForeignKey("RateId")]
        public Rate Rate { get; set; }

        [ForeignKey("PostalCodeId")]
        public PostalCode PostalCode { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
