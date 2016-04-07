using System.Drawing;
using DominationsBot.DI;
using DominationsBot.Services.ImageProcessing.ImageComporators;
using NUnit.Framework;
using StructureMap;

namespace Tests.Tests
{
    [TestFixture]
    public class ImageComparatorTests
    {
        private readonly IContainer _container = new Container(new RootRegistry());

        private static readonly TestCaseData[] Images =
        {
            new TestCaseData(TestScreens.Battle_1, TestScreens.Battle_2, true) {TestName = "Battle"},
            new TestCaseData(TestScreens.Food_1, TestScreens.Food_2, false) {TestName = "Food"},
            new TestCaseData(TestScreens.sshot_1, TestScreens.sshot_2, false) {TestName = "sshot"},
            new TestCaseData(TestScreens.Store_1, TestScreens.Store_2, true) {TestName = "Store"},
            new TestCaseData(TestScreens.screen_1, TestScreens.screen_2, true) {TestName = "screen"},
            new TestCaseData(TestScreens.simple_1, TestScreens.simple_2, false) {TestName = "simple"},
            new TestCaseData(TestScreens.space_1, TestScreens.space_2, false) {TestName = "space"}
        };

        [Test]
        [TestCaseSource(nameof(Images))]
        public void Compare(Bitmap one, Bitmap another, bool result)
        {
            var actual = _container.GetInstance<ByteLevelComparer>().Compare(one, another);
            Assert.That(actual, Is.EqualTo(result));
        }
    }
}