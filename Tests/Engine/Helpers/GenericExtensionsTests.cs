using Engine.Entities.Environmental;
using Engine.Entities.Environmental.Builders;
using Engine.Helpers.Functions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Engine.Helpers
{
    [TestClass]
    public class GenericExtensionsTests
    {
        [TestMethod]
        public void SetProperties_SameValues_StaysSame()
        {
            // arrange
            var cell = new EnvironmentalCellBuilder()
                .With(c => c.IsAlive, true)
                .With(c => c.Diet, DietaryRestriction.Herbivore)
                .With(c => c.LifeTime, 5)
                .Create();
            var cellToCopy = new EnvironmentalCellBuilder()
                .With(c => c.IsAlive, true)
                .With(c => c.Diet, DietaryRestriction.Herbivore)
                .With(c => c.LifeTime, 5)
                .Create();

            // act
            cell.SetProperties(cellToCopy);

            // assert
            Assert.AreEqual(cell.IsAlive, true);
            Assert.AreEqual(cell.Diet, DietaryRestriction.Herbivore);
            Assert.AreEqual(cell.LifeTime, 5);
        }

        [TestMethod]
        public void SetProperties_DifferentValues_IsDifferent()
        {
            // arrange
            var cell = new EnvironmentalCellBuilder()
                .With(c => c.IsAlive, true)
                .With(c => c.Diet, DietaryRestriction.Herbivore)
                .With(c => c.LifeTime, 5)
                .Create();
            var cellToCopy = new EnvironmentalCellBuilder()
                .With(c => c.IsAlive, false)
                .With(c => c.Diet, DietaryRestriction.Carnivore)
                .With(c => c.LifeTime, 0)
                .Create();

            // act
            cell.SetProperties(cellToCopy);

            // assert
            Assert.AreEqual(cell.IsAlive, cellToCopy.IsAlive);
            Assert.AreEqual(cell.Diet, cellToCopy.Diet);
            Assert.AreEqual(cell.LifeTime, cellToCopy.LifeTime);
        }

        [TestMethod]
        public void GetPropertyName_PrimitiveProperty_IsName()
        {
            // arrange
            const string expected = nameof(TestClass.IAmAProperty);

            // act
            var result = GenericExtensions.GetPropertyName<TestClass, int>(tc => tc.IAmAProperty);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetPropertyName_ComplexProperty_IsName()
        {
            // arrange
            const string expected = nameof(TestClass.MeToo);

            // act
            var result = GenericExtensions.GetPropertyName<TestClass, TestClass>(tc => tc.MeToo);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetPropertyName_Field_IsName()
        {
            // arrange
            const string expected = nameof(TestClass.IAmMostDefinitelyNotAProperty);

            // act
            var result = GenericExtensions.GetPropertyName<TestClass, string>(tc => tc.IAmMostDefinitelyNotAProperty);

            // assert
            Assert.AreEqual(expected, result);
        }
    }
}