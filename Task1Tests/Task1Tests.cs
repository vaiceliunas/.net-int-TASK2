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

            var passParameter2 = 7;
            long result2;

            var passParameter3 = 8;
            long result3;

            //Act
            result1 = await Calculator.CalculateAsync(passParameter1, CancellationToken.None);
            result2 = await Calculator.CalculateAsync(passParameter2, CancellationToken.None);
            result3 = await Calculator.CalculateAsync(passParameter3, CancellationToken.None);

            //Assert
            Assert.AreEqual(result1, 15);
            Assert.AreEqual(result2, 28);
            Assert.AreEqual(result3, 36);

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
