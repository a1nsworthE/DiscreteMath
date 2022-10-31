using System.Collections;

namespace Graph;

public sealed class Graph : IMatrix<bool>, ICloneable
{
    private bool[,]? _matrix;
    public int CountRows { get; private set; }
    public int CountColumn { get; private set; }

    public Graph() => _matrix = null;

    public Graph(bool[,] matrix)
    {
        _matrix = (bool[,])matrix.Clone();

        CountRows = _matrix.GetLength(0);
        CountColumn = _matrix.GetLength(1);
    }

    public Graph(int nRows, int nColumns) : this(new bool[nRows, nColumns])
    {
    }

    public Graph(IEnumerable<(int, int)> enumerable, int nRows, int nColumns) : this(nRows, nColumns)
    {
        if (_matrix is null)
            throw new NullReferenceException();

        foreach (var pair in enumerable)
            _matrix[pair.Item1 - 1, pair.Item2 - 1] = true;
    }

    public Graph(IEnumerable<(int, int)> enumerable) : this()
    {
        var values = new List<(int, int)>();
        foreach (var tuple in enumerable)
        {
            values.Add(tuple);
            CountRows = Math.Max(CountRows, Math.Max(tuple.Item1, tuple.Item2));
        }

        CountColumn = CountRows;

        _matrix = new bool[CountRows, CountColumn];
        foreach (var tuple in values)
            _matrix[tuple.Item1 - 1, tuple.Item2 - 1] = true;
    }

    public Graph(in Graph graph) : this(graph._matrix ?? throw new NullReferenceException())
    {
    }

    public Graph(params (int, int)[] valuesTuples) : this((IEnumerable<(int, int)>)valuesTuples)
    {
    }

    public bool this[int iRow, int iColumn]
    {
        get => _matrix?[iRow, iColumn] ?? throw new NullReferenceException();
        set
        {
            if (_matrix is null)
                throw new NullReferenceException();
            _matrix[iRow, iColumn] = value;
        }
    }

    public bool[,]? GetData() => _matrix;


    private static void GetListVerticesByLenght(Graph graph, uint beginVertex, uint length, List<uint> selectedVertices,
        ref List<List<uint>> resultVertices)
    {
        if (selectedVertices.Count == length)
        {
            resultVertices.Add(selectedVertices.GetRange(0, selectedVertices.Count));
            selectedVertices.RemoveAt(selectedVertices.Count - 1);
        }
        else
        {
            for (var column = 1; column <= graph.CountColumn; column++)
            {
                if (graph[(int)beginVertex - 1, column - 1])
                {
                    selectedVertices.Add((uint)column);

                    GetListVerticesByLenght(graph, (uint)column, length,
                        selectedVertices, ref resultVertices);
                }
            }

            selectedVertices.RemoveAt(selectedVertices.Count - 1);
        }
    }

    public static List<List<uint>> GetListVerticesByLenght(Graph graph, uint beginVertex, uint length)
    {
        var resultVertices = new List<List<uint>>();
        var selectedVertices = new List<uint> { beginVertex };
        GetListVerticesByLenght(graph, beginVertex, length + 1, selectedVertices, ref resultVertices);

        return resultVertices;
    }

    public object Clone() => new Graph(CountRows, CountRows);

    public IEnumerator<bool> GetEnumerator() =>
        _matrix?.Cast<bool>().GetEnumerator() ?? throw new NullReferenceException();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}