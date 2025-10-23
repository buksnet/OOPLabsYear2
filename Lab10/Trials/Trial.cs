using System.Collections;

namespace Trials
{
    public class Trial: IInit, ICloneable, IComparable<Trial>, IComparer
    {
        private string name;
        private int duration;

        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    name = value;
                else
                    name = "Вступительное испытание";
            }
        }

        public int Duration
        {
            get { return duration; }
            set
            {
                if (value >= 0)
                    duration = value;
                else
                    duration = 0;
            }
        }

        // Конструкторы
        public Trial()
        {
            this.name = "Вступительное испытание";
            this.duration = 60; // значение по умолчанию
        }

        public Trial(string name, int duration)
        {
            this.name = name;
            this.duration = duration;
        }

        public Trial(Trial trial)
        {
            this.name = trial.name;
            this.duration = trial.duration;
        }

        public void Init()
        {
            this.RandomInit();
        }

        // Методы
        public void Init(string name, int duration)
        {
            this.Name = name;
            this.Duration = duration;
        }

        public virtual void RandomInit()
        {
            var rng = new Random();
            string[] names = { "Экзамен", "Зачёт", "Дифф. Зачёт" };

            this.Name = names[rng.Next(names.Length)];
            this.Duration = rng.Next(30, 181); // от 30 до 180 минут
        }

        public virtual void Show()
        {
            Console.WriteLine($"[Испытание]: {Name}, длительность: {Duration} мин.");
        }

        // Метод Equals (требование задания)
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (obj is Trial other)
            {
                return Name == other.Name && Duration == other.Duration;
            }
            return false;
        }

        // Для работы с коллекциями
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Duration);
        }

        public int CompareTo(Trial? other)
        {
            if (other == null) return -1;

            // Уровни приоритета:
            // 1. Государственные экзамены (FinalExam с IsStateExam = true)
            // 2. Выпускные экзамены (FinalExam)
            // 3. Обычные экзамены (Exam)
            // 4. Тесты (Test)
            // 5. Испытания (Trial)

            int thisPriority = GetPriorityLevel(this);
            int otherPriority = GetPriorityLevel(other);

            // Сначала сравниваем по приоритету
            if (thisPriority != otherPriority)
                return thisPriority.CompareTo(otherPriority);

            // Если приоритет одинаковый, сравниваем по длительности
            return Duration.CompareTo(other.Duration);
        }

        private int GetPriorityLevel(Trial trial)
        {
            return trial switch
            {
                FinalExam { IsStateExam: true } => 1,  // Высший приоритет
                FinalExam => 2,
                Exam => 3,
                Test => 4,
                Trial => 5,                           // Низший приоритет
                _ => 6
            };
        }

        // Реализация IComparer
        public int Compare(object? x, object? y)
        {
            if (x is Trial trial1 && y is Trial trial2)
            {
                return trial1.CompareTo(trial2);
            }
            return 0;
        }

        public virtual object Clone()
        {
            var clone = new Trial();
            clone.Name = Name;
            clone.Duration = Duration;
            return clone;
        }

        public static int BinarySearch(Trial[] array, Trial target)
        {
            if (array == null || array.Length == 0)
                return -1;

            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int comparison = array[mid].CompareTo(target);

                if (comparison == 0)
                    return mid; // Найден
                else if (comparison < 0)
                    left = mid + 1; // Искомый элемент справа
                else
                    right = mid - 1; // Искомый элемент слева
            }

            return -1; // Не найден
        }

        // Перегруженный метод для поиска по критериям
        public static int BinarySearch(Trial[] array, string name, int duration)
        {
            var temp = new Trial(name, duration);
            return BinarySearch(array, temp);
        }
    }
}