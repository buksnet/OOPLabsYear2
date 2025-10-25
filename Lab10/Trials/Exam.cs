namespace Trials
{
    public class Exam : Test, IInit, ICloneable
    {

        private string subject;

        public string Subject
        {
            get { return subject; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    subject = value;
                else
                    subject = "Другой экзамен";
            }
        }

        // Конструкторы
        public Exam()
        {
            this.Name = "Экзамен";
            this.Duration = 90; // значения по умолчанию
            this.QuestionCount = 30;
            this.subject = "Другой экзамен";
        }

        public Exam(string name, int duration, int questionCount, string subject)
        {
            this.Name = name;    // используем свойство для валидации
            this.Duration = duration;
            this.QuestionCount = questionCount;
            this.subject = subject;
        }

        public Exam(Exam exam)
        {
            this.Name = exam.Name;
            this.Duration = exam.Duration;
            this.QuestionCount = exam.QuestionCount;
            this.subject = exam.Subject;
        }

        // Методы
        public virtual void Init(string name, int duration, int questionCount, string subject)
        {
            base.Init(name, duration, questionCount);
            this.Subject = subject;
        }

        public override void RandomInit()
        {
            var rng = new Random();

            string[] subjects = { "Математика", "Физика", "Информатика", "Русский язык", "История" };

            base.RandomInit();
            this.Subject = subjects[rng.Next(subjects.Length)];
        }

        public override void Show()
        {
            Console.WriteLine($"[Экзамен]: {Name} по предмету {Subject}, длительность: {Duration} мин., число вопросов: {QuestionCount}.");
        }

        // Метод Equals (требование задания)
        public override bool Equals(object obj)
        {
            if (obj is Exam other)
            {
                return Name == other.Name && Duration == other.Duration && QuestionCount == other.QuestionCount && Subject == other.Subject;
            }
            return false;
        }

        // Для работы с коллекциями
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Duration, QuestionCount, Subject);
        }

        // Для IClonable
        public new object Clone()
        {
            return new Exam
            {
                Name = this.Name,
                Duration = this.Duration,
                QuestionCount = this.QuestionCount,
                Subject = this.Subject
            };
        }
    }
}
