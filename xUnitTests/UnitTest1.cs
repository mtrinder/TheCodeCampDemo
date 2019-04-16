using Microsoft.EntityFrameworkCore;
using Moq;
using TheCodeCamp.Data;
using Xunit;

namespace xUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var mockSet = new Mock<DbSet<Camp>>();

            var mockContext = new Mock<CampContext>();
            mockContext.Setup(m => m.Camps).Returns(mockSet.Object);

            var service = new CampRepository(mockContext.Object);
            service.AddCamp(new Camp { CampId = 1, Name = "The camp name" });
            var result = service.SaveChanges();

            mockSet.Verify(m => m.Add(It.IsAny<Camp>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
