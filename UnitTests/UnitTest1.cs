using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TheCodeCamp.Data;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void CreateBlog_saves_a_blog_via_context()
        {
            var mockSet = new Mock<DbSet<Camp>>();

            var mockContext = new Mock<CampContext>();
            mockContext.Setup(m => m.Camps).Returns(mockSet.Object);

            var service = new CampRepository(mockContext.Object);
            service.AddCamp(new Camp { CampId = 1, Name= "The camp name" });
            var result = await service.SaveChangesAsync();

            mockSet.Verify(m => m.Add(It.IsAny<Camp>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
