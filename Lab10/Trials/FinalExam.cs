namespace Trials
{
    public class FinalExam : Exam, IInit, ICloneable
    {

        private bool isStateExam; // флаг - Гос. экзамен

        public bool IsStateExam
        {
            get { return isStateExam; }
            set { isStateExam = value; }
        }

        // Конструкторы
        public FinalExam()
        {
            this.Name = "Выпускной Экзамен";
            this.Duration = 120; // значения по умолчанию
            this.QuestionCount = 50;
            this.Subject = "Другой предмет";
            this.isStateExam = false;
        }

        public FinalExam(string name, int duration, int questionCount, string subject, bool isStateExam)
        {
            this.Name = name;    // используем свойство для валидации
            this.Duration = duration;
            this.QuestionCount = questionCount;
            this.Subject = subject;
            this.isStateExam = isStateExam;

        }

        public FinalExam(FinalExam exam)
        {
            this.Name = exam.Name;
            this.Duration = exam.Duration;
            this.QuestionCount = exam.QuestionCount;
            this.Subject = exam.Subject;
            this.isStateExam = exam.IsStateExam;
        }

        // Методы
        public void Init(string name, int duration, int questionCount, string subject, bool isStateExam)
        {
            this.Name = name;
            this.Duration = duration;
            this.QuestionCount = questionCount;
            this.Subject = subject;
            this.isStateExam = isStateExam;
        }

        public override void RandomInit()
        {
            var rng = new Random();
            string[] names = { "Экзамен", "Зачёт", "Дифф. Зачёт" };
            string[] subjects = { "Математика", "Физика", "Информатика", "Русский язык", "История" };

            this.Name = names[rng.Next(names.Length)];
            this.Duration = rng.Next(30, 361); // от 30 до 360 минут
            this.QuestionCount = rng.Next(30, 80);
            this.Subject = subjects[rng.Next(subjects.Length)];
            this.isStateExam = rng.Next(0, 2) > 0; // случайное boolean значение (50/50
        }

        public override void Show()
        {
            Console.Write($"[Выпускной экзамен]: {Name} по предмету {Subject}, длительность: {Duration} мин., число вопросов: {QuestionCount}. ");
            if (this.isStateExam)
            {
                Console.WriteLine("Государственный экзамен.");
            }
            else
            {
                Console.WriteLine("Стандартный экзамен");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is FinalExam other)
            {
                return Name == other.Name && Duration == other.Duration && QuestionCount == other.QuestionCount && Subject == other.Subject && IsStateExam == other.IsStateExam;
            }
            return false;
        }

        // Для работы с коллекциями
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Duration, QuestionCount, Subject, IsStateExam);
        }

        public new object Clone()
        {
            return new FinalExam
            {
                Name = this.Name,
                Duration = this.Duration,
                QuestionCount = this.QuestionCount,
                Subject = this.Subject,
                IsStateExam = this.IsStateExam
            };
        }
    }
}
