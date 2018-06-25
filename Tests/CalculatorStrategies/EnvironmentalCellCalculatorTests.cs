﻿using System;
using System.Linq;
using GameOfLife.Core.CalculatorStrategies;
using GameOfLife.Entities;
using GameOfLife.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Tests.Sut;

namespace Tests.CalculatorStrategies
{
    [TestClass]
    public class EnvironmentalCellCalculatorTests
    {
        private (Cell, WorldData) HerbivoreWithHerbivoreDensityAndTemperature(Density herbivoreDensity, double temperature) =>
            (CellBuilder().WithAlive(true).WithDiet(DietaryRestrictions.Herbivore).Create(),
             WorldDataBuilder().WithHerbivoreDensity(herbivoreDensity).WithTemperature(temperature).Create());

        [TestMethod]
        public void CalculateCell_IsAlive_NoneAlive_Dies() =>
            CellCalculatorBase.CalculateCell(new EnvironmentalCellCalculator(new Random()), Alive(), Enumerable.Empty<Cell>(), new WorldData(),
                cell => !cell.IsAlive,
                cell => cell.LifeTime == 0);

        [TestMethod]
        public void CalculateCell_IsAlive_TwoAlive_StaysAlive() =>
            CellCalculatorBase.CalculateCell(new EnvironmentalCellCalculator(new Random()), Alive(), Enumerable.Repeat(Alive(), 2), new WorldData(),
                cell => cell.IsAlive,
                cell => cell.LifeTime == 2);

        [TestMethod]
        public void CalculateCell_IsAlive_FiveAlive_Dies() =>
            CellCalculatorBase.CalculateCell(new EnvironmentalCellCalculator(new Random()), Alive(), Enumerable.Repeat(Alive(), 5), new WorldData(),
                cell => !cell.IsAlive,
                cell => cell.LifeTime == 0);

        [TestMethod]
        public void CalculateCell_IsDead_ThreeAlive_ComesAlive() =>
            CellCalculatorBase.CalculateCell(new EnvironmentalCellCalculator(new Random()), Dead(), Enumerable.Repeat(Alive(), 3), new WorldData(),
                cell => cell.IsAlive,
                cell => cell.LifeTime == 1);

        [TestMethod]
        public void CalculateCell_IsHerbivore_DensityAndTemperatureMax_BecomesCarnivore()
        {
            var (cell, data) = HerbivoreWithHerbivoreDensityAndTemperature(Density.MaxValue, double.MaxValue);
            var neighbours = Enumerable.Repeat(CellBuilder().WithAlive(true).WithDiet(DietaryRestrictions.Carnivore).Create(), 3);
            CellCalculatorBase.CalculateCell(new EnvironmentalCellCalculator(new Random()), cell, neighbours, data,
                c => c.Diet == DietaryRestrictions.Carnivore);
        }

        [TestMethod]
        public void CalculateCell_IsHerbivore_DensityMaxTemperatureZero_StaysHerbivore()
        {
            var (cell, data) = HerbivoreWithHerbivoreDensityAndTemperature(Density.MaxValue, 0);
            var neighbours = Enumerable.Repeat(CellBuilder().WithAlive(true).WithDiet(DietaryRestrictions.Carnivore).Create(), 3);
            CellCalculatorBase.CalculateCell(new EnvironmentalCellCalculator(new Random()), cell, neighbours, data,
                c => c.Diet == DietaryRestrictions.Herbivore);
        }

        [TestMethod]
        public void CalculateCell_IsHerbivore_DensityMinTemperatureMax_StaysHerbivore()
        {
            var (cell, data) = HerbivoreWithHerbivoreDensityAndTemperature(Density.MinValue, double.MaxValue);
            var neighbours = Enumerable.Repeat(CellBuilder().WithAlive(true).WithDiet(DietaryRestrictions.Carnivore).Create(), 3);
            CellCalculatorBase.CalculateCell(new EnvironmentalCellCalculator(new Random()), cell, neighbours, data,
                c => c.Diet == DietaryRestrictions.Herbivore);
        }

        [TestMethod]
        public void CalculateCell_IsHerbivore_DensityAndTemperatureMin_StaysHerbivore()
        {
            var (cell, data) = HerbivoreWithHerbivoreDensityAndTemperature(Density.MinValue, 0);
            var neighbours = Enumerable.Repeat(CellBuilder().WithAlive(true).WithDiet(DietaryRestrictions.Carnivore).Create(), 3);
            CellCalculatorBase.CalculateCell(new EnvironmentalCellCalculator(new Random()), cell, neighbours, data,
                c => c.Diet == DietaryRestrictions.Herbivore);
        }

        [TestMethod]
        public void CalculateCell_IsHerbivore_DensityAndTemperatureMax_NoCarnivoreNeighbours_StaysHerbivore()
        {
            var (cell, data) = HerbivoreWithHerbivoreDensityAndTemperature(Density.MaxValue, double.MaxValue);
            var neighbours = Enumerable.Empty<Cell>();
            CellCalculatorBase.CalculateCell(new EnvironmentalCellCalculator(new Random()), cell, neighbours, data,
                c => c.Diet == DietaryRestrictions.Herbivore);
        }
    }
}