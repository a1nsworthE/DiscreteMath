namespace TestRelations.DiscreteMath;

[TestClass]
public class TestRelations
{
    [TestMethod]
    public void Relations_FromBoolArray1_AreEqual()
    {
        // Arrange
        Relations expected = new Relations(
            (1, 3),
            (2, 2),
            (3, 1)
        );

        // Act
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
        // Arrange
        Relations expected = new Relations(
            (1, 1),
            (3, 3),
            (2, 2)
        );

        // Act
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
        // Arrange
        Relations expected = new Relations(
            (1, 3),
            (2, 2),
            (3, 1)
        );

        // Act
        Relations result = new Relations(new List<(int, int)>{
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
        // Arrange
        Relations expected = new Relations(
            (1, 1),
            (3, 3),
            (2, 2)
        );

        // Act
        Relations result = new Relations(new List<(int, int)>{
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
        // Arrange
        Relations expected = new Relations(10);

        // Act
        Relations result = new Relations(10);


        // Asserts
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Power, expected.Power);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
    }

    [TestMethod]
    public void Relations_Empty_AreNotEqual()
    {
        // Arrange
        Relations expected = new Relations(10);

        // Act
        Relations result = new Relations(4);


        // Asserts
        Assert.AreNotEqual(result, expected);
        Assert.AreEqual(result.Power, expected.Power);
        Assert.AreNotEqual(result.CounterCells, expected.CounterCells);
    }

    [TestMethod]
    public void Relations_FromRelations1_AreEqual()
    {
        // Arrange
        Relations expected = new Relations(
            (1, 3),
            (2, 2),
            (3, 1)
        );

        // Actbool[0,0
        Relations result = new Relations(expected);

        // Asserts
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Power, expected.Power);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
    }

    [TestMethod]
    public void Relations_FromRelations2_AreEqual()
    {
        // Arrange
        Relations expected = new Relations(
            (1, 1),
            (3, 3),
            (2, 2)
        );

        // Act
        Relations result = new Relations(expected);

        // Asserts
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Power, expected.Power);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
    }

    [TestMethod]
    public void Aggregation_EmptyRelation_MustRelationsWithZeroCellsAndLengthAndPower()
    {
        // Arrange
        int expected = 0;

        // Act
        Relations result = new Relations();

        // Asserts
        Assert.AreEqual(result.CounterCells, expected);
        Assert.AreEqual(result.Length, expected);
        Assert.AreEqual(result.Power, expected);
    }

    [TestMethod]
    public void Aggregation_OnlyOnePair_MustCounterCellsEqual4Length2AndPower1()
    {
        // Arrange
        int expected = 4;

        // Act
        Relations result = new Relations((1,2));

        // Asserts
        Assert.AreEqual(result.CounterCells, expected);
        Assert.AreEqual(result.Length, Math.Sqrt(expected));
        Assert.AreEqual(result.Power, 1);
    }

    [TestMethod]
    public void SubSet_TwoEqualRelations_ShouldReturnTrue()
    {
        // Arrange
        Relations expected = new Relations((1,2),(2,1),(9,10),(10,9));

        // Act
        Relations result = new Relations((1,2),(2,1),(9,10),(10,9));

        // Assert
        Assert.IsTrue(result >= expected);
    }

    [TestMethod]
    public void SubSet_TwoAreEmpty_ShouldReturnTrue()
    {
        // Arrange
        Relations expected = new Relations();

        // Act
        Relations result = new Relations();

        // Assert
        Assert.IsTrue(result >= expected);
    }

    [TestMethod]
    public void SubSet_SubSetIsEmpty_ShouldReturnTrue()
    {
        // Arrange
        Relations expected = new Relations((1,2));

        // Act
        Relations result = new Relations();

        // Assert
        Assert.IsTrue(result >= expected);
    }

    [TestMethod]
    public void SubSet_MainSetIsEmpty_ShouldReturnFalse()
    {
        // Arrange
        Relations expected = new Relations();

        // Act
        Relations result = new Relations((1,2),(1,3));

        // Assert
        Assert.IsFalse(result >= expected);
    }

    [TestMethod]
    public void SubSet_SubSetIsLargestLength_ShouldReturnFalse()
    {
        // Arrange
        Relations expected = new Relations((1,2));

        // Act
        Relations result = new Relations((1,2),(1,3));

        // Assert
        Assert.IsFalse(result >= expected);
    }

    [TestMethod]
    public void SubSet_SubSetWithoutOneElement_ShouldReturnTrue()
    {
        // Arrange
        Relations expected = new Relations((1,2),(1,3),(3,4));

        // Act
        Relations result = new Relations((1,2),(1,3));

        // Assert
        Assert.IsTrue(result >= expected);
    }

    [TestMethod]
    public void SubSet_SubSetOnlyOnePairInMainSet_ShouldReturnTrue()
    {
        // Arrange
        Relations expected = new Relations((1,2),(1,3),(3,4),(11,9));

        // Act
        Relations result = new Relations((11,9));

        // Assert
        Assert.IsTrue(result >= expected);
    }

    [TestMethod]
    public void SubSet_SubSetOnlyMaxMinPairInMainSet_ShouldReturnTrue()
    {
        // Arrange
        Relations expected = new Relations((1,2),(1,3),(3,4),(11,9));

        // Act
        Relations result = new Relations((11,9),(1,2));

        // Assert
        Assert.IsTrue(result >= expected);
    }
}