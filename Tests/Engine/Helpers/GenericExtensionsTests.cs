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
                .With(c => c.Diet, DietaryRestrictions.Herbivore)
                .With(c => c.LifeTime, 5)
                .Create();
            var cellToCopy = new EnvironmentalCellBuilder()
                .With(c => c.IsAlive, true)
                .With(c => c.Diet, DietaryRestrictions.Herbivore)
                .With(c => c.LifeTime, 5)
                .Create();

            // act
            cell.SetProperties(cellToCopy);

            // assert
            Assert.AreEqual(cell.IsAlive, true);
            Assert.AreEqual(cell.Diet, DietaryRestrictions.Herbivore);
            Assert.AreEqual(cell.LifeTime, 5);
        }

        [TestMethod]
        public void SetProperties_DifferentValues_IsDifferent()
        {
            // arrange
            var cell = new EnvironmentalCellBuilder()
                .With(c => c.IsAlive, true)
                .With(c => c.Diet, DietaryRestrictions.Herbivore)
                .With(c => c.LifeTime, 5)
                .Create();
            var cellToCopy = new EnvironmentalCellBuilder()
                .With(c => c.IsAlive, false)
                .With(c => c.Diet, DietaryRestrictions.Carnivore)
                .With(c => c.LifeTime, 0)
                .Create();

            // act
            cell.SetProperties(cellToCopy);

            // assert
            Assert.AreEqual(cell.IsAlive, cellToCopy.IsAlive);
            Assert.AreEqual(cell.Diet, cellToCopy.Diet);
            Assert.AreEqual(cell.LifeTime, cellToCopy.LifeTime);
        }
    }
}