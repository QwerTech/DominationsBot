using DominationsBot.DI;
using NUnit.Framework;
using StructureMap;


namespace Tests.Tests
{
    [TestFixture]
    public class IoCTests
    {
        [Test, Explicit]
        public void TestIoCConfiguration()
        {
            new Container(new TestRootRegistry()).AssertConfigurationIsValid();

        }         
    }
}