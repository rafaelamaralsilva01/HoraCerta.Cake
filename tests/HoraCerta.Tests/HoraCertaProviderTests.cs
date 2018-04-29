using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace HoraCerta.Tests
{
    [TestClass]
    public class HoraCertaProviderTests
    {
        [TestMethod]
        public void HoraCerta_Now_Success()
        {
            // Arrange
            var expected = DateTime.Now;
            var provider = new HoraCertaProvider(() => expected, () => expected);

            // Act
            var actual = provider.Now();

            // Arrange
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HoraCerta_Now_Fail()
        {
            // Arrange
            var rightNow = DateTime.Now;
            Task.Delay(100);
            var plusAHundredMiliseconds = DateTime.Now;
            var provider = new HoraCertaProvider(
                () => plusAHundredMiliseconds, 
                () => plusAHundredMiliseconds);

            // Act
            var actual = provider.Now();

            // Assert
            Assert.AreNotEqual(rightNow, actual);
        }

        [TestMethod]
        public void HoraCerta_NowUtc_Success()
        {
            // Arrange
            var expected = DateTime.UtcNow;
            var provider = new HoraCertaProvider(() => expected, () => expected);

            // Act
            var actual = provider.Now();

            // Arrange
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HoraCerta_NowUtc_Fail()
        {
            // Arrange
            var rightNow = DateTime.UtcNow;
            Task.Delay(100);
            var plusAHundredMiliseconds = DateTime.UtcNow;
            var provider = new HoraCertaProvider(
                () => plusAHundredMiliseconds,
                () => plusAHundredMiliseconds);

            // Act
            var actual = provider.Now();

            // Assert
            Assert.AreNotEqual(rightNow, actual);
        }
    }
}
