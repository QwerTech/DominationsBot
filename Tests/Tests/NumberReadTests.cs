using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Linq;
using DominationsBot;
using DominationsBot.DI;
using DominationsBot.Services.ImageProcessing;
using DominationsBot.Services.ImageProcessing.TextReading;
using NUnit.Framework;
using StructureMap;
using TextReader = DominationsBot.Services.ImageProcessing.TextReading.TextReader;

namespace Tests.Tests
{
    [TestFixture]
    public class NumberReadTests
    {
        private readonly Container _container;


        public NumberReadTests()
        {
            _container = new Container(new TestRootRegistry());
        }

        private static IEnumerable Images
        {
            get
            {
                var path = Path.Combine(Settings.BasePath, "Resources\\NumbersRead");
                var testCaseDatas = Directory.EnumerateFiles(path, "*.png", SearchOption.TopDirectoryOnly).Select(f =>
                {
                    var bitmap = new Bitmap(f);
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(f);
                    var expectedResult = int.Parse(fileNameWithoutExtension);
                    var testCaseData = new TestCaseData(bitmap, NumberResourcesType.Gold)
                    {
                        TestName = fileNameWithoutExtension,
                        ExpectedResult = expectedResult
                    };
                    return testCaseData;
                });
                return testCaseDatas;
            }
        }

        [Test, Explicit] 

        public void CitizensRecognition()
        {
            var textReader = _container.GetInstance<TextReader>();

            var text412 = textReader.Read(TestScreens._4_12, NumberResourcesType.Citizens);

            Assert.AreEqual("4/12", text412);
        }


        [Test, Explicit]
        public void LevelRecognition()
        {
            var reader = _container.GetInstance<TextReader>();
            var int96 = reader.Read(TestScreens._96, NumberResourcesType.Level);

            Assert.AreEqual("96", int96);
        }

        [TestCaseSource(nameof(Images))]
        [Test]
        public int ReadTest(Bitmap bitmap, NumberResourcesType resourcesType)
        {
            var textReader = _container.GetInstance<NumberReader>();

            return textReader.Read(bitmap, resourcesType);
        }
    }
}