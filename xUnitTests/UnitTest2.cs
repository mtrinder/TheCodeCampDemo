using System;
using NSubstitute;
using Microsoft.EntityFrameworkCore;
using TheCodeCamp.Data;
using Xunit;

namespace xUnitTests
{
    public class UnitTest2
    {
        [Fact]
        public void Test1()
        {
            var repository = Substitute.For<ICampRepository>();


        }
    }
}
