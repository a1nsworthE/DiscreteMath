using System.Collections;
using DiscreteMath;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;

class ClosureRelations
{
    public static Relations DegreesMerger1(in Relations a)
    {
        var c = (Relations)a.Clone();
        var c2 = Relations.Composition(c, c);

        while (!(c2 >= c))
        {
            c += c2;
            c2 = Relations.Composition(c, c);
        }

        return c;
    }

    public static Relations DegreesMerger2(in Relations a)
    {
        var result = (Relations)a.Clone();
        var exp = Relations.Composition(a, a);
        for (var degree = 2; degree <= a.Length; degree++)
        {
            result += exp;
            exp = Relations.Composition(a, exp);
        }

        return result;
    }

    public static Relations WorshallAlgorithm(in Relations a)
    {
        var result = (Relations)a.Clone();

        for (var z = 0; z < a.Length; z++)
        {
            for (var x = 0; x < a.Length; x++)
            {
                for (var y = 0; y < a.Length && result[x,z]; y++)
                    result[x, y] = result[x, y] || result[x, z] && result[z, y];
            }
        }

        return result;
    }

    private static Relations GenerateRelation(int n, int totalPairs)
    {
        var result = new Relations(n);

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (totalPairs <= 0)
                    break;

                result[new Random().Next(0, n), new Random().Next(0, n)] = true;
            }
        }

        return result;
    }

    public delegate Relations Algorithm(in Relations r);

    private static ulong TimeTest(Algorithm alg, int n, int totalPairs)
    {
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        alg?.Invoke(GenerateRelation(n, totalPairs));
        stopwatch.Stop();

        return (ulong)stopwatch.ElapsedMilliseconds;
    }

    private static IEnumerable Benchmark(Algorithm alg, int nRepeat)
    {
        var (max, min) = ((ulong)0, (ulong.MaxValue));
        for (int n = 50; n <= 150; n += 50)
        {
            for (var i = 0; i < nRepeat; i++)
            {
                var time = TimeTest(alg, n, 1);
                (max, min) = (Math.Max(max, time), Math.Min(min, time));
            }

            yield return (max, min);
            (max, min) = (0, ulong.MaxValue);
        }


        for (int n = 50; n <= 150; n += 50)
        {
            for (var i = 0; i < nRepeat; i++)
            {
                var time = TimeTest(alg, n, (n * n) / 2);
                (max, min) = (Math.Max(max, time), Math.Min(min, time));
            }

            yield return (max, min);
            (max, min) = (0, ulong.MaxValue);
        }


        for (int n = 50; n <= 150; n += 50)
        {
            for (var i = 0; i < nRepeat; i++)
            {
                var time = TimeTest(alg, n, (n * n) / 4);
                (max, min) = (Math.Max(max, time), Math.Min(min, time));
            }

            yield return (max, min);
            (max, min) = (0, ulong.MaxValue);
        }


        for (int n = 50; n <= 150; n += 50)
        {
            for (var i = 0; i < nRepeat; i++)
            {
                var time = TimeTest(alg, n, (n * n * 2) / 3);
                (max, min) = (Math.Max(max, time), Math.Min(min, time));
            }

            yield return (max, min);
            (max, min) = (0, ulong.MaxValue);
        }


        for (int n = 50; n <= 150; n += 50)
        {
            for (var i = 0; i < nRepeat; i++)
            {
                var time = TimeTest(alg, n, n * n);
                (max, min) = (Math.Max(max, time), Math.Min(min, time));
            }

            yield return (max, min);
            (max, min) = (0, ulong.MaxValue);
        }
    }

    public static void Main(string[] args)
    {
        var r = new Relations((1, 1), (1, 3), (1, 5), (2, 1), (2, 3), (2, 5), (3, 1), (3, 4), (3, 2), (4, 1), (4, 5));
        Relations.Output(WorshallAlgorithm(r));
    }
}