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
        Assert.AreEqual(result, expected);
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

        // Act
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
        Relations result = new Relations((1, 2));

        // Asserts
        Assert.AreEqual(result.CounterCells, expected);
        Assert.AreEqual(result.Length, Math.Sqrt(expected));
        Assert.AreEqual(result.Power, 1);
    }

    [TestMethod]
    public void SubSet_TwoEqualRelations_ShouldReturnTrue()
    {
        // Arrange
        Relations expected = new Relations((1, 2), (2, 1), (9, 10), (10, 9));

        // Act
        Relations result = new Relations((1, 2), (2, 1), (9, 10), (10, 9));

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
        Relations expected = new Relations((1, 2));

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
        Relations result = new Relations((1, 2), (1, 3));

        // Assert
        Assert.IsFalse(result >= expected);
    }

    [TestMethod]
    public void SubSet_SubSetIsLargestLength_ShouldReturnFalse()
    {
        // Arrange
        Relations expected = new Relations((1, 2));

        // Act
        Relations result = new Relations((1, 2), (1, 3));

        // Assert
        Assert.IsFalse(result >= expected);
    }

    [TestMethod]
    public void SubSet_SubSetWithoutOneElement_ShouldReturnTrue()
    {
        // Arrange
        Relations expected = new Relations((1, 2), (1, 3), (3, 4));

        // Act
        Relations result = new Relations((1, 2), (1, 3));

        // Assert
        Assert.IsTrue(result >= expected);
    }

    [TestMethod]
    public void SubSet_SubSetOnlyOnePairInMainSet_ShouldReturnTrue()
    {
        // Arrange
        Relations expected = new Relations((1, 2), (1, 3), (3, 4), (11, 9));

        // Act
        Relations result = new Relations((11, 9));

        // Assert
        Assert.IsTrue(result >= expected);
    }

    [TestMethod]
    public void SubSet_SubSetOnlyMaxMinPairInMainSet_ShouldReturnTrue()
    {
        // Arrange
        Relations expected = new Relations((1, 2), (1, 3), (3, 4), (11, 9));

        // Act
        Relations result = new Relations((11, 9), (1, 2));

        // Assert
        Assert.IsTrue(result >= expected);
    }

    [TestMethod]
    public void Equals_TwoAreEmptyWithNotEqualLength_ShouldReturnTrue()
    {
        // Arrange
        Relations expected = new Relations(10);

        // Act
        Relations result = new Relations(9);

        // Assert
        Assert.IsTrue(result == expected);
    }

    [TestMethod]
    public void Equals_TwoAreEmptyWithEqualLength_ShouldReturnTrue()
    {
        // Arrange
        Relations expected = new Relations(10);

        // Act
        Relations result = new Relations(10);

        // Assert
        Assert.IsTrue(result == expected);
    }

    [TestMethod]
    public void Equals_TwoAreEmpty_ShouldReturnTrue()
    {
        // Arrange
        Relations expected = new Relations();

        // Act
        Relations result = new Relations();

        // Assert
        Assert.IsTrue(result == expected);
    }

    [TestMethod]
    public void Equals_NotEmptyOnlyOneNotEqualAtBegin_ShouldReturnFalse()
    {
        // Arrange
        Relations expected = new Relations((1, 2), (1, 3), (1, 3), (13, 3));

        // Act
        Relations result = new Relations((1, 6), (1, 3), (1, 3), (13, 3));

        // Assert
        Assert.IsFalse(result == expected);
    }

    [TestMethod]
    public void Equals_NotEmptyOnlyOneNotEqualAtEnd_ShouldReturnFalse()
    {
        // Arrange
        Relations expected = new Relations((1, 2), (1, 3), (1, 3), (13, 3));

        // Act
        Relations result = new Relations((1, 2), (1, 3), (1, 3), (11, 3));

        // Assert
        Assert.IsFalse(result == expected);
    }

    [TestMethod]
    public void Equals_NotEmptyOnlyOneNotEqualAtBeginAndEnd_ShouldReturnFalse()
    {
        // Arrange
        Relations expected = new Relations((1, 2), (1, 3), (1, 3), (13, 3));

        // Act
        Relations result = new Relations((1, 2), (1, 3), (1, 3), (11, 3));

        // Assert
        Assert.IsFalse(result == expected);
    }

    [TestMethod]
    public void Equals_NotEmptyOnlyOneNotEqualAtMiddle_ShouldReturnFalse()
    {
        // Arrange
        Relations expected = new Relations((1, 2), (1, 3), (1, 3), (13, 3));

        // Act
        Relations result = new Relations((1, 2), (1, 3), (1, 4), (13, 3));

        // Assert
        Assert.IsFalse(result == expected);
    }

    [TestMethod]
    public void Equals_LargeLengthAndOneNotEqualAtEnd_ShouldReturnFalse()
    {
        // Arrange
        Relations expected = new Relations(
        (1, 2),
        (1, 3),
        (1, 5),
        (1, 6),
        (2, 7),
        (3, 2),
        (4, 3),
        (5, 5),
        (6, 6),
        (7, 7),
        (10, 11)
        );

        // Act
        Relations result = new Relations(
        (1, 2),
        (1, 3),
        (1, 5),
        (1, 6),
        (2, 7),
        (3, 2),
        (4, 3),
        (5, 5),
        (6, 6),
        (7, 7),
        (10, 12)
        );

        // Assert
        Assert.IsFalse(result == expected);
    }

    [TestMethod]
    public void Union_TwoAreEmpty_ShouldReturnEmptySetWithMaxLength()
    {
        // Arrange
        Relations a = new Relations(10);
        Relations b = new Relations(5);
        Relations expected = new Relations(10);

        // Act
        var result = a + b;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Union_TwoAreEmpty_ShouldReturnEmpty()
    {
        // Arrange
        Relations a = new Relations();
        Relations b = new Relations();
        Relations expected = new Relations();

        // Act
        var result = a + b;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Union_RightIsEmpty_ShouldReturnNotEmpty()
    {
        // Arrange
        Relations a = new Relations((1, 2), (1, 3), (2, 2));
        Relations b = new Relations();
        Relations expected = new Relations((1, 2), (1, 3), (2, 2));

        // Act
        var result = a + b;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Union_LeftIsEmpty_ShouldReturnNotEmpty()
    {
        // Arrange
        Relations b = new Relations((1, 2), (1, 3), (2, 2));
        Relations a = new Relations();
        Relations expected = new Relations((1, 2), (1, 3), (2, 2));

        // Act
        var result = b + a;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Union_TwoAreEqual_ShouldReturnOneOfRelations()
    {
        // Arrange
        Relations b = new Relations((1, 2), (1, 3), (2, 2));
        Relations a = new Relations((1, 2), (1, 3), (2, 2));
        Relations expected = new Relations((1, 2), (1, 3), (2, 2));

        // Act
        var result = b + a;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Union_OneNotEqualAtBegin_ShouldReturnUnionOfRelationsWithTwoNewPair()
    {
        // Arrange
        Relations b = new Relations((1, 1), (1, 3), (2, 2));
        Relations a = new Relations((1, 2), (1, 3), (2, 2));
        Relations expected = new Relations((1, 1), (1, 2), (1, 3), (2, 2));

        // Act
        var result = b + a;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Union_OneNotEqualAtEnd_ShouldReturnUnionOfRelationsWithTwoNewPair()
    {
        // Arrange
        Relations b = new Relations((1, 2), (1, 3), (2, 3));
        Relations a = new Relations((1, 2), (1, 3), (2, 2));
        Relations expected = new Relations((1, 2), (1, 3), (2, 2), (2, 3));

        // Act
        var result = b + a;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Union_OneNotEqualAtBeginAndEnd_ShouldReturnUnionOfRelationsWithFourNewPair()
    {
        // Arrange
        Relations b = new Relations((1, 1), (1, 3), (1, 5), (1, 6), (2, 3));
        Relations a = new Relations((1, 2), (1, 3), (1, 5), (1, 6), (2, 2));
        Relations expected = new Relations((1, 1), (1, 2), (1, 3), (1, 5), (1, 6), (2, 2), (2, 3));

        // Act
        var result = b + a;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Union_TwoNotEqualRelations_ShouldReturnRelationsWithAllElementsOfTwo()
    {
        // Arrange
        Relations b = new Relations((1, 1), (1, 3), (1, 5), (1, 6), (2, 3));
        Relations a = new Relations((1, 2), (3, 3), (2, 5), (2, 6), (2, 2));
        Relations expected = new Relations((1, 1), (1, 2), (1, 3), (1, 5), (1, 6), (2, 2), (2, 3), (3, 3), (2, 5), (2, 6));

        // Act
        var result = b + a;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Intersection_TwoEmpty_ShouldReturnEmpty()
    {
        // Arrange
        Relations b = new Relations();
        Relations a = new Relations();
        Relations expected = new Relations();

        // Act
        var result = a * b;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Intersection_TwoEmpty_ShouldReturnEmptyWithMinLength()
    {
        // Arrange
        Relations b = new Relations(10);
        Relations a = new Relations(3);
        Relations expected = new Relations(3);

        // Act
        var result = a * b;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Intersection_OneIsEmpty_ShouldReturnEmpty_1()
    {
        // Arrange
        Relations a = new Relations((1, 2));
        Relations b = new Relations(10);
        Relations expected = new Relations(10);

        // Act
        var result = a * b;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Intersection_OneIsEmpty_ShouldReturnEmpty_2()
    {
        // Arrange
        Relations a = new Relations((10));
        Relations b = new Relations((1, 2));
        Relations expected = new Relations(10);

        // Act
        var result = a * b;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Intersection_AreTwoEqual_ShouldReturnOneOfRelations()
    {
        // Arrange
        Relations a = new Relations((1, 2), (1, 3), (1, 4), (1, 5));
        Relations b = new Relations((1, 2), (1, 3), (1, 4), (1, 5));
        Relations expected = new Relations((1,2),(1,3),(1,4),(1,5));

        // Act
        var result = a * b;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Intersection_EqualsOnlyAtBegin_ShouldReturnRelationsOfOnePair()
    {
        // Arrange
        Relations a = new Relations((1, 2), (1, 3), (1, 4), (1, 5));
        Relations b = new Relations((1, 2), (2, 3), (6, 4), (8, 5));
        Relations expected = new Relations((1,2));

        // Act
        var result = a * b;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    
    [TestMethod]
    public void Intersection_EqualsOnlyAtEnd_ShouldReturnRelationsOfOnePair()
    {
        // Arrange
        Relations a = new Relations((1, 2), (1, 3), (1, 4), (1, 5));
        Relations b = new Relations((7, 2), (2, 3), (6, 4), (1, 5));
        Relations expected = new Relations((1,5));

        // Act
        var result = a * b;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }

    [TestMethod]
    public void Intersection_EqualsOnlyAtBeginAndEnd_ShouldReturnRelationsOfTwoPair()
    {
        // Arrange
        Relations a = new Relations((1, 2), (1, 3), (1, 4), (1, 5));
        Relations b = new Relations((1, 2), (2, 3), (6, 4), (1, 5));
        Relations expected = new Relations((1,2),(1,5));

        // Act
        var result = a * b;

        // Assert
        Assert.AreEqual(result, expected);
        Assert.AreEqual(result.Length, expected.Length);
        Assert.AreEqual(result.CounterCells, expected.CounterCells);
        Assert.AreEqual(result.Power, expected.Power);
    }
}