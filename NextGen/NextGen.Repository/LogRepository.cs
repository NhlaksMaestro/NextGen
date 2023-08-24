using NextGen.Contract.Repository;
using NextGen.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Repository
{
    public class LogRepository : BaseRepository<Log>, ILogRepository, IRepository<Log>
    {
        public LogRepository(TaxCalculatorDbContext dbContext)
          : base(dbContext)
        {
        }
    }
}
