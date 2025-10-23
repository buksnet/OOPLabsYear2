using Trials;

namespace Lab10
{
    public class Program
    {
        public static void Main()
        {
            Trial[] trials = new Trial[30];
            var rng = new Random();

            Console.WriteLine("Исходный массив:");
            for (int i = 0; i < trials.Length; i++)
            {
                switch (rng.Next(4))
                {
                    case 0:
                        trials[i] = new Trial();
                        break;
                    case 1:
                        trials[i] = new Test();
                        break;
                    case 2:
                        trials[i] = new Exam();
                        break;
                    case 3:
                        trials[i] = new FinalExam();
                        break;
                }
                trials[i].RandomInit();
                trials[i].Show();
            }

            var nameComparer = new NameComparer();
            
            Console.WriteLine("\nОтсортированный по имени массив:");

            Array.Sort(trials, nameComparer);
            foreach (var trial in trials)
            {
                trial.Show();
            }

            Console.WriteLine("\nОтсортированный по 'важности' массив:");

            Array.Sort(trials);
            foreach (var trial in trials)
            {
                trial.Show();
            }

            // Демонстрация бинарного поиска
            Demo.DemonstrateBinarySearch(trials);

            // Демонстрация интеграции класса, созданного под тот же интерфейс
            Demo.DemonstrateUniversalArray();
            Demo.DemonstrateCloning();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}