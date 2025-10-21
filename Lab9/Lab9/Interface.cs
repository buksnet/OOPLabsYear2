using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    public static class Interface
    {
        public static void Run()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                DisplayMainMenu();

                Console.Write("\nВыберите действие: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DemonstrateDiapason();
                        break;
                    case "2":
                        DemonstrateDiapasonArray();
                        break;
                    case "3":
                        DemonstrateMaxDiapason();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Выход из программы...");
                        continue;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        public static void DisplayMainMenu()
        {
            Console.WriteLine("=== Демонстрация работы с классами Diapason и DiapasonArray ===");
            Console.WriteLine("1. Работа с одиночным диапазоном (Diapason)");
            Console.WriteLine("2. Работа с массивом диапазонов (DiapasonArray)");
            Console.WriteLine("3. Найти максимальный диапазон в коллекции");
            Console.WriteLine("0. Выход");
        }

        public static void DemonstrateDiapason()
        {
            Console.WriteLine("\n--- Демонстрация работы с Diapason ---");

            // Создание диапазона с ручным вводом
            Diapason? diapason = CreateDiapasonManually();
            if (diapason == null) return;

            Console.WriteLine($"Создан диапазон: {diapason}");

            // Проверка вхождения числа
            Console.Write("Введите число для проверки вхождения в диапазон: ");
            if (double.TryParse(Console.ReadLine(), out double testValue))
            {
                bool isInRange = diapason.IsInRange(testValue);
                Console.WriteLine($"Число {testValue} {(isInRange ? "входит" : "не входит")} в диапазон");
            }

            // Демонстрация операторов
            DemonstrateDiapasonOperators(diapason);
        }

        public static Diapason? CreateDiapasonManually()
        {
            Console.WriteLine("\nСоздание диапазона:");
            Console.Write("Введите начало диапазона: ");
            if (!double.TryParse(Console.ReadLine(), out double start))
            {
                Console.WriteLine("Некорректный ввод начала диапазона!");
                return null;
            }

            Console.Write("Введите конец диапазона: ");
            if (!double.TryParse(Console.ReadLine(), out double end))
            {
                Console.WriteLine("Некорректный ввод конца диапазона!");
                return null;
            }

            return new Diapason(start, end);
        }

        public static void DemonstrateDiapasonOperators(Diapason diapason)
        {
            Console.WriteLine($"\n--- Демонстрация операторов ---");
            Console.WriteLine($"Исходный: {diapason}");

            // Создаем копии для демонстрации операторов без изменения оригинала
            Diapason temp1 = new(diapason.Start, diapason.End);
            temp1++;
            Console.WriteLine($"После ++: {temp1}");

            Diapason temp2 = new(diapason.Start, diapason.End);
            temp2--;
            Console.WriteLine($"После --: {temp2}");

            double length = !diapason;
            Console.WriteLine($"Длина диапазона: {length:F2}");

            int startInt = (int)diapason;
            Console.WriteLine($"Начало как int: {startInt}");

            double endDouble = diapason;
            Console.WriteLine($"Конец как double: {endDouble:F2}");

            Diapason shifted = diapason + 5;
            Console.WriteLine($"После +5: {shifted}");

            double testValue = diapason.Start + (!diapason) / 2;
            Console.WriteLine($"Проверка {testValue:F2} в диапазоне: {diapason < testValue}");
        }

        public static void DemonstrateDiapasonArray()
        {
            Console.WriteLine("\n--- Демонстрация работы с DiapasonArray ---");

            Console.WriteLine("Выберите способ создания массива:");
            Console.WriteLine("1 - Создать массив со случайными значениями");
            Console.WriteLine("2 - Создать массив с ручным вводом значений");

            string? choice = Console.ReadLine();
            DiapasonArray array;

            switch (choice)
            {
                case "1":
                    Console.Write("Введите количество диапазонов: ");
                    if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
                    {
                        array = new DiapasonArray(count);
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод!");
                        return;
                    }
                    break;
                case "2":
                    array = CreateManualArray() ?? new DiapasonArray();
                    if (array == null) return;
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    return;
            }

            Console.WriteLine($"Создан массив: {array}");

            // Демонстрация индексатора
            DemonstrateIndexer(array);
        }

        public static DiapasonArray? CreateManualArray()
        {
            Console.Write("Введите количество элементов: ");
            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
            {
                Console.WriteLine("Некорректный ввод!");
                return null;
            }

            Diapason[] ranges = new Diapason[count];

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nЭлемент {i + 1}:");
                Diapason? range = CreateDiapasonManually();
                if (range == null)
                {
                    Console.WriteLine("Ошибка при создании диапазона!");
                    return null;
                }
                ranges[i] = range;
            }

            return new DiapasonArray(ranges);
        }

        public static void DemonstrateIndexer(DiapasonArray array)
        {
            Console.WriteLine($"\n--- Демонстрация работы индексатора ---");

            if (array.Length > 0)
            {
                Console.WriteLine($"Первый элемент: {array[0]}");
                Console.WriteLine($"Последний элемент: {array[^1]}");
            }

            // Демонстрация обработки ошибок
            try
            {
                Console.WriteLine($"Попытка доступа к несуществующему индексу {array.Length}...");
                var temp = array[array.Length];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public static void DemonstrateMaxDiapason()
        {
            Console.WriteLine("\n--- Поиск максимального диапазона ---");

            Console.WriteLine("Выберите способ создания коллекции:");
            Console.WriteLine("1 - Использовать тестовую коллекцию");
            Console.WriteLine("2 - Создать коллекцию вручную");

            string? choice = Console.ReadLine();
            DiapasonArray array;

            switch (choice)
            {
                case "1":
                    // Создаем тестовую коллекцию
                    Diapason[] testRanges =
                    [
                        new Diapason(1, 5),    // длина 4
                        new Diapason(10, 15),  // длина 5
                        new Diapason(2, 8),    // длина 6
                        new Diapason(20, 25)   // длина 5
                    ];
                    array = new DiapasonArray(testRanges);
                    break;
                case "2":
                    array = CreateManualArray() ?? new DiapasonArray();
                    if (array == null) return;
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    return;
            }

            Console.WriteLine($"Коллекция: {array}");

            Diapason maxDiapason = FindMaxDiapason(array);
            Console.WriteLine($"Диапазон с максимальной длиной: {maxDiapason}");
            Console.WriteLine($"Длина: {!maxDiapason:F2}");
        }

        /// <summary>
        /// Находит диапазон с максимальной длиной в коллекции
        /// </summary>
        public static Diapason FindMaxDiapason(DiapasonArray array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Коллекция не может быть пустой", nameof(array));

            Diapason maxDiapason = array[0];
            double maxLength = !maxDiapason;

            for (int i = 1; i < array.Length; i++)
            {
                double currentLength = !array[i];
                if (currentLength > maxLength)
                {
                    maxLength = currentLength;
                    maxDiapason = array[i];
                }
            }

            return maxDiapason;
        }
    }
}