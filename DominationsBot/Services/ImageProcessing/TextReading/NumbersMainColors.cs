using System.Collections.Generic;
using System.Collections.ObjectModel;
using AForge;
using AForge.Imaging.Filters;

namespace DominationsBot.Services.ImageProcessing.TextReading
{
    public class NumbersMainColors : ReadOnlyDictionary<NumberResourcesType, BaseInPlacePartialFilter>
    {
        private static readonly HSLFiltering GoldFoodCitizensFiltering = new HSLFiltering
        {
            Hue = new IntRange(53, 63),
            Luminance = new Range(0.5f, 1)
        };

        private static readonly HSLFiltering LevelFiltering = new HSLFiltering
        {
            Hue = new IntRange(50, 70),
            Luminance = new Range(0.45f, 1),
            Saturation = new Range(0.9f,1)
        };

        public NumbersMainColors() : base(new Dictionary<NumberResourcesType, BaseInPlacePartialFilter>
        {
            {NumberResourcesType.Gold, GoldFoodCitizensFiltering},
            {NumberResourcesType.Food, GoldFoodCitizensFiltering},
            {NumberResourcesType.Citizens, GoldFoodCitizensFiltering},
            {NumberResourcesType.Level, LevelFiltering},
            {NumberResourcesType.BeforeBattleTroops, GoldFoodCitizensFiltering}
        })
        {
        }
    }
}