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
        private readonly IAssistant _manualAssistant;
        private readonly Mock<ISupportService> _supportServiceMock = new Mock<ISupportService>();
        public Task2Tests()
        {
            _manualAssistant = new ManualAssistant(_supportServiceMock.Object);
        }
        [TestMethod]
        public async Task TestAssistant()
        {
            //Arrange
            var data = "test";
            _supportServiceMock.Setup(x => x.RegisterSupportRequestAsync(data));
            _supportServiceMock.Setup(x => x.GetSupportInfoAsync(data)).ReturnsAsync("12345");
            string result;

            //Act
            result = await _manualAssistant.RequestAssistanceAsync(data);

            //Assert
            Assert.AreEqual(result, "12345");
        }
    }
}
