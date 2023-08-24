using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Model.Data
{
    public class Rate
    {
        public int Id { get; set; }

        [Column("Rate")]
        public int RateValue { get; set; }

        public int From { get; set; }

        public string To { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
