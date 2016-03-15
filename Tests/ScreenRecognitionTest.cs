using DominationsBot.DI;
using DominationsBot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Tests
{
    [TestClass]
    public class ScreenRecognitionTests
    {
        private readonly TextRecognition _textRecognition;

        public ScreenRecognitionTests()
        {
            IContainer container = new Container(new RootRegistry());
            _textRecognition = container.GetInstance<TextRecognition>();
        }

        [TestMethod]
        public void GoldAndMoneyRecognition()
        {
            var int1790024 = _textRecognition.GetText<int>(TestScreens._1790024);
            var int819444 = _textRecognition.GetText<int>(TestScreens._819444);

            Assert.AreEqual(1790024, int1790024);
            Assert.AreEqual(819444, int819444);
        }

        [TestMethod]
        public void CitizensRecognition()
        {
            var textRecognition = _textRecognition;

            var text4_12 = textRecognition.GetText(TestScreens._4_12);

            Assert.AreEqual(text4_12, "4/12");
        }

        [TestMethod, Ignore]
        public void LevelRecognition()
        {
            var int96 = _textRecognition.GetText<int>(TestScreens._96);

            Assert.AreEqual(96, int96);
        }
    }
}