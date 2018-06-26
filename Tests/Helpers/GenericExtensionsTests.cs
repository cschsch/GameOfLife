using GameOfLife.Entities;
using GameOfLife.Entities.Builder;
using GameOfLife.Helpers.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Helpers
{
    [TestClass]
    public class GenericExtensionsTests
    {
        [TestMethod]
        public void SetProperties_SameValues_StaysSame()
        {
            // arrange
            var cell = new CellBuilder()
                .WithAlive(true)
                .WithDiet(DietaryRestrictions.Herbivore)
                .WithLifetime(5)
                .Create();
            var cellToCopy = new CellBuilder()
                .WithAlive(true)
                .WithDiet(DietaryRestrictions.Herbivore)
                .WithLifetime(5)
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
            var cell = new CellBuilder()
                .WithAlive(true)
                .WithDiet(DietaryRestrictions.Herbivore)
                .WithLifetime(5)
                .Create();
            var cellToCopy = new CellBuilder()
                .WithAlive(false)
                .WithDiet(DietaryRestrictions.Carnivore)
                .WithLifetime(0)
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