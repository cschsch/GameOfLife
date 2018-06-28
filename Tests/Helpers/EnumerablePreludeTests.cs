using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife.Helpers.Functions;

namespace Tests.Helpers
{
    [TestClass]
    public class EnumerablePreludeTests
    {
        [TestMethod]
        public void Repeat_Concat_IsConcatRepeated()
        {
            // arrange
            var numbers = Enumerable.Range(1, 5).Select(x => x.ToString());
            var expected = new[] {"12345", "12345", "12345"};

            // act
            var result = EnumerablePrelude.Repeat(() => string.Concat(numbers), 3);

            // assert
            CollectionAssert.AreEqual(expected, result.ToArray());
        }
    }
}