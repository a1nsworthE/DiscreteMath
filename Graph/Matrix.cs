using System.Collections;

namespace Graph;

public interface IMatrix<T> : IEnumerable<T>
{
    public int CountRows { get; }
    public int CountColumn { get; }

    public T this[int iRow, int iColumn] { get; set; }
}