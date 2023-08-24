using Microsoft.EntityFrameworkCore;
using NextGen.Contract.Repository;
using NextGen.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Repository
{
    public class PostalCodeRepository :
    BaseRepository<PostalCode>,
    IPostalCodeRepository,
    IRepository<PostalCode>
    {
        public PostalCodeRepository(TaxCalculatorDbContext dbContext)
          : base(dbContext)
        {
        }

        public async Task<PostalCode?> GetPostalCode(string postalCode)
        {
            PostalCode? postalCode1 = await Query().FirstOrDefaultAsync(pc => pc.PostalCodeValue.Equals(postalCode));
            return postalCode1;
        }
    }
}
