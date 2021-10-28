using System.Threading.Tasks;
using AsyncAwait.Task2.CodeReviewChallenge.Models.Support;
using CloudServices;
using CloudServices.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Task2Tests
{
    [TestClass]
    public class Task2Tests
    {
        [TestMethod]
        public async Task TestAssistant()
        {
            //Arrange
            var data = "test";
            var supportServiceMock = new Mock<ISupportService>();
            var manualAssistant = new ManualAssistant(supportServiceMock.Object);
            supportServiceMock.Setup(x => x.RegisterSupportRequestAsync(data));
            supportServiceMock.Setup(x => x.GetSupportInfoAsync(data)).ReturnsAsync("12345");
            string result;

            //Act
            result = await manualAssistant.RequestAssistanceAsync(data);

            //Assert
            Assert.AreEqual(result, "12345");
        }
    }
}
