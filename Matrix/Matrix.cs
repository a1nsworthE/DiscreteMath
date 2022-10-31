using System.Collections;

namespace Matrix;

public abstract class Matrix<T> : IEnumerable<T>
{
    protected T[,] _matrix;

    public int CountRows { get; protected set; }
    public int CountColumn { get; protected set; }

    public T this[int row, int column]
    {
        get
        {
            if (row >= CountRows || column >= CountColumn)
                throw new IndexOutOfRangeException();

            return _matrix[row, column];
        }
        set
        {
            if (row >= CountRows || column >= CountColumn)
                throw new IndexOutOfRangeException();

            _matrix[row, column] = value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return (IEnumerator<T>)_matrix.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}