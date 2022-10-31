using System.Collections.ObjectModel;
using DiscreteMath;

class Factorset
{
    private class ListEqualityComparer : IEqualityComparer<List<int>>
    {
        public bool Equals(List<int>? x, List<int>? y)
        {
            if (x is null && y is null)
                return true;
            
            if (x is null || y is null)
                return false;

            return x.Count == y.Count && x.SequenceEqual(y);
        }

        public int GetHashCode(List<int> obj)
        {
            unchecked
            {
                return obj.Aggregate(19, (current, item) => current * 31 + item.GetHashCode());
            }
        }
    }

    private static List<int> CreateListWithItemsIncludeRows(in Relations r, ulong indexRow)
    {
        if (r.Length < (int)indexRow)
            throw new IndexOutOfRangeException();

        var result = new List<int>();
        for (var column = 1; column <= r.Length; column++)
            if (r[(int)indexRow, column - 1])
                result.Add(column);

        return result;
    }

    public static HashSet<List<int>> MakePartition(in Relations r)
    {
        var result = new HashSet<List<int>>(new ListEqualityComparer());

        for (var row = 0; row < r.Length; row++)
        {
            result.Add(CreateListWithItemsIncludeRows(r, (ulong)row));
        }

        return result;
    }

    public static void Main(string[] args)
    {
        var r = new Relations((1,1),(1,2),(1,4),(2,1),(2,3),(2,4),(3,1),(3,4));

        var result = MakePartition(r);
        
        
        Console.Write('{');
        foreach (var list in result)
        {
            Console.Write('{');
            foreach (var x in list)
            {
                Console.Write($" {x} ");
            }
        
            Console.Write('}');
        }
        
        Console.Write('}');
    }
}