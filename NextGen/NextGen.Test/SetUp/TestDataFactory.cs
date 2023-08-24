using NextGen.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Test.SetUp
{
    public class TestDataFactory : ITestDataFactory
    {
        private Random _random = new Random();

        public User CreateUser() => new User()
        {
            Id = 1,
            Email = "test@example.com",
            EarningPerMonth = 1000M,
            EarningPerYear = new decimal?(12000),
            RatePercentage = "10%",
            PostalCodeId = "7441",
            CreatedDate = DateTime.Now
        };

        public List<PostalCode> AddPostalCodes() => new List<PostalCode>()
    {
      new PostalCode()
      {
        PostalCodeValue = "7441",
        TaxCalculationType = "Progressive"
      },
      new PostalCode()
      {
        PostalCodeValue = "A100",
        TaxCalculationType = "Flat Value"
      },
      new PostalCode()
      {
        PostalCodeValue = "7441",
        TaxCalculationType = "Progressive"
      },
      new PostalCode()
      {
        PostalCodeValue = "7441",
        TaxCalculationType = "Progressive"
      }
    };

        public List<Rate> AddRates() => new List<Rate>()
    {
      new Rate() { Id = 1, RateValue = 10, From = 0, To = "8350" },
      new Rate()
      {
        Id = 2,
        RateValue = 15,
        From = 8351,
        To = "33950 (0 to 8350 at 10%)"
      },
      new Rate()
      {
        Id = 3,
        RateValue = 25,
        From = 33951,
        To = "82250 (8351 to 33950 - 15%)"
      },
      new Rate()
      {
        Id = 4,
        RateValue = 28,
        From = 82251,
        To = "171550 (33951 - 82250 25%)"
      },
      new Rate()
      {
        Id = 5,
        RateValue = 33,
        From = 171551,
        To = "372950 (82251 - 171550 28%)"
      },
      new Rate()
      {
        Id = _random.Next(123, 556),
        RateValue = 35,
        From = 372951,
        To = "- (171551-372950 33%)')"
      }
    };

        public List<User> AddUsers()
        {
            List<User> userList1 = new List<User>();
            int count = AddRates().Count;
            for (int index = 0; index < count; ++index)
            {
                List<User> userList2 = userList1;
                User user = new User();
                user.Id = _random.Next(123, 556);
                DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 1);
                interpolatedStringHandler.AppendLiteral("email");
                interpolatedStringHandler.AppendFormatted(count);
                interpolatedStringHandler.AppendLiteral("@gmeil.com");
                user.Email = interpolatedStringHandler.ToStringAndClear();
                user.EarningPerMonth = 0M;
                user.EarningPerYear = new decimal?(_random.Next(8000, 400001));
                interpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
                interpolatedStringHandler.AppendFormatted<int>(AddRates()[index].RateValue);
                interpolatedStringHandler.AppendLiteral("%");
                user.RatePercentage = interpolatedStringHandler.ToStringAndClear();
                user.RateId = new int?(AddRates()[count].Id);
                user.PostalCodeId = AddPostalCodes()[_random.Next(1, AddPostalCodes().Count + 1)].PostalCodeValue;
                user.CreatedDate = DateTime.Now;
                userList2.Add(user);
            }
            return userList1;
        }
    }
}
