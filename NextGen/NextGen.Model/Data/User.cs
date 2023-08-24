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
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Invalid email format.")]

        public string Email { get; set; }

        public decimal? EarningPerMonth { get; set; }

        [Required]
        public decimal EarningPerYear { get; set; }
        [Required]
        public string RatePercentage { get; set; }

        public int? RateId { get; set; }

        [Required]
        public string PostalCodeId { get; set; }

        [ForeignKey("RateId")]
        public Rate Rate { get; set; }

        [ForeignKey("PostalCodeId")]
        public PostalCode PostalCode { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
