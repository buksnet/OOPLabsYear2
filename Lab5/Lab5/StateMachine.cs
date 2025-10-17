namespace Lab5
{
    public class Cycle
    {
        private bool isRunning = false;
        private char choice = '-';

        private static char ListenForAction()
        {
            try
            {
                return Console.ReadKey().KeyChar;
            }
            catch (Exception ex) when (ex is IOException || ex is OutOfMemoryException)
            {
                Console.WriteLine($"Ошибка ввода: {ex.Message}");
                return '-';
            }
        }

        private static void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Выберите нужную вам функцию (нажмите клавишу на клавиатуре):\n" +
                "[1] Задача на удаление элемента по номеру\n" +
                "[2] Задача на добавление строки в начало матрицы\n" +
                "[3] Задача на удаление K строк с элемента N\n" +
                "[0] Выход из программы\n");
        }

        private void Execute(char command)
        {
            switch (command)
            {
                case '0':
                    isRunning = false;
                    return;
                case '1':
                    ExecuteRemoveElement();
                    break;
                case '2':
                    ExecuteAddRowAtBeginning();
                    break;
                case '3':
                    ExecuteRemoveRows();
                    break;
                default:
                    Console.WriteLine("Неизвестная команда");
                    WaitForUser();
                    break;
            }
        }

        private static void ExecuteRemoveElement()
        {
            Console.Clear();
            Console.WriteLine("Выполнение задачи на удаление элемента:\n");
            var array = new DynamicArray();
            ArrayClassSelector(array);
            FillArraySelector(array);
            RemoveSpecifiedElementCase(array);
        }

        private static void ExecuteAddRowAtBeginning()
        {
            Console.Clear();
            Console.WriteLine("Выполнение задачи на добавление строки в начало:\n");
            var array = new DynamicArray();
            ArrayClassSelector(array);
            FillArraySelector(array);
            AddRowAtBeginningCase(array);
        }

        private static void ExecuteRemoveRows()
        {
            Console.Clear();
            Console.WriteLine("Выполнение задачи на удаление K строк:\n");
            var array = new DynamicArray();
            ArrayClassSelector(array);
            FillArraySelector(array);
            RemoveRowsCase(array);
        }

        public void Run()
        {
            isRunning = true;
            while (isRunning)
            {
                PrintMainMenu();
                choice = ListenForAction();
                Execute(choice);
            }
        }

        // Utility methods
        private static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) && result > 0)
                    return result;
                Console.WriteLine("Некорректный ввод. Введите положительное число.");
            }
        }

        private static int ReadIntWithMin(string prompt, int minValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) && result >= minValue)
                    return result;
                Console.WriteLine($"Некорректный ввод. Введите число не меньше {minValue}.");
            }
        }

        private static int ReadAnyInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;
                Console.WriteLine("Некорректный ввод. Введите целое число.");
            }
        }

        private static void WaitForUser()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        // Array creation methods
        static void ArrayClassSelector(DynamicArray array)
        {
            Console.WriteLine("Укажите вид требуемого массива (1 для одномерного, 2 для двумерного или 3 для рваного).");

            while (true)
            {
                if (short.TryParse(Console.ReadLine(), out short choice))
                {
                    try
                    {
                        ExecuteArrayCreation(array, choice);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка создания массива: {ex.Message}");
                        Console.WriteLine("Попробуйте ещё раз:");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Введите 1, 2 или 3:");
                }
            }
        }

        // Array filling method selection
        static void FillArraySelector(DynamicArray array)
        {
            Console.WriteLine("\nВыберите способ заполнения массива:");
            Console.WriteLine("[1] Автоматическое заполнение случайными числами");
            Console.WriteLine("[2] Ручное заполнение");

            while (true)
            {
                if (short.TryParse(Console.ReadLine(), out short choice))
                {
                    switch (choice)
                    {
                        case 1:
                            array.RandomFill();
                            return;
                        case 2:
                            array.ManualFill();
                            return;
                        default:
                            Console.WriteLine("Некорректный ввод. Введите 1 или 2:");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Введите 1 или 2:");
                }
            }
        }

        private static void ExecuteArrayCreation(DynamicArray array, short choice)
        {
            switch (choice)
            {
                case 1:
                    CreateSingleDimensionalArray(array);
                    break;
                case 2:
                    CreateTwoDimensionalArray(array);
                    break;
                case 3:
                    CreateJaggedArray(array);
                    break;
                default:
                    throw new ArgumentException("Неизвестный тип массива");
            }
        }

        private static void CreateSingleDimensionalArray(DynamicArray array)
        {
            int j = ReadInt("Введите число элементов массива: ");
            array.MakeArray(1, j);
        }

        private static void CreateTwoDimensionalArray(DynamicArray array)
        {
            int i = ReadInt("Введите число строк: ");
            int j = ReadInt("Введите число столбцов: ");
            array.MakeArray(i, j);
        }

        private static void CreateJaggedArray(DynamicArray array)
        {
            int i = ReadInt("Введите число строк: ");
            List<int> quantities = [];

            for (int k = 0; k < i; k++)
            {
                int j = ReadInt($"Введите число элементов для строки {k + 1}: ");
                quantities.Add(j);
            }

            array.MakeArray(i, quantities);
        }

        // Task execution methods
        private static void RemoveSpecifiedElementCase(DynamicArray array)
        {
            Console.Clear();
            Console.WriteLine("Сгенерированный массив:");
            array.PrintArray();
            Console.WriteLine();

            if (array.IsSingleDismension())
            {
                int j = ReadIntWithMin("Введите номер элемента для удаления: ", 1);
                array.RemoveThisElement(j);
            }
            else
            {
                int row = ReadIntWithMin("Введите номер строки: ", 1);
                int col = ReadIntWithMin("Введите номер столбца: ", 1);
                array.RemoveThisElement(col, row - 1);
            }

            Console.WriteLine("\nРезультат после удаления:");
            array.PrintArray();
            WaitForUser();
        }

        private static void AddRowAtBeginningCase(DynamicArray array)
        {
            Console.Clear();
            Console.WriteLine("Исходный массив:");
            array.PrintArray();
            Console.WriteLine();

            Console.WriteLine("Введите элементы новой строки:");
            int columns = array.GetColumnCount(0); // Assume same columns for all rows
            int[] newRow = new int[columns];

            for (int i = 0; i < columns; i++)
            {
                newRow[i] = ReadAnyInt($"Элемент {i + 1}: ");
            }

            array.AddRowAtBeginning(newRow);

            Console.WriteLine("\nРезультат после добавления строки:");
            array.PrintArray();
            WaitForUser();
        }

        private static void RemoveRowsCase(DynamicArray array)
        {
            Console.Clear();
            Console.WriteLine("Исходный массив:");
            array.PrintArray();
            Console.WriteLine();

            int startRow = ReadIntWithMin("Введите номер строки, с которой начать удаление: ", 1);
            int count = ReadIntWithMin("Введите количество строк для удаления: ", 1);

            array.RemoveRowsStartingFrom(startRow, count);

            Console.WriteLine("\nРезультат после удаления строк:");
            array.PrintArray();
            WaitForUser();
        }
    }
}