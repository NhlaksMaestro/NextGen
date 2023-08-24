using Microsoft.EntityFrameworkCore;
using NextGen.Contract.Repository;
using NextGen.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NextGen.Repository
{
    public class RateRepository : BaseRepository<Rate>, IRateRepository, IRepository<Rate>
    {
        public RateRepository(TaxCalculatorDbContext dbContext)
          : base(dbContext)
        {
        }

        public async Task<Rate?> GetUserRateForIncome(decimal annualIncome)
        {
            List<Rate> ratesInRange = await Query().Where(r => annualIncome >= r.From).ToListAsync();
            foreach (Rate rateModel in ratesInRange)
            {
                int rangeTo = 0;
                if (int.TryParse(rateModel.To.Split(' ')[0], out rangeTo))
                {
                    if (annualIncome <= rangeTo)
                        return rateModel;
                }
                else
                {
                    return rateModel;
                }
            }
            return null;
        }
    }
}
