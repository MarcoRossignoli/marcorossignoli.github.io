using System;
using Xunit;

namespace CoverletTest
{
    public class TestClass
    {
        [Fact]
        public void Test()
        {
            CoverletSampleLib.Class1 c = new CoverletSampleLib.Class1();
            c.MethodToTest();
        }       
    }
}
