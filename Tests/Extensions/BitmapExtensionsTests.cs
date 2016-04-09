using System.Drawing;
using DominationsBot.Extensions;
using NUnit.Framework;

namespace Tests.Extensions
{
    [TestFixture]
    public class BitmapExtensionsTests
    {
        public static TestCaseData[] ColorsCompare =
        {
            new TestCaseData(Color.Black, Color.Black) {ExpectedResult = true, TestName = "BlackToBlack"},
            new TestCaseData(Color.Black, Color.FromKnownColor(KnownColor.Black))
            {
                ExpectedResult = true,
                TestName = "BlackToBlack2"
            },
            new TestCaseData(Color.Black, Color.FromArgb(0, 0, 0)) {ExpectedResult = true, TestName = "BlackToBlack"},
            new TestCaseData(Color.Black, Color.White) {ExpectedResult = false, TestName = "BlackToWhite"}
        };

        [Test]
        [TestCaseSource(nameof(ColorsCompare))]
        public bool CompareTest(Color one, Color another)
        {
            return one.Compare(another);
        }
    }
}