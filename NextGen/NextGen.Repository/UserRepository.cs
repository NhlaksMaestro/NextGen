using NextGen.Contract.Repository;
using NextGen.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository, IRepository<User>
    {
        public UserRepository(TaxCalculatorDbContext dbContext)
          : base(dbContext)
        {
        }
    }
}
