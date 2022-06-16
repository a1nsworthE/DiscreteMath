namespace TestRelations.DiscreteMath;

[TestClass]
public class TestRelations
{
    [TestMethod]
    public void Relations_FromBoolArray1_AreEqual()
    {
        // Arrange and Act
        Relations expected = new Relations(
            (1,3),
            (2,2),
            (3,1)
        );

        // Arrange
        Relations result = new Relations(new bool[,]{
            {false,false,true},
            {false,true,false},
            {true,false,false},
        });

        // Asserts
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Power, expected.Power);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
    }

    [TestMethod]
    public void Relations_FromBoolArray2_AreEqual()
    {
        // Arrange and Act
        Relations expected = new Relations(
            (1,1),
            (3,3),
            (2,2)
        );

        // Arrange
        Relations result = new Relations(new bool[,]{
            {true,false,false},
            {false,true,false},
            {false,false,true},
        });

        // Asserts
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Power, expected.Power);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
    }

    [TestMethod]
    public void Relations_FromList1_AreEqual()
    {
        // Arrange and Act
        Relations expected = new Relations(
            (1,3),
            (2,2),
            (3,1)
        );

        // Arrange
        Relations result = new Relations(new List<(int,int)>{
            (1,3),
            (2,2),
            (3,1)
        });

        // Asserts
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Power, expected.Power);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
    }

    [TestMethod]
    public void Relations_FromList2_AreEqual()
    {
        // Arrange and Act
        Relations expected = new Relations(
            (1,1),
            (3,3),
            (2,2)
        );

        // Arrange
        Relations result = new Relations(new List<(int,int)>{
            (2,2),
            (3,3),
            (1,1)
        });


        // Asserts
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Power, expected.Power);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
    }
    
    [TestMethod]
    public void Relations_Empty_AreEqual()
    {
        // Arrange and Act
        Relations expected = new Relations(10);

        // Arrange
        Relations result = new Relations(10);


        // Asserts
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Power, expected.Power);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
    }

    [TestMethod]
    public void Relations_Empty_AreNotEqual()
    {
        // Arrange and Act
        Relations expected = new Relations(10);

        // Arrange
        Relations result = new Relations(4);


        // Asserts
        Assert.AreNotEqual(result, expected);
        Assert.AreEqual(result.Power, expected.Power);
        Assert.AreNotEqual(result.CounterCells, expected.CounterCells);
    }
}