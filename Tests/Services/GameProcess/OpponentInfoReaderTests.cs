using NUnit.Framework;
using DominationsBot.Services.GameProcess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominationsBot.Extensions;
using DominationsBot.Services.ImageProcessing.TextReading;
using StructureMap;
using Tests;

namespace DominationsBot.Services.GameProcess.Tests
{
    [TestFixture()]
    public class OpponentInfoReaderTests
    {
        private static IEnumerable Images
        {
            get
            {
                var battlePath = Path.Combine(Settings.BasePath, "Resources\\Screens\\Battle");
                var opponentInfo3 = new OpponentInfo{Gold = 73300,Food = 57500, Level = 43};
                var opponentInfo2 = new OpponentInfo{Gold = 927,Food = 943, Level = 4};
                var opponentInfo1 = new OpponentInfo {Gold = 12900,Food = 20000, Level = 20};
                return new[]
                {
                    new TestCaseData(
                        new Bitmap(Path.Combine(battlePath, "1.png")))
                    {
                        TestName = "Opponent_"+opponentInfo1,ExpectedResult = opponentInfo1.ToString()
                    },
                    new TestCaseData(
                        new Bitmap(Path.Combine(battlePath, "2.png")))
                    {
                        TestName = "Opponent_"+opponentInfo2,ExpectedResult = opponentInfo2.ToString()
                    },
                    new TestCaseData(new Bitmap(Path.Combine(battlePath, "3.png")) )
                    {
                        TestName = "Opponent_"+opponentInfo3,ExpectedResult = opponentInfo3.ToString()
                    }
                };
            }
        }

        

        [TestCaseSource(nameof(Images))]
        [Test()]
        public string ReadOpponentNumbersTest(Bitmap bitmap)
        {
            var testRootRegistry = new TestRootRegistry();
            testRootRegistry.ScreenCaptureMock.Setup(capture => capture.SnapShot()).Returns(bitmap);
            IContainer _container = new Container(testRootRegistry);
            
            var opponentInfoReader = _container.GetInstance<OpponentInfoReader>();
            var readenInfo = opponentInfoReader.Read();
            return readenInfo.ToString();
        }
    }
}