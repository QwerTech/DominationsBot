using DominationsBot.DI;
using DominationsBot.Services.ImageProcessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Tests.Tests
{
    [TestClass]
    public class NumberReadTests
    {
        private Container _container;


        public NumberReadTests()
        {
            _container = new Container(new RootRegistry());
        }

        [TestMethod]
        public void GoldAndMoneyRecognition()
        {
            var foodReader = _container.With<ICurrentResourcesType>(new ResourcesType(NumberResourcesType.Food)).GetInstance<TextReader>();
            var goldReader = _container.With<ICurrentResourcesType>(new ResourcesType(NumberResourcesType.Gold)).GetInstance<TextReader>();

            var int1790024 = goldReader.Read(TestScreens._1790024);
            var int819444 = foodReader.Read(TestScreens._819444);

            Assert.AreEqual("1790024", int1790024);
            Assert.AreEqual("819444", int819444);
        }

        [TestMethod]
        public void CitizensRecognition()
        {
            var reader = _container.With<ICurrentResourcesType>(new ResourcesType(NumberResourcesType.Citizens)).GetInstance<TextReader>();

            var text4_12 = reader.Read(TestScreens._4_12);

            Assert.AreEqual("4/12", text4_12);
        }


        [TestMethod]
        public void LevelRecognition()
        {
            var reader = _container.With<ICurrentResourcesType>(new ResourcesType(NumberResourcesType.Level)).GetInstance<TextReader>();
            var int96 = reader.Read(TestScreens._96);

            Assert.AreEqual("96", int96);
        }
    }
}