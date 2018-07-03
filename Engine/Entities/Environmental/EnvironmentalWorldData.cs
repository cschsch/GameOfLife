﻿using Engine.Helpers;

namespace Engine.Entities.Environmental
{
    public class EnvironmentalWorldData : BaseWorldData<EnvironmentalCell, EnvironmentalCellGrid>
    {
        public double Temperature { get; set; }
        public Density HerbivoreDensity { get; set; }
        public Season Season { get; set; }
    }
}