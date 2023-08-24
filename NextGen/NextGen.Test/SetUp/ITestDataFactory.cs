using NextGen.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Test.SetUp
{
    public interface ITestDataFactory
    {
        User CreateUser();

        List<Rate> AddRates();

        List<User> AddUsers();

        List<PostalCode> AddPostalCodes();
    }
}
