using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using AForge;
using AForge.Imaging.Filters;

namespace DominationsBot.Services.ImageProcessing.TextReading
{
    public class NumbersMainColors : ReadOnlyDictionary<NumberResourcesType, BaseInPlacePartialFilter>
    {
        private static readonly Color GoldFoodCitizensColor = Color.FromArgb(253, 251, 212);
        private static readonly Color LevelColor = Color.FromArgb(255, 235, 0);

        private static BaseInPlacePartialFilter CreateFilter(Color color)
        {
            var delta = 2;
            //var colorFiltering = new ColorFiltering(new IntRange(color.R-delta, color.R + delta), new IntRange(color.G- delta, color.G+ delta), new IntRange(color.B- delta, color.B+ delta));
            var hslFiltering = new HSLFiltering( );
            hslFiltering.Hue = new IntRange(53,63);
            hslFiltering.Luminance =  new Range(0.5f,1);
            return hslFiltering;
        }

        public NumbersMainColors() : base(new Dictionary<NumberResourcesType, BaseInPlacePartialFilter>
        {
            {NumberResourcesType.Gold, CreateFilter(GoldFoodCitizensColor)},
            {NumberResourcesType.Food, CreateFilter(GoldFoodCitizensColor)},
            {NumberResourcesType.Citizens, CreateFilter(GoldFoodCitizensColor)},
            {NumberResourcesType.Level, CreateFilter(LevelColor)}
        })
        {
        }
    }
}