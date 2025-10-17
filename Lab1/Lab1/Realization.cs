namespace Lab1
{
    public class Realization
    {
        #region Tasks
        public static void SolveTaskOne(int m, int n, int x)
        {
            Console.WriteLine($"m - ++n: {m - ++n}");
            Console.WriteLine($"m++ > --n: {m++ > --n}");
            Console.WriteLine($"m-- < ++n: {m-- < ++n}");

            Console.WriteLine($"Значение выражения 25x^-√(x^2+x): {25 * Math.Pow(x, 5) - Math.Sqrt(Math.Pow(x, 2) + x)}");
        }

        public static void SolveTaskTwo(double x, double y)
        {
            if (y <= 0 && (Math.Pow(x, 2) + Math.Pow(y, 2)) <= 1) Console.WriteLine("Точка принадлежит площади графика");
            else Console.WriteLine("Точка не относится к площади графика");
        }

        public static void SolveTaskThree(float a, float b)
        {
            Console.WriteLine(Math.Pow(a - b, 3) - (Math.Pow(a, 3) + 3 * a * Math.Pow(b, 2)) / (-3 * Math.Pow(a, 2) * b - Math.Pow(b, 3)));
        }

        public static void SolveTaskThree(double a, double b)
        {
            Console.WriteLine(Math.Pow(a - b, 3) - (Math.Pow(a, 3) + 3 * a * Math.Pow(b, 2)) / (-3 * Math.Pow(a, 2) * b - Math.Pow(b, 3)));
        }
        #endregion

        #region Utils
        private static void _TaskWrapper(int? taskNumber)
        {
            const byte first = 1;
            const byte second = 2;
            const byte third = 3;

            switch (taskNumber)
            {
                case first:
                    {
                        Console.Write("n?");
                        var n = Convert.ToInt32(Console.ReadLine());
                        Console.Write("m?");
                        var m = Convert.ToInt32(Console.ReadLine());
                        Console.Write("x?");
                        var x = Convert.ToInt32(Console.ReadLine());
                        Realization.SolveTaskOne(n, m, x);
                        break;
                    }
                case second:
                    {
                        Console.Write("x?");
                        var x = Convert.ToDouble(Console.ReadLine());
                        Console.Write("y?");
                        var y = Convert.ToDouble(Console.ReadLine());
                        Realization.SolveTaskTwo(x, y);
                        break;
                    }
                case third:
                    {
                        Console.Write("Выберите тип переменной:\n1 - float, 2 - double: ");
                        var dataType = Convert.ToByte(Console.ReadLine());

                        bool isFloat = (dataType == 1) ? true : false; // тернарный оператор для избавления от 
                        if (isFloat)
                        {
                            float a = 1000f, b = .0001f;
                            Realization.SolveTaskThree(a, b);
                        }
                        else
                        {
                            double a = 1000, b = .0001;
                            Realization.SolveTaskThree(a, b);
                        }

                        break;
                    }
                default:
                    {
                        Console.WriteLine("Некорректный ввод.");
                        break;
                    }
            }
            Console.WriteLine();
        }

        public void RunInterface()
        {
            while (true)
            {
                Console.Write("Введите номер нужной вам задачи (1/2/3 - номера задач, 0 для выхода из программы): ");
                try
                {
                    var chosenTaskNumber = Convert.ToByte(Console.ReadLine());
                    if (chosenTaskNumber == 0) break;
                    _TaskWrapper(chosenTaskNumber);
                }

                catch (FormatException) { Console.WriteLine("Некорректный ввод!\nПодсказка: используйте запятую при введении дробных чисел.\n"); } //  Обработка ошибки некорректного ввода значений

                catch (OverflowException) { Console.WriteLine("Введённое значение слишком велико для текущего шага!"); } // Обработка ошибки переполнения переменных

                catch (Exception e) { Console.WriteLine($"!\nПроизошла ошибка выполнения! Текст ошибки: {e.ToString()}!"); } //  Обработка ошибок выполнения

            }
        }
        #endregion
    }
}