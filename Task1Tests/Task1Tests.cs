using System.Threading;
using System.Threading.Tasks;
using AsyncAwait.Task1.CancellationTokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task1Tests
{
    [TestClass]
    public class Task1Tests
    {
        [TestMethod]
        public async Task TestCalculator_ShouldReturnNumbers_WhenNotCanceled()
        {
            //Arrange
            var passParameter1 = 5;
            long result1;

            //Act
            result1 = await Calculator.CalculateAsync(passParameter1, CancellationToken.None);

            //Assert
            Assert.AreEqual(result1, 15);
        }

        [TestMethod]
        public async Task TestCalculator_ShouldBeMarkedAsCanceled_WhenCanceled()
        {
            //Arrange
            var passParameter1 = 666;
            Task t1;
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            //Act
            t1 =  Calculator.CalculateAsync(passParameter1, token);
            Thread.Sleep(500);
            tokenSource.Cancel();
            await t1;

            //Assert
            Assert.AreEqual(token.IsCancellationRequested, true);

        }
    }
}
