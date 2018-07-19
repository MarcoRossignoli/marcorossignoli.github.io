using Xunit;

namespace CoverletTest
{
    public class MyFunTest
    {
        [Fact]
        public void Test2()
        {
            CoverletSampleLib.MyFunnyClass c = new CoverletSampleLib.MyFunnyClass();
            c.Do();
        }
    }
}
