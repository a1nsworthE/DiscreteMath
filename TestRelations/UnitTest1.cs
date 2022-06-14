namespace TestRelations;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        Relations a = new Relations((1,2));
        Relations b = new Relations((1,2));

        Assert.IsTrue(a == b);
    }
}