using Engine.Helpers.Functions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Engine.Helpers
{
    [TestClass]
    public class DoubleExtensionTests
    {
        [TestMethod]
        public void DivideSkipZeroDivisor_WithNonZero_IsNormal()
        {
            // arrange
            const double dividend = 1;
            const double divisor = 3;

            // act
            var result = dividend.DivideSkipZeroDivisor(divisor);

            // assert
            Assert.AreEqual(dividend / divisor, result);
        }

        [TestMethod]
        public void DivideSkipZeroDivisor_WithZero_IsZero()
        {
            // arrange
            const double dividend = 1;
            const double divisor = 0;

            // act
            var result = dividend.DivideSkipZeroDivisor(divisor);

            // assert
            Assert.AreEqual(0, result);
            Assert.AreNotEqual(dividend / divisor, result);
        }
    }
}