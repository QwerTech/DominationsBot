using System.Collections;
using System.Drawing;
using System.IO;
using System.Linq;
using DominationsBot;
using DominationsBot.Extensions;
using DominationsBot.Services.GameProcess;
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
                        TestName = "GoldOrFood_"+fileNameWithoutExtension,
                        ExpectedResult = expectedResult
                    };
                    return testCaseData;
                });
                var levelPath = Path.Combine(Settings.BasePath, "Resources\\NumbersRead\\Level");
                var levelTestCaseDatas =
                    Directory.EnumerateFiles(levelPath, "*.png", SearchOption.TopDirectoryOnly).Select(f =>
                    {
                        var bitmap = new Bitmap(f);
                        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(f);
                        var expectedResult = int.Parse(fileNameWithoutExtension);
                        var testCaseData = new TestCaseData(bitmap, NumberResourcesType.Level)
                        {
                            TestName = "Level_"+fileNameWithoutExtension,
                            ExpectedResult = expectedResult
                        };
                        return testCaseData;
                    });
                
                return testCaseDatas.Union(new[]
                {
                    new TestCaseData(Resources.Screens.Battle.F1Bmp.GetSubImage(
                            WindowStaticPositions.Battle.OpponentGold), NumberResourcesType.Gold)
                    {
                        ExpectedResult = 12900, TestName = "OpponentGold_12900"
                    },
                    new TestCaseData(Resources.Screens.Battle.F1Bmp.GetSubImage(
                            WindowStaticPositions.Battle.OpponentFood), NumberResourcesType.Food)
                    {
                        ExpectedResult = 20000, TestName = "OpponentFood_20000"
                    },
                    new TestCaseData(Resources.Screens.Battle.F2Bmp.GetSubImage(
                            WindowStaticPositions.Battle.OpponentGold), NumberResourcesType.Gold)
                    {
                        ExpectedResult = 927, TestName = "OpponentFood_927"
                    },
                    new TestCaseData(Resources.Screens.Battle.F2Bmp.GetSubImage(
                            WindowStaticPositions.Battle.OpponentFood), NumberResourcesType.Food)
                    {
                        ExpectedResult = 943, TestName = "OpponentFood_943"
                    },
                    new TestCaseData(Resources.Screens.Battle.F2Bmp.GetSubImage(
                            WindowStaticPositions.Battle.FirstTroopsCount), NumberResourcesType.BeforeBattleTroops)
                    {
                        ExpectedResult = 8, TestName = "FirstTroopsCount_8"
                    }
                }).Union(levelTestCaseDatas);
            }
        }
        [TestCaseSource(nameof(Images))]
        [Test]
        public int ReadTest(Bitmap bitmap, NumberResourcesType resourcesType)
        {
            var textReader = _container.GetInstance<NumberReader>();

            return textReader.Read(bitmap, resourcesType).Value;
        }
    }
}