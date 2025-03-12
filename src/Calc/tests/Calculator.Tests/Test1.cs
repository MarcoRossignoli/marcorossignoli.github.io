namespace Calculator.Tests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Calculator calculator = new Calculator();
            Assert.AreEqual(4, calculator.Add(2, 2));
        }
    }
}
