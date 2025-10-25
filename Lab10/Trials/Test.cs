namespace Trials
{
    public class Test : Trial, IInit, ICloneable
    {
        private int questionCount;

        public int QuestionCount
        {
            get { return questionCount; }
            set
            {
                if (value > 0)
                {
                    questionCount = value;
                }
            }
        }

        // Конструкторы
        public Test()
        {
            this.Name = "Тест";
            this.Duration = 90; // значение по умолчанию
            this.QuestionCount = 15;
        }

        public Test(string name, int duration, int questionCount)
        {
            this.Name = name;    // используем свойство для валидации
            this.Duration = duration;
            this.QuestionCount = questionCount;
        }

        public Test(Test test)
        {
            this.Name = test.Name;
            this.Duration = test.Duration;
            this.QuestionCount = test.QuestionCount;
        }

        // Методы
        public virtual void Init(string name, int duration, int questionCount) 
        {
            // Здесь и далее по иерархии - используется для избегания повторения кода
            // Использует родительский метод заполнения в качестве части инициализации
            base.Init(name, duration);
            this.QuestionCount = questionCount;
        }

        public override void RandomInit()
        {
            var rng = new Random();
            
            base.RandomInit();

            this.QuestionCount = rng.Next(5, 35);
        }

        public override void Show()
        {
            Console.WriteLine($"[Тест]: {Name}, длительность: {Duration} мин., число вопросов: {QuestionCount}.");
        }

        // Метод Equals (требование задания)
        public override bool Equals(object obj)
        {
            if (obj is Test other)
            {
                return Name == other.Name && Duration == other.Duration && QuestionCount == other.QuestionCount;
            }
            return false;
        }

        // Для работы с коллекциями
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Duration, QuestionCount);
        }

        // Для IClonable
        public new object Clone()
        {
            return new Test
            {
                Name = this.Name,
                Duration = this.Duration,
                QuestionCount = this.QuestionCount
            };
        }
    }
}
