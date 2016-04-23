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
using DominationsBot.Services.ImageProcessing.TextReading;
using StructureMap;
using Tests;

namespace DominationsBot.Services.GameProcess.Tests
{
    [TestFixture()]
    public class UnzoomingTests
    {
        private static IEnumerable Images
        {
            get
            {
                var path = Path.Combine(Settings.BasePath, "Resources\\Zoom");
                var testCaseDatas = Directory.EnumerateFiles(path, "*.png", SearchOption.TopDirectoryOnly).Select(f =>
                {
                    var bitmap = new Bitmap(f);
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(f);
                    var expectedResult = bool.Parse(fileNameWithoutExtension);
                    var testCaseData = new TestCaseData(bitmap)
                    {
                        TestName = fileNameWithoutExtension,
                        ExpectedResult = expectedResult
                    };
                    return testCaseData;
                });
                return testCaseDatas;
            }
        }

        [Test, TestCaseSource(nameof(Images))]
        public bool WorkabilityTest(Bitmap screen)
        {
            var testRootRegistry = new TestRootRegistry();
            testRootRegistry.ScreenCaptureMock.Setup(capture => capture.SnapShot()).Returns(screen);
            var container = new Container(testRootRegistry);
            var unzooming = container.GetInstance<Unzooming>();

            return unzooming.Workability();
        }
    }
}