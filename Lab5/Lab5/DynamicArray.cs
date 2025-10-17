namespace Lab5
{
    public class DynamicArray
    {
        private int[][]? _data;

        public bool IsSingleDismension()
        {
            if (_data == null) return true;
            return _data.Length <= 1;
        }

        public int RowCount => _data?.Length ?? 0;

        public int GetColumnCount(int row) =>
            (_data != null && row >= 0 && row < _data.Length) ? _data[row].Length : 0;

        /// <summary>
        /// Creates array with specified rows and columns count.
        /// </summary>
        public void MakeArray(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
                throw new ArgumentException("Rows and columns must be positive numbers");

            int[][] outputArray = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                outputArray[i] = new int[columns];
            }

            _data = outputArray;
        }

        /// <summary>
        /// Creates jagged array with specified element counts per row.
        /// </summary>
        public void MakeArray(int rows, List<int> elem_counts)
        {
            if (rows <= 0)
                throw new ArgumentException("Rows must be positive number");
            if (elem_counts.Count != rows)
                throw new ArgumentException("Length of `elem_counts` list must match rows count");

            int[][] outputArray = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                if (elem_counts[i] <= 0)
                    throw new ArgumentException($"Element count for row {i} must be positive");

                outputArray[i] = new int[elem_counts[i]];
            }

            _data = outputArray;
        }

        /// <summary>
        /// Fill array with random integers.
        /// </summary>
        public void RandomFill()
        {
            if (_data == null) return;

            var rng = new Random();
            for (int i = 0; i < _data.Length; i++)
            {
                for (int j = 0; j < _data[i].Length; j++)
                {
                    _data[i][j] = rng.Next(1, 1024); // Минимальное значение 1 для лучшей читаемости
                }
            }
        }

        /// <summary>
        /// Fill array manually with user input.
        /// </summary>
        public void ManualFill()
        {
            if (_data == null) return;

            for (int i = 0; i < _data.Length; i++)
            {
                Console.WriteLine($"Заполнение строки {i + 1}:");
                for (int j = 0; j < _data[i].Length; j++)
                {
                    while (true)
                    {
                        Console.Write($"Элемент [{i + 1},{j + 1}]: ");
                        if (int.TryParse(Console.ReadLine(), out int value))
                        {
                            _data[i][j] = value;
                            break;
                        }
                        Console.WriteLine("Некорректный ввод. Введите целое число.");
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints array to console for visualization.
        /// </summary>
        public void PrintArray()
        {
            if (_data == null)
            {
                Console.WriteLine("Массив не инициализирован");
                return;
            }

            for (int i = 0; i < _data.Length; i++)
            {
                if (_data[i] == null)
                {
                    Console.WriteLine("null");
                    continue;
                }

                Console.Write($"Строка {i + 1}: [");
                for (int j = 0; j < _data[i].Length; j++)
                {
                    Console.Write(_data[i][j]);
                    if (j < _data[i].Length - 1) Console.Write(", ");
                }
                Console.WriteLine("]");
            }
        }

        /// <summary>
        /// Deletes element at specified position (1-based indexing).
        /// </summary>
        public void RemoveThisElement(int col, int row = 0)
        {
            if (_data == null || row < 0 || row >= _data.Length)
                return;

            if (_data[row] == null || col < 1 || col > _data[row].Length)
                return;

            int actualIndex = col - 1;

            if (_data[row].Length == 1)
            {
                RemoveRow(row);
            }
            else
            {
                RemoveElementFromRow(row, actualIndex);
            }
        }

        /// <summary>
        /// Adds new row at the beginning of the matrix.
        /// </summary>
        public void AddRowAtBeginning(int[] newRow)
        {
            if (_data == null)
            {
                _data = [newRow];
                return;
            }

            ArgumentNullException.ThrowIfNull(newRow);

            int[][] newData = new int[_data.Length + 1][];
            newData[0] = newRow;

            for (int i = 0; i < _data.Length; i++)
            {
                newData[i + 1] = _data[i];
            }

            _data = newData;
        }

        /// <summary>
        /// Removes K rows starting from row N (1-based indexing).
        /// </summary>
        public void RemoveRowsStartingFrom(int startRow, int count)
        {
            if (_data == null) return;

            int actualStart = startRow - 1; // Convert to 0-based

            if (actualStart < 0 || actualStart >= _data.Length || count <= 0)
                return;

            int rowsToRemove = Math.Min(count, _data.Length - actualStart);
            int newSize = _data.Length - rowsToRemove;

            if (newSize == 0)
            {
                _data = null;
                return;
            }

            int[][] newData = new int[newSize][];

            // Copy rows before the removal section
            for (int i = 0; i < actualStart; i++)
            {
                newData[i] = _data[i];
            }

            // Copy rows after the removal section
            for (int i = actualStart + rowsToRemove, newIndex = actualStart;
                 i < _data.Length;
                 i++, newIndex++)
            {
                newData[newIndex] = _data[i];
            }

            _data = newData;
        }

        // Private helper methods
        private void RemoveRow(int rowIndex)
        {
            if (_data is null) return;
            int[][] newData = new int[_data.Length - 1][];
            for (int i = 0, newIndex = 0; i < _data.Length; i++)
            {
                if (i != rowIndex)
                {
                    newData[newIndex++] = _data[i];
                }
            }
            _data = newData.Length > 0 ? newData : null;
        }

        private void RemoveElementFromRow(int rowIndex, int elementIndex)
        {
            if (_data is null) return;

            int[] newRow = new int[_data[rowIndex].Length - 1];
            for (int j = 0, newIndex = 0; j < _data[rowIndex].Length; j++)
            {
                if (j != elementIndex)
                {
                    newRow[newIndex++] = _data[rowIndex][j];
                }
            }
            _data[rowIndex] = newRow;
        }
    }
}