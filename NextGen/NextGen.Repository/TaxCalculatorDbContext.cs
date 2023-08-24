using Microsoft.EntityFrameworkCore;
using NextGen.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Repository
{
    public class TaxCalculatorDbContext : DbContext
    {
        public TaxCalculatorDbContext(DbContextOptions<TaxCalculatorDbContext> options)
          : base((DbContextOptions)options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<PostalCode> PostalCodes { get; set; }
    }
}
