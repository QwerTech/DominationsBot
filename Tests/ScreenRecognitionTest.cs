using DominationsBot.DI;
using DominationsBot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Tests
{
    [TestClass]
    public class ScreenRecognitionTests
    {
        private readonly IContainer _container = new Container(new RootRegistry());
        [TestMethod]
        public void TestTextsRecognition()
        {
            var textRecognition = _container.GetInstance<TextRecognition>();
            var text1790024 = textRecognition.ImageToText(TestScreens._1790024);
            var text4_12 = textRecognition.ImageToText(TestScreens._4_12);
            var text819444 = textRecognition.ImageToText(TestScreens._819444);
            var text96 = textRecognition.ImageToText(TestScreens._96);

        }
    }
}
