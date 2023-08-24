using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Model.Data
{
    public class PostalCode
    {
        [Key]
        [StringLength(10)]
        [Column("PostalCode")]
        public string PostalCodeValue { get; set; }

        [Required]
        [StringLength(50)]
        public string TaxCalculationType { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
