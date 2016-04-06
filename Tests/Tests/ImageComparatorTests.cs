using System.Diagnostics;
using System.Drawing;
using DominationsBot.DI;
using DominationsBot.Services.ImageProcessing.ImageComporators;
using NUnit.Framework;
using StructureMap;
using Stopwatch = NUnit.Framework.Compatibility.Stopwatch;

namespace Tests.Tests
{
    [TestFixture]
    public class ImageComparatorTests
    {
        private readonly IContainer _container = new Container(new RootRegistry());

        private static readonly object[] Images =
        {
            //new object[] { TestScreens.Battle_1, TestScreens.Battle_2, true},
            //new object[] { TestScreens.Food_1, TestScreens.Food_2, false},
            //new object[] { TestScreens.sshot_1, TestScreens.sshot_2, false},
            //new object[] { TestScreens.Store_1, TestScreens.Store_2, true},
            //new object[] { TestScreens.screen_1, TestScreens.screen_2, true},
            new object[] { TestScreens.simple_1, TestScreens.simple_2, true},
            //new object[] { TestScreens.space_1, TestScreens.space_2, true}
        };

        [Test]
        [TestCaseSource(nameof(Images))]
        public void Compare(Bitmap one, Bitmap another, bool result)
        {
            var actual = _container.GetInstance<ByteLevelComparer>().GetSimilarityPersent(one, another);
            Assert.That(actual,Is.EqualTo(result));
        }
    }
}