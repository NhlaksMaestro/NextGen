using Microsoft.EntityFrameworkCore;
using NextGen.Contract.Repository;
using NextGen.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Repository
{
    public class RateRepository : BaseRepository<Rate>, IRateRepository, IRepository<Rate>
    {
        public RateRepository(TaxCalculatorDbContext dbContext)
          : base(dbContext)
        {
        }

        public async Task<Rate?> GetUserRateForIncome(Decimal annualIncome)
        {
            List<Rate> ratesInRange = await Query().Where(r => annualIncome >= r.From).ToListAsync();
            foreach (Rate rate1 in ratesInRange)
            {
                Rate rate = rate1;
                int rangeTo = int.Parse(rate.To.Split(' ')[0]);
                if (annualIncome <= rangeTo)
                    return rate;
            }
            return null;
        }
    }
}
