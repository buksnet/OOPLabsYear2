using System.Collections;

namespace Trials
{
    public class Trial: IInit, ICloneable, IComparable<Trial>
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
            this.Name = "Вступительное испытание N";
            this.duration = 0;
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


        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (obj is Trial other)
            {
                return Name == other.Name && Duration == other.Duration;
            }
            return false;
        }

        // Для работы с коллекциями (напр. Hashtable)
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

        public object Clone()
        {
            return new Trial
            {
                Name = this.Name,
                Duration = this.Duration
            };
        }

        // копирует ВСЕ нестатические поля объекта и добавляет их в новый объект
        public object ShallowCopy()
        {
            return (Trial)this.MemberwiseClone();
        }
    }
}