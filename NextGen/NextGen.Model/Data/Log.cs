using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Model.Data
{
    public class Log
    {
        public int Id { get; set; }

        [Required]
        public DateTime Logged { get; set; }

        [Required]
        [StringLength(5)]
        public string Level { get; set; }

        [Required]
        public string Message { get; set; }

        public string Properties { get; set; }

        public string Exception { get; set; }
    }
}
