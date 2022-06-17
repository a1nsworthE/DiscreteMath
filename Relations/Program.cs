using System;
using System.Collections;
using System.Collections.Generic;

namespace DiscreteMath
{
    /// <summary>
    /// Класс Отношения для Натуральных чисел.
    /// Реализованный в виде, квадратной матрицы.
    /// Заполненной true либо false.
    /// </summary>
    public partial class Relations
    {
        /// <summary>
        /// Матрица.
        /// </summary>
        private bool[,] _relations;

        /// <summary>
        /// Мощность матрицы, определяется по количеству входящих отношений.
        /// </summary>
        private int _power = 0;

        private bool Empty() => _power == 0;

        public Relations()
        {
            _power = 0;
            _relations = new bool[0, 0];
        }

        /// <summary>
        /// Выделение памяти под квадратную матрицу.
        /// </summary>
        /// <param name="size">Размерность квадратной матрицы.</param>
        public Relations(int size)
        {
            _relations = new bool[size, size];
        }

        /// <summary>
        /// Инициализирует отношение. 
        /// </summary>
        /// <param name="collection">Список для инициализации.</param>
        public Relations(IEnumerable<(int, int)> collection)
        {
            int max = 0;
            foreach (var item in collection)
            {
                max = Math.Max(max, Math.Max(item.Item1, item.Item2));
            }

            _relations = new bool[max, max];
            foreach (var item in collection)
            {
                this[item.Item1 - 1, item.Item2 - 1] = true;
            }
        }

        /// <summary>
        /// Инициализирует отношение. 
        /// </summary>
        /// <param name="collection">Список для инициализации.</param>
        public Relations(IEnumerable<Tuple<int, int>> collection)
        {
            int max = 0;
            foreach (var item in collection)
            {
                max = Math.Max(max, Math.Max(item.Item1, item.Item2));
            }

            _relations = new bool[max, max];
            foreach (var item in collection)
            {
                this[item.Item1 - 1, item.Item2 - 1] = true;
            }
        }

        /// <summary>
        /// Выделение памяти и инициализирование матрицы из другой матрицы.
        /// </summary>
        /// <param name="array">Матрица, для инициализации.</param>
        public Relations(bool[,] array)
        {
            _relations = array;
            foreach (var element in _relations)
            {
                if (element)
                {
                    _power++;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="maxElement"></param>
        public Relations(in IEnumerable<Tuple<int, int>> collection, int maxElement) : this(maxElement)
        {
            foreach (var pair in collection)
            {
                this[pair.Item1 - 1, pair.Item2 - 1] = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="maxElement"></param>
        public Relations(in IEnumerable<(int, int)> collection, int maxElement) : this(maxElement)
        {
            foreach (var pair in collection)
            {
                this[pair.Item1 - 1, pair.Item2 - 1] = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="maxElement"></param>
        public Relations(int maxElement, params (int, int)[] collection) : this(maxElement)
        {
            foreach (var pair in collection)
            {
                this[pair.Item1 - 1, pair.Item2 - 1] = true;
            }
        }

        /// <summary>
        /// Инициализирует отношение. 
        /// </summary>
        /// <param name="collection">Список для инициализации.</param>
        public Relations(params (int, int)[] collection)
        {
            int maxElement = 0;
            foreach (var pair in collection)
            {
                maxElement = Math.Max(maxElement, Math.Max(pair.Item1, pair.Item2));
            }

            _relations = new bool[maxElement, maxElement];
            foreach (var pair in collection)
            {
                this[pair.Item1 - 1, pair.Item2 - 1] = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        public Relations(in Relations other)
        {
            this._power = other._power;
            this._relations = other._relations;
        }

        /// <summary>
        /// Всего ячеек в матрице.
        /// </summary>
        /// <value>Возвращает количество ячеек в матрице.</value>
        public int CounterCells => _relations.Length;

        /// <summary>
        /// Всего количество строк и столбцов.
        /// </summary>
        /// <value>Возвращает количество строк и столбцов.</value>
        public int Length => (int)Math.Sqrt(_relations.Length);

        /// <summary>
        /// Мощность матрицы.
        /// </summary>
        /// <value>Возвращает кол-во лежащих элементов в матрице.</value>
        public int Power => _power;

        public bool[,] Data => _relations;

        /// <summary>
        /// Удаления отношения (a,b) из матрицы
        /// </summary>
        /// <param name="p">Пара (a,b)</param>
        public void DeleteElementByValues(in Tuple<int, int> p)
        {
            try
            {
                if ((p.Item1 > Length || p.Item2 > Length) || p.Item2 * p.Item1 == 0)
                {
                    throw new ArgumentOutOfRangeException("Bad Elements");
                }

                _relations[p.Item1 - 1, p.Item2 - 1] = false;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                System.Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Удаления отношения (a,b) из матрицы
        /// </summary>
        /// <param name="p">Пара (a,b)</param>
        public void DeleteElementByValues(in (int, int) p)
        {
            try
            {
                if ((p.Item1 > Length || p.Item2 > Length) || p.Item2 * p.Item1 == 0)
                {
                    throw new ArgumentOutOfRangeException("Bad Elements");
                }

                _relations[p.Item1 - 1, p.Item2 - 1] = false;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                System.Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Вывод отношений.
        /// </summary>
        /// <param name="r">Отношение.</param>
        public static void Output(in Relations r)
        {
            Console.Write('{');
            for (int row = 0; row < r.Length; row++)
            {
                for (int column = 0; column < r.Length; column++)
                {
                    if (r._relations[row, column])
                    {
                        Console.Write($"({row + 1},{column + 1})");
                    }
                }
            }
            Console.WriteLine('}');
        }

        /// <summary>
        /// Ввод отношений.
        /// </summary>
        /// <param name="totalPairs">Кол-во элементов в отношениях.</param>
        /// <returns>Возвращает отношения.</returns>
        public static Relations Input(int totalPairs)
        {
            var list = new List<Tuple<int, int>>();
            string[] tuple = Console.ReadLine().Split(' ');
            int maxElement = 0;

            // Пары через enter.
            for (int i = 0; i < totalPairs; i++)
            {
                list.Add(Tuple.Create<int, int>(Convert.ToInt32(tuple[0]), Convert.ToInt32(tuple[1])));
                maxElement = Math.Max(maxElement, Math.Max(list[i - 1].Item1, list[i - 1].Item2));

                tuple = Console.ReadLine().Split(' ');
            }

            return new Relations(list, maxElement);
        }
    }

    /// <summary>
    /// Содержит операции и перегрузки над отношениями.
    /// </summary>
    public partial class Relations
    {
        /// <summary>
        /// Перегрузка [,]
        /// </summary>
        /// <value>Возвращает true, если элемент под индексом indexRow, indexColumn, есть в матрице.</value>
        public bool this[int indexRow, int indexColumn]
        {
            get
            {
                if (indexColumn >= Length || indexRow >= Length)
                {
                    throw new ArgumentOutOfRangeException("Bad indexs");
                }

                return _relations[indexRow, indexColumn];
            }

            set
            {
                if (indexColumn >= Length || indexRow >= Length)
                {
                    throw new ArgumentOutOfRangeException("Bad indexs");
                }

                if (_relations[indexRow, indexColumn] && !value)
                {
                    _relations[indexRow, indexColumn] = value;
                    _power--;
                }
                else if (!_relations[indexRow, indexColumn] && value)
                {
                    _relations[indexRow, indexColumn] = value;
                    _power++;
                }
            }
        }

        /// <summary>
        /// Является ли матрица отношений r1 подможеством матрицы отношений r2.
        /// </summary>
        /// <param name="r1">Отношения первое.</param>
        /// <param name="r2">Отношение второе.</param>
        /// <returns>Возвращает true, если r1 подмножество множества r2</returns>
        public static bool operator >=(in Relations r1, in Relations r2)
        {
            if (r1.Length > r2.Length)
            {
                return false;
            }

            for (int row = 0; row < r1.Length; row++)
            {
                for (int column = 0; column < r1.Length; column++)
                {
                    if (r1[row, column] && r1[row, column] != r2[row, column])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Является ли матрица отношений r2 подможеством матрицы отношений r1.
        /// </summary>
        /// <param name="r1">Отношения первое.</param>
        /// <param name="r2">Отношение второе.</param>
        /// <returns>Возвращает true, если r2 подмножество множества r1, иначе false</returns>
        public static bool operator <=(in Relations r1, in Relations r2) => r2 >= r1;

        /// <summary>
        /// Равенство отношений.
        /// </summary>
        /// <param name="r1">Отношение первое.</param>
        /// <param name="r2">Отношение второе.</param>
        /// <returns>Возвращает true если r1 равняется r2, иначе false.</returns>
        public static bool operator ==(in Relations r1, in Relations r2)
        {
            return (r1.Empty() && r2.Empty()) || (r1.Length == r2.Length && r1 >= r2);
        }

        /// <summary>
        /// Неравенство отношений.
        /// </summary>
        /// <param name="r1">Отношение первое.</param>
        /// <param name="r2">Отношение второе.</param>
        /// <returns>Возвращает true если r1 неравняется r2, иначе false.</returns>
        public static bool operator !=(in Relations r1, in Relations r2) => !(r1 == r2);

        /// <summary>
        /// Включена ли строго матрица отношений r1 в матрицу отношений r2.
        /// </summary>
        /// <param name="r1">Отношения первое.</param>
        /// <param name="r2">Отношение второе.</param>
        /// <returns>Возвращает true, если r1 строго включено в r2, иначе false.</returns>
        public static bool operator >(in Relations r1, in Relations r2) => r1 != r2 && r1 >= r2;

        /// <summary>
        /// Включена ли строго матрица отношений r2 в матрицу отношений r1.
        /// </summary>
        /// <param name="r1">Отношения первое.</param>
        /// <param name="r2">Отношение второе.</param>
        /// <returns>Возвращает true, если r2 строго включено в r1, иначе false.</returns>
        public static bool operator <(in Relations r1, in Relations r2) => r2 > r1;

        /// <summary>
        /// Объединение двух Отношений.
        /// </summary>
        /// <param name="r1">Отношения первое.</param>
        /// <param name="r2">Отношение второе.</param>
        /// <returns>Возвращает результат объединения r1 и r2.</returns>
        public static Relations operator +(in Relations r1, in Relations r2)
        {
            Relations result = new Relations(Math.Max(r1.Length, r2.Length));

            for (int row = 0; row < result.Length; row++)
            {
                for (int column = 0; column < result.Length; column++)
                {
                    if (((row <= r1.Length - 1) && (column <= r1.Length - 1)) &&
                    (((row <= r2.Length - 1) && (column <= r2.Length - 1))))
                    {
                        result[row, column] = r1[row, column] || r2[row, column];
                    }
                    else if ((row <= r1.Length - 1) && (column <= r1.Length - 1))
                    {
                        result[row, column] = r1[row, column];
                    }
                    else if ((row <= r2.Length - 1) && (column <= r2.Length - 1))
                    {
                        result[row, column] = r2[row, column];
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// Пересечение двух Отношений.
        /// </summary>
        /// <param name="r1">Отношения первое.</param>
        /// <param name="r2">Отношение второе.</param>
        /// <returns>Возвращает результат Пересечения r1 и r2.</returns>
        public static Relations operator *(in Relations r1, in Relations r2)
        {
            if (r1.Empty() && r2.Empty())
            {
                return new Relations(Math.Min(r1.Length, r2.Length));
            }
            else if (r1.Empty())
            {
                return new Relations(r1.Length);
            }
            else if (r2.Empty())
            {
                return new Relations(r2.Length);
            }

            var result = new Relations(Math.Min(r1.Length, r2.Length));
            for (int row = 0; row < result.Length; row++)
            {
                for (int column = 0; column < result.Length; column++)
                {
                    if (((row <= r1.Length - 1) && (column <= r1.Length - 1)) &&
                         (((row <= r2.Length - 1) && (column <= r2.Length - 1))))
                    {
                        result[row, column] = r1[row, column] && r2[row, column];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Разность двух Отношений.
        /// </summary>
        /// <param name="r1">Отношения первое.</param>
        /// <param name="r2">Отношение второе.</param>
        /// <returns>Возвращает результат Разности r1 и r2.</returns>
        public static Relations operator -(in Relations r1, in Relations r2)
        {
            Relations result = new Relations(r1.Length);

            for (int row = 0; row < result.Length; row++)
            {
                for (int column = 0; column < result.Length; column++)
                {
                    if (((row <= r1.Length - 1) && (column <= r1.Length - 1)) &&
                        (((row <= r2.Length - 1) && (column <= r2.Length - 1))))
                    {
                        if (r1[row, column] != r2[row, column])
                        {
                            result[row, column] = r1[row, column];
                        }
                    }
                    else if (((row <= r1.Length - 1) && (column <= r1.Length - 1)))
                    {
                        result[row, column] = r1[row, column];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Симметрическая разность двух Отношений.
        /// </summary>
        /// <param name="r1">Отношения первое.</param>
        /// <param name="r2">Отношение второе.</param>
        /// <returns>Возвращает результат Симметрической разности r1 и r2.</returns>
        public static Relations operator ^(in Relations r1, in Relations r2)
        {
            Relations result = new Relations(Math.Max(r1.Length, r2.Length));

            for (int row = 0; row < result.Length; row++)
            {
                for (int column = 0; column < result.Length; column++)
                {
                    if (((row <= r1.Length - 1) && (column <= r1.Length - 1)) &&
                        (((row <= r2.Length - 1) && (column <= r2.Length - 1))))
                    {
                        result[row, column] = r1[row, column] ^ r2[row, column];
                    }
                    else if (((row <= r1.Length - 1) && (column <= r1.Length - 1)))
                    {
                        result[row, column] = r1[row, column];
                    }
                    else if ((row <= r2.Length - 1) && (column <= r2.Length - 1))
                    {
                        result[row, column] = r2[row, column];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Обращение отношения.
        /// </summary>
        /// <param name="r">Отношение.</param>
        /// <returns>Возвращает обращенное отношение.</returns>
        public static Relations Conversion(in Relations r)
        {
            var list = new List<(int, int)>();
            int maxElement = 0;
            for (int i = 1; i <= r.Length; i++)
            {
                if (r[i - 1, i - 1])
                {
                    list.Add((i, i));
                    maxElement = i;
                }
            }

            return new Relations(list, maxElement); ;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        // TODO исправить
        public static Relations Composition(in Relations r1, in Relations r2)
        {
            if (r1.Empty() && r2.Empty())
            {
                return new Relations(Math.Min(r1.Length, r2.Length));
            }
            else if (r1.Empty())
            {
                return new Relations(r1.Length);
            }
            else if (r2.Empty())
            {
                return new Relations(r2.Length);
            }

            var list = new List<(int, int)>();
            var minLength = Math.Min(r1.Length, r2.Length);
            int maxElement = 0;
            for (int row = 1; row <= minLength; row++)
            {
                for (int column = 1; column <= minLength; column++)
                {
                    for (int z = 0; z < minLength; z++)
                    {
                        if (r1[row - 1, z] && r2[z, column - 1])
                        {
                            maxElement = Math.Max(maxElement, Math.Max(row, column));
                            list.Add((row, column));
                        }
                    }
                }
            }

            return new Relations(list, maxElement);
        }

        /// <summary>
        /// Возводит отношение в степень.
        /// </summary>
        /// <param name="r">Отношение.</param>
        /// <param name="degree">Степень в которую требуется возвести.</param>
        /// <returns>Возвращает отношение r возведенное в степень degree.</returns>
        public static dynamic Degree(in Relations r, int degree)
        {
            if (degree == 0)
            {
                return 1;
            }
            else
            {
                var result = r;
                for (int i = 1; i < degree; i++)
                {
                    result = Relations.Composition(r, result);
                }

                return result;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj != null && obj is Relations)
            {
                Relations other = obj as Relations;
                return other == this;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _relations.GetHashCode();
        }
    }


    class Program
    {

        static void Main(string[] args)
        {
            Relations a = new Relations((1, 3), (2, 3), (1, 4));
            Relations b = new Relations((3, 1), (4, 1));

            Relations.Output(Relations.Composition(a, b));
        }
    }

}