using System.Collections.Generic;
using Engine.Entities.Environmental;
using Engine.Strategies.SeasonStrategies;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Engine.SeasonStrategies
{
    [TestClass]
    public class StandardSeasonCalculatorTests
    {
        private (double, double, double) GetEarlyMediumAndLateTemperature(SeasonalTime initial, ICalculateSeason calculator) =>
            (calculator.CalculateTemperature(initial),
            calculator.CalculateTemperature(new SeasonalTime(initial) {CurrentTime = initial.Length / 2}),
            calculator.CalculateTemperature(new SeasonalTime(initial) {CurrentTime = initial.Length}));

        [TestMethod]
        public void GetSeasonOfNextTick_CurrentTime0_IsSameSeason()
        {
            // arrange
            var calculator = new StandardSeasonCalculator();
            var currentSeasonalTime = SeasonalTime.Spring;

            // act
            var result = calculator.GetSeasonOfNextTick(currentSeasonalTime);

            // assert
            Assert.AreEqual(currentSeasonalTime.Id, result.Id);
            Assert.AreEqual(currentSeasonalTime.CurrentTime + 1, result.CurrentTime);
        }

        [TestMethod]
        public void GetSeasonOfNextTick_CurrentTimeMax_IsNextSeason()
        {
            // arrange
            var calculator = new StandardSeasonCalculator();
            var currentSeasonalTime = new SeasonalTime {CurrentTime = int.MaxValue, Id = Season.Spring, Length = 30};

            // act
            var result = calculator.GetSeasonOfNextTick(currentSeasonalTime);

            // assert
            Assert.AreEqual(Season.Summer, result.Id);
            Assert.AreEqual(0, result.CurrentTime);
        }

        [TestMethod]
        public void CalculateTemperature_Spring_IsLinearRising()
        {
            // arrange
            var calculator = new StandardSeasonCalculator();

            // act
            var (early, medium, late) = GetEarlyMediumAndLateTemperature(SeasonalTime.Spring, calculator);

            // assert
            Assert.IsTrue(early < medium);
            Assert.IsTrue(medium < late);
        }

        [TestMethod]
        public void CalculateTemperature_Summer_IsRisingThenSinking()
        {
            // arrange
            var calculator = new StandardSeasonCalculator();

            // act
            var (early, medium, late) = GetEarlyMediumAndLateTemperature(SeasonalTime.Summer, calculator);

            // assert
            Assert.IsTrue(early < medium);
            Assert.IsTrue(medium > late);
        }

        [TestMethod]
        public void CalculateTemperature_Autumn_IsLinearSinking()
        {
            // arrange
            var calculator = new StandardSeasonCalculator();

            // act
            var (early, medium, late) = GetEarlyMediumAndLateTemperature(SeasonalTime.Autumn, calculator);

            // assert
            Assert.IsTrue(early > medium);
            Assert.IsTrue(medium > late);
        }

        [TestMethod]
        public void CalculateTemperature_Winter_IsSinkingThenRising()
        {
            // arrange
            var calculator = new StandardSeasonCalculator();

            // act
            var (early, medium, late) = GetEarlyMediumAndLateTemperature(SeasonalTime.Winter, calculator);

            // assert
            Assert.IsTrue(early > medium);
            Assert.IsTrue(medium < late);
        }
    }
}