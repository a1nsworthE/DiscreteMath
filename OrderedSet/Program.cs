using System.Drawing;
using DiscreteMath;

static class OrderedSet
{
    public static bool DefaultBinaryPredicate(Point point1, Point point2) => point1.X <= point2.X;

    public static IList<Point> CreateSetPoints1(int left = -1, int right = 1)
    {
        if (left > right)
            throw new ArgumentException("right must equal or more then left");

        var points = new List<Point>();

        for (var y = right; y >= left; y--)
        {
            for (var x = left; x <= right; x++)
            {
                points.Add(new Point(x, y));
            }
        }

        return points;
    }

    public static IList<Point> CreateSetPoints2(int left = -2, int right = 2)
    {
        if (left > right)
            throw new ArgumentException("right must equal or more then left");

        var points = new List<Point>();

        points.Add(new Point(0, left));
        for (var x = left + 1; x <= right - 1; x++)
            points.Add(new Point(x, 1));
        for (var x = left; x <= right; x++)
            points.Add(new Point(x, 0));
        for (var x = left + 1; x <= right - 1; x++)
            points.Add(new Point(x, -1));
        points.Add(new Point(0, right));

        return points;
    }

    public static Relations CreateRelationByPredicateFromSetPoints(IList<Point> points,
        Func<Point, Point, bool>? binaryPredicate = null)
    {
        if (points == null) throw new ArgumentNullException(nameof(points));

        binaryPredicate ??= DefaultBinaryPredicate;

        var r = new Relations(points.Count);
        for (var row = 0; row < r.Length; row++)
        {
            for (var column = 0; column < r.Length; column++)
            {
                r[row, column] = binaryPredicate(points[row], points[column]);
            }
        }

        return r;
    }

    public static void MakeDominanceRelation(ref Relations r)
    {
        for (var row = 0; row < r.Length; row++)
            r[row, row] = false;

        for (var row = 0; row < r.Length; row++)
        {
            for (var column = 0; column < r.Length; column++)
            {
                if (r[row, column])
                {
                    for (var z = 0; z < r.Length; z++)
                    {
                        if (r[row, z] && r[z, column])
                        {
                            r[row, column] = false;
                            break;
                        }
                    }
                }
            }
        }
    }

    private static long RelationSumColumn(in Relations r, uint iColumn)
    {
        if (r.Length < iColumn)
            throw new ArgumentException();

        long sum = 0;
        for (var row = 0; row < r.Length; row++)
            sum += Convert.ToInt64(r[row, (int)iColumn]);

        return sum;
    }

    private static long[] RelationArraySumColumns(in Relations r)
    {
        var sumColumns = new long[r.Length];
        for (var column = 0; column < r.Length; column++)
            sumColumns[column] += RelationSumColumn(r, (uint)column);

        return sumColumns;
    }

    private static void SubtractRowByIndex(ref IList<long> source, in Relations r, ulong indexRow)
    {
        if (source.Count != r.Length)
            throw new Exception("Not equal Lenght source and r");
        if ((ulong)r.Length < indexRow)
            throw new ArgumentOutOfRangeException(nameof(indexRow));

        for (var i = 0; i < r.Length; i++)
            source[i] -= Convert.ToInt32(r[(int)indexRow, i]);
    }

    private static void SubtractRowOnValue(ref IList<long> source, int value)
    {
        for (var i = 0; i < source.Count; i++)
            source[i] -= value;
    }

    public static List<List<Point>> TopologicalSort(ref Relations r, IList<Point> points)
    {
        var sumColumns = RelationArraySumColumns(r) as IList<long>;
        var resultOfTopologicalSort = new List<List<Point>>();

        var indicesRows = new Stack<ulong>();

        ulong lvl = 0;
        while (sumColumns.Any(x => x >= 0))
        {
            resultOfTopologicalSort.Add(new List<Point>());
            for (var i = 0; i < sumColumns.Count; i++)
            {
                if (sumColumns[i] == 0)
                {
                    resultOfTopologicalSort[(int)lvl].Add(points[i]);
                    sumColumns[i] = -1;
                    indicesRows.Push((ulong)i);
                }
            }

            while (indicesRows.Count != 0)
            {
                SubtractRowByIndex(ref sumColumns, r, indicesRows.Peek());
                indicesRows.Pop();
            }

            lvl++;
        }

        return resultOfTopologicalSort;
    }

    public static void Main()
    {
        var points1 = CreateSetPoints1();
        var points2 = CreateSetPoints2();
        var r1 = CreateRelationByPredicateFromSetPoints(points1);
        var r2 = CreateRelationByPredicateFromSetPoints(points2);

        for (int row = 0; row < r1.Length; row++)
        {
            for (int column = 0; column < r1.Length; column++)
            {
                Console.Write($"{Convert.ToInt16(r1[row, column])}");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine();

        for (int row = 0; row < r2.Length; row++)
        {
            for (int column = 0; column < r2.Length; column++)
            {
                Console.Write($"{Convert.ToInt16(r2[row, column])}");
            }

            Console.WriteLine();
        }

        MakeDominanceRelation(ref r1);
        MakeDominanceRelation(ref r2);

        for (int row = 0; row < r1.Length; row++)
        {
            for (int column = 0; column < r1.Length; column++)
            {
                Console.Write($"{Convert.ToInt16(r1[row, column])}");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine();

        for (int row = 0; row < r2.Length; row++)
        {
            for (int column = 0; column < r2.Length; column++)
            {
                Console.Write($"{Convert.ToInt16(r2[row, column])}");
            }

            Console.WriteLine();
        }

        var topologicalSort1 = TopologicalSort(ref r1, points1);
        var topologicalSort2 = TopologicalSort(ref r2, points2);

        Console.WriteLine("M1");
        int i = 0;
        foreach (var list in topologicalSort1)
        {
            Console.Write($"{i}:  ");
            foreach (var point in list)
            {
                Console.Write($"{point}");
            }

            i++;
            Console.WriteLine();
        }

        Console.WriteLine();

        Console.WriteLine("M2");
        i = 0;
        foreach (var list in topologicalSort2)
        {
            Console.Write($"{i}:  ");
            foreach (var point in list)
            {
                Console.Write($"{point}");
            }

            i++;
            Console.WriteLine();
        }
    }
}