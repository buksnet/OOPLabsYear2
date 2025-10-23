using Trials;

namespace Lab10
{
    public class Demo
    {
        public static void DemonstrateBinarySearch(Trial[] trials)
        {
            Console.WriteLine("\n=== ДЕМОНСТРАЦИЯ БИНАРНОГО ПОИСКА ===");
            Console.WriteLine($"Размер массива: {trials.Length} элементов");
            Console.WriteLine($"Теоретическое максимальное число шагов: {Math.Ceiling(Math.Log2(trials.Length))}");

            if (trials.Length == 0) return;

            // 1. Поиск элементов на разных позициях в отсортированном массиве
            Console.WriteLine("\n1. Поиск элементов на разных позициях:");

            // Первый элемент (должен найтись быстро - в корне или близко к корню)
            Console.WriteLine($"\n   а) Поиск первого элемента:");
            Console.Write("   ");
            trials[0].Show();
            int firstIndex = BinarySearchWithSteps(trials, trials[0], out int firstSteps);
            Console.WriteLine($"    Найден на позиции {firstIndex} за {firstSteps} шагов");

            // Последний элемент (тоже должен найтись быстро)
            Console.WriteLine($"\n   б) Поиск последнего элемента:");
            Console.Write("   ");
            trials[trials.Length - 1].Show();
            int lastIndex = BinarySearchWithSteps(trials, trials[trials.Length - 1], out int lastSteps);
            Console.WriteLine($"    Найден на позиции {lastIndex} за {lastSteps} шагов");

            // Средний элемент (скорее всего найдет за 1 шаг!)
            Console.WriteLine($"\n   в) Поиск среднего элемента:");
            int middlePos = trials.Length / 2;
            Console.Write("   ");
            trials[middlePos].Show();
            int middleIndex = BinarySearchWithSteps(trials, trials[middlePos], out int middleSteps);
            Console.WriteLine($"    Найден на позиции {middleIndex} за {middleSteps} шагов");

            // Элемент на позиции 1/4 (должен найтись за меньше шагов)
            Console.WriteLine($"\n   г) Поиск элемента на позиции 1/4:");
            int quarterPos = trials.Length / 4;
            Console.Write("   ");
            trials[quarterPos].Show();
            int quarterIndex = BinarySearchWithSteps(trials, trials[quarterPos], out int quarterSteps);
            Console.WriteLine($"    Найден на позиции {quarterIndex} за {quarterSteps} шагов");

            // Элемент на позиции 3/4 (тоже должен найтись быстро)
            Console.WriteLine($"\n   д) Поиск элемента на позиции 3/4:");
            int threeQuarterPos = 3 * trials.Length / 4;
            Console.Write("   ");
            trials[threeQuarterPos].Show();
            int threeQuarterIndex = BinarySearchWithSteps(trials, trials[threeQuarterPos], out int threeQuarterSteps);
            Console.WriteLine($"    Найден на позиции {threeQuarterIndex} за {threeQuarterSteps} шагов");

            // 2. Поиск случайных элементов из массива
            Console.WriteLine("\n2. Поиск случайных элементов из массива:");
            var rng = new Random();

            for (int i = 0; i < 3; i++)
            {
                int randomPos = rng.Next(trials.Length);
                Console.WriteLine($"\n   Случайный элемент #{i + 1}:");
                Console.Write("   ");
                trials[randomPos].Show();
                int randomIndex = BinarySearchWithSteps(trials, trials[randomPos], out int randomSteps);
                Console.WriteLine($"    Найден на позиции {randomIndex} за {randomSteps} шагов");
            }

            // 3. Поиск элементов разных типов с разной сложностью
            Console.WriteLine("\n3. Поиск элементов разных типов:");

            // Ищем простой Trial (скорее всего в конце массива из-за приоритетов)
            var simpleTrial = new Trial("Простое испытание", 60);
            Console.WriteLine($"\n   а) Поиск простого Trial:");
            Console.Write("   ");
            simpleTrial.Show();
            int trialIndex = BinarySearchWithSteps(trials, simpleTrial, out int trialSteps);
            if (trialIndex != -1)
            {
                Console.WriteLine($"    Найден на позиции {trialIndex} за {trialSteps} шагов");
            }
            else
            {
                Console.WriteLine($"    Не найден (за {trialSteps} шагов)");
            }

            // Ищем государственный экзамен (скорее всего в начале массива)
            var stateExam = new FinalExam("Госэкзамен", 180, 60, "Математика", true);
            Console.WriteLine($"\n   б) Поиск государственного экзамена:");
            Console.Write("   ");
            stateExam.Show();
            int stateIndex = BinarySearchWithSteps(trials, stateExam, out int stateSteps);
            if (stateIndex != -1)
            {
                Console.WriteLine($"    Найден на позиции {stateIndex} за {stateSteps} шагов");
            }
            else
            {
                Console.WriteLine($"    Не найден (за {stateSteps} шагов)");
            }

            // 4. Демонстрация "пограничных" случаев
            Console.WriteLine("\n4. Пограничные случаи:");

            // Поиск элемента, который может быть между двумя существующими
            if (trials.Length > 1)
            {
                // Создаем элемент со значением между двумя соседними
                int betweenPos = trials.Length / 2;
                var betweenTrial = new Trial(
                    trials[betweenPos].Name,
                    (trials[betweenPos].Duration + trials[betweenPos + 1].Duration) / 2
                );

                Console.WriteLine($"\n   а) Поиск несуществующего элемента между двумя существующими:");
                Console.Write("   ");
                betweenTrial.Show();
                int betweenIndex = BinarySearchWithSteps(trials, betweenTrial, out int betweenSteps);
                Console.WriteLine($"    Не найден (за {betweenSteps} шагов) - как и ожидалось");
            }

            // 5. Анализ эффективности
            Console.WriteLine("\n5. Анализ эффективности:");
            Console.WriteLine($"   - Минимальное число шагов: 1 (элемент в корне)");
            Console.WriteLine($"   - Максимальное число шагов: {Math.Ceiling(Math.Log2(trials.Length))} (листовые элементы)");
            Console.WriteLine($"   - Среднее число шагов: ~{Math.Ceiling(Math.Log2(trials.Length)) - 1}");
            Console.WriteLine($"   - Эффективность vs линейный поиск: в {trials.Length / Math.Ceiling(Math.Log2(trials.Length)):F0} раз лучше");

            // Визуализация дерева поиска для маленького массива
            if (trials.Length <= 10)
            {
                Console.WriteLine("\n6. Визуализация дерева поиска:");
                VisualizeSearchTree(trials);
            }
        }

        // Метод бинарного поиска с подсчетом шагов
        private static int BinarySearchWithSteps(Trial[] array, Trial target, out int steps)
        {
            steps = 0;
            if (array == null || array.Length == 0)
                return -1;

            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                steps++;
                int mid = left + (right - left) / 2;
                int comparison = array[mid].CompareTo(target);

                if (comparison == 0)
                    return mid;
                else if (comparison < 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return -1;
        }

        // Визуализация дерева поиска (для маленьких массивов)
        private static void VisualizeSearchTree(Trial[] array)
        {
            Console.WriteLine("   Индексы в порядке обхода бинарного поиска:");
            Console.Write("   ");

            var stack = new Stack<(int, int, int)>();
            stack.Push((0, array.Length - 1, 0));

            while (stack.Count > 0)
            {
                var (left, right, depth) = stack.Pop();
                if (left <= right)
                {
                    int mid = left + (right - left) / 2;
                    Console.Write($"{mid}({depth}) ");

                    if (right >= mid + 1)
                        stack.Push((mid + 1, right, depth + 1));
                    if (left <= mid - 1)
                        stack.Push((left, mid - 1, depth + 1));
                }
            }
            Console.WriteLine();
        }

        // Дополнительный метод для поиска с детальной отладкой (опционально)
        public static void DemonstrateSearchWithDebug(Trial[] trials, Trial target)
        {
            Console.WriteLine($"\n--- Детальный поиск ---");
            Console.Write("Цель: ");
            target.Show();

            int steps = 0;
            int left = 0;
            int right = trials.Length - 1;

            while (left <= right)
            {
                steps++;
                int mid = left + (right - left) / 2;
                Console.WriteLine($"Шаг {steps}: left={left}, right={right}, mid={mid}");
                Console.Write($"  Сравниваем с trials[{mid}]: ");
                trials[mid].Show();

                int comparison = trials[mid].CompareTo(target);

                if (comparison == 0)
                {
                    Console.WriteLine($"   Найден за {steps} шагов!");
                    return;
                }
                else if (comparison < 0)
                {
                    Console.WriteLine($"   Ищем справа (left = {mid + 1})");
                    left = mid + 1;
                }
                else
                {
                    Console.WriteLine($"   Ищем слева (right = {mid - 1})");
                    right = mid - 1;
                }
            }

            Console.WriteLine($" Не найден за {steps} шагов");
        }

        public static void DemonstrateUniversalArray()
        {
            // Массив типа IInit - может содержать ЛЮБЫЕ объекты, реализующие IInit
            IInit[] universalArray = new IInit[4];

            // Заполняем разными типами объектов
            universalArray[0] = new Trial();
            universalArray[1] = new Test();
            universalArray[2] = new Car("Toyota", 2020, 1500000); // новый класс
            universalArray[3] = new Exam();

            Console.WriteLine("=== Просмотр массива ===");
            foreach (IInit item in universalArray)
            {
                item.Show(); // У каждого своя реализация Show()
            }

            Console.WriteLine("\n=== Демонстрация RandomInit() ===");
            foreach (IInit item in universalArray)
            {
                item.RandomInit(); // У каждого своя случайная генерация
                item.Show();
            }

            // Если нужно показать Init() - можно для одного элемента
            Console.WriteLine("\n=== Демонстрация Init() ===");
            universalArray[0].Init(); // Ручной ввод для первого элемента
            universalArray[0].Show();
        }
        public static void DemonstrateCloning()
        {
            Console.WriteLine("\n=== Демонстрация клонирования ===");

            Car originalCar = new Car("BMW", 2022, 3000000);

            // Поверхностное копирование
            Car shallowCopy = originalCar.ShallowCopy();
            shallowCopy.Brand = "Audi"; // Изменяем копию

            // Глубокое копирование  
            Car deepCopy = (Car)originalCar.Clone();
            deepCopy.Year = 2023; // Изменяем копию

            Console.WriteLine("Оригинал:");
            originalCar.Show();
            Console.WriteLine("Поверхностная копия (изменен бренд):");
            shallowCopy.Show();
            Console.WriteLine("Глубокая копия (изменен год):");
            deepCopy.Show();

            // Демонстрация, что оригинал не изменился
            Console.WriteLine("Оригинал после клонирования (не изменился):");
            originalCar.Show();
        }
    }
}