using Trials;

namespace Lab10.Tests
{
    public class TrialTests
    {
        [Fact]
        public void Trial_DefaultConstructor_SetsDefaultValues()
        {
            var trial = new Trial();
            Assert.Equal("Вступительное испытание", trial.Name);
            Assert.Equal(60, trial.Duration);
        }

        [Fact]
        public void Trial_ParameterizedConstructor_SetsValues()
        {
            var trial = new Trial("Тест", 120);
            Assert.Equal("Тест", trial.Name);
            Assert.Equal(120, trial.Duration);
        }

        [Fact]
        public void Trial_CopyConstructor_CopiesValues()
        {
            var original = new Trial("Оригинал", 90);
            var copy = new Trial(original);
            Assert.Equal(original.Name, copy.Name);
            Assert.Equal(original.Duration, copy.Duration);
        }

        [Theory]
        [InlineData("", "Вступительное испытание")]
        [InlineData("   ", "Вступительное испытание")]
        [InlineData("Valid Name", "Valid Name")]
        public void Trial_NameProperty_ValidatesInput(string input, string expected)
        {
            var trial = new Trial();
            trial.Name = input;
            Assert.Equal(expected, trial.Name);
        }

        [Theory]
        [InlineData(-5, 0)]
        [InlineData(0, 0)]
        [InlineData(100, 100)]
        public void Trial_DurationProperty_ValidatesInput(int input, int expected)
        {
            var trial = new Trial();
            trial.Duration = input;
            Assert.Equal(expected, trial.Duration);
        }

        [Fact]
        public void Trial_Equals_ReturnsTrueForSameObjects()
        {
            var trial1 = new Trial("Тест", 60);
            var trial2 = new Trial("Тест", 60);
            Assert.True(trial1.Equals(trial2));
        }

        [Fact]
        public void Trial_Equals_ReturnsFalseForDifferentObjects()
        {
            var trial1 = new Trial("Тест1", 60);
            var trial2 = new Trial("Тест2", 90);
            Assert.False(trial1.Equals(trial2));
        }

        [Fact]
        public void Trial_Equals_ReturnsFalseForNull()
        {
            var trial = new Trial();
            Assert.False(trial.Equals(null));
        }

        [Fact]
        public void Trial_Equals_ReturnsFalseForDifferentType()
        {
            var trial = new Trial();
            var car = new Car();
            Assert.False(trial.Equals(car));
        }

        [Fact]
        public void Trial_GetHashCode_ReturnsSameForEqualObjects()
        {
            var trial1 = new Trial("Тест", 60);
            var trial2 = new Trial("Тест", 60);
            Assert.Equal(trial1.GetHashCode(), trial2.GetHashCode());
        }

        [Fact]
        public void Trial_Clone_CreatesNewInstance()
        {
            var original = new Trial("Оригинал", 75);
            var clone = (Trial)original.Clone();
            Assert.Equal(original.Name, clone.Name);
            Assert.Equal(original.Duration, clone.Duration);
            Assert.NotSame(original, clone);
        }

        [Fact]
        public void Trial_RandomInit_SetsValidValues()
        {
            var trial = new Trial();
            trial.RandomInit();
            Assert.False(string.IsNullOrWhiteSpace(trial.Name));
            Assert.True(trial.Duration >= 30 && trial.Duration <= 180);
        }

        [Fact]
        public void Trial_Init_Method_SetsValues()
        {
            var trial = new Trial();
            trial.Init("Новое имя", 100);
            Assert.Equal("Новое имя", trial.Name);
            Assert.Equal(100, trial.Duration);
        }

        [Fact]
        public void Trial_Show_ExecutesWithoutException()
        {
            var trial = new Trial();
            var exception = Record.Exception(() => trial.Show());
            Assert.Null(exception);
        }

        [Fact]
        public void Trial_CompareTo_ReturnsMinusOneForNull()
        {
            var trial = new Trial();
            var result = trial.CompareTo(null);
            Assert.Equal(-1, result);
        }

        [Fact]
        public void Trial_CompareTo_ComparesByPriorityAndDuration()
        {
            var trial1 = new Trial("A", 60);
            var trial2 = new Trial("B", 90);
            var result = trial1.CompareTo(trial2);
            Assert.True(result != 0);
        }
    }

    public class TestTests
    {
        [Fact]
        public void Test_DefaultConstructor_SetsDefaultValues()
        {
            var test = new Test();
            Assert.Equal("Тест", test.Name);
            Assert.Equal(90, test.Duration);
            Assert.Equal(15, test.QuestionCount);
        }

        [Fact]
        public void Test_ParameterizedConstructor_SetsValues()
        {
            var test = new Test("Математика", 120, 25);
            Assert.Equal("Математика", test.Name);
            Assert.Equal(120, test.Duration);
            Assert.Equal(25, test.QuestionCount);
        }

        [Fact]
        public void Test_CopyConstructor_CopiesValues()
        {
            var original = new Test("Оригинал", 90, 20);
            var copy = new Test(original);
            Assert.Equal(original.Name, copy.Name);
            Assert.Equal(original.Duration, copy.Duration);
            Assert.Equal(original.QuestionCount, copy.QuestionCount);
        }

        [Theory]
        [InlineData(-5, 15)] // остаётся значение по умолчанию
        [InlineData(0, 15)]  // остаётся значение по умолчанию
        [InlineData(10, 10)] // устанавливается
        public void Test_QuestionCountProperty_ValidatesInput(int input, int expected)
        {
            var test = new Test();
            test.QuestionCount = input;
            Assert.Equal(expected, test.QuestionCount);
        }

        [Fact]
        public void Test_Equals_ReturnsTrueForSameObjects()
        {
            var test1 = new Test("Тест", 60, 15);
            var test2 = new Test("Тест", 60, 15);
            Assert.True(test1.Equals(test2));
        }

        [Fact]
        public void Test_Equals_ReturnsFalseForDifferentObjects()
        {
            var test1 = new Test("Тест1", 60, 15);
            var test2 = new Test("Тест2", 90, 25);
            Assert.False(test1.Equals(test2));
        }

        [Fact]
        public void Test_GetHashCode_ReturnsSameForEqualObjects()
        {
            var test1 = new Test("Тест", 60, 15);
            var test2 = new Test("Тест", 60, 15);
            Assert.Equal(test1.GetHashCode(), test2.GetHashCode());
        }

        [Fact]
        public void Test_Clone_CreatesNewInstance()
        {
            var original = new Test("Оригинал", 75, 20);
            var clone = (Test)original.Clone();
            Assert.Equal(original.Name, clone.Name);
            Assert.Equal(original.Duration, clone.Duration);
            Assert.Equal(original.QuestionCount, clone.QuestionCount);
            Assert.NotSame(original, clone);
        }

        [Fact]
        public void Test_RandomInit_SetsValidValues()
        {
            var test = new Test();
            test.RandomInit();
            Assert.False(string.IsNullOrWhiteSpace(test.Name));
            Assert.True(test.Duration >= 30 && test.Duration <= 180);
            Assert.True(test.QuestionCount >= 5 && test.QuestionCount <= 100);
        }

        [Fact]
        public void Test_Init_Method_SetsValues()
        {
            var test = new Test();
            test.Init("Новый тест", 100, 30);
            Assert.Equal("Новый тест", test.Name);
            Assert.Equal(100, test.Duration);
            Assert.Equal(30, test.QuestionCount);
        }
    }

    public class ExamTests
    {
        [Fact]
        public void Exam_DefaultConstructor_SetsDefaultValues()
        {
            var exam = new Exam();
            Assert.Equal("Экзамен", exam.Name);
            Assert.Equal(90, exam.Duration);
            Assert.Equal(30, exam.QuestionCount);
            Assert.Equal("Другой экзамен", exam.Subject);
        }

        [Fact]
        public void Exam_ParameterizedConstructor_SetsValues()
        {
            var exam = new Exam("Физика", 120, 40, "Физика");
            Assert.Equal("Физика", exam.Name);
            Assert.Equal(120, exam.Duration);
            Assert.Equal(40, exam.QuestionCount);
            Assert.Equal("Физика", exam.Subject);
        }

        [Theory]
        [InlineData("", "Другой экзамен")]
        [InlineData("   ", "Другой экзамен")]
        [InlineData("Математика", "Математика")]
        public void Exam_SubjectProperty_ValidatesInput(string input, string expected)
        {
            var exam = new Exam();
            exam.Subject = input;
            Assert.Equal(expected, exam.Subject);
        }

        [Fact]
        public void Exam_RandomInit_SetsValidValues()
        {
            var exam = new Exam();
            exam.RandomInit();
            Assert.False(string.IsNullOrWhiteSpace(exam.Name));
            Assert.False(string.IsNullOrWhiteSpace(exam.Subject));
            Assert.True(exam.Duration >= 30 && exam.Duration <= 360);
            Assert.True(exam.QuestionCount >= 5 && exam.QuestionCount <= 100);
        }

        [Fact]
        public void Exam_Init_Method_SetsValues()
        {
            var exam = new Exam();
            exam.Init("Новый экзамен", 150, 35, "Химия");
            Assert.Equal("Новый экзамен", exam.Name);
            Assert.Equal(150, exam.Duration);
            Assert.Equal(35, exam.QuestionCount);
            Assert.Equal("Химия", exam.Subject);
        }
    }

    public class FinalExamTests
    {
        [Fact]
        public void FinalExam_DefaultConstructor_SetsDefaultValues()
        {
            var finalExam = new FinalExam();
            Assert.Equal("Выпускной Экзамен", finalExam.Name);
            Assert.Equal(120, finalExam.Duration);
            Assert.Equal(50, finalExam.QuestionCount);
            Assert.Equal("Другой предмет", finalExam.Subject);
            Assert.False(finalExam.IsStateExam);
        }

        [Fact]
        public void FinalExam_ParameterizedConstructor_SetsValues()
        {
            var finalExam = new FinalExam("Математика", 180, 60, "Математика", true);
            Assert.Equal("Математика", finalExam.Name);
            Assert.Equal(180, finalExam.Duration);
            Assert.Equal(60, finalExam.QuestionCount);
            Assert.Equal("Математика", finalExam.Subject);
            Assert.True(finalExam.IsStateExam);
        }

        [Fact]
        public void FinalExam_IsStateExamProperty_WorksCorrectly()
        {
            var finalExam = new FinalExam();
            finalExam.IsStateExam = true;
            Assert.True(finalExam.IsStateExam);
        }

        [Fact]
        public void FinalExam_RandomInit_SetsValidValues()
        {
            var finalExam = new FinalExam();
            finalExam.RandomInit();
            Assert.False(string.IsNullOrWhiteSpace(finalExam.Name));
            Assert.False(string.IsNullOrWhiteSpace(finalExam.Subject));
            Assert.True(finalExam.Duration >= 30 && finalExam.Duration <= 360);
            Assert.True(finalExam.QuestionCount >= 5 && finalExam.QuestionCount <= 100);
        }

        [Fact]
        public void FinalExam_Init_Method_SetsValues()
        {
            var finalExam = new FinalExam();
            finalExam.Init("Госэкзамен", 200, 65, "История", true);
            Assert.Equal("Госэкзамен", finalExam.Name);
            Assert.Equal(200, finalExam.Duration);
            Assert.Equal(65, finalExam.QuestionCount);
            Assert.Equal("История", finalExam.Subject);
            Assert.True(finalExam.IsStateExam);
        }
    }

    public class CarTests
    {
        [Fact]
        public void Car_DefaultConstructor_InitializesProperties()
        {
            var car = new Car();
            Assert.Equal("Игрушечная машинка", car.Brand);
            Assert.Equal(2025, car.Year);
            Assert.Equal(85.9, car.Price);
        }

        [Fact]
        public void Car_ParameterizedConstructor_SetsValues()
        {
            var car = new Car("Toyota", 2022, 1500000);
            Assert.Equal("Toyota", car.Brand);
            Assert.Equal(2022, car.Year);
            Assert.Equal(1500000, car.Price);
        }

        [Fact]
        public void Car_Clone_CreatesDeepCopy()
        {
            var original = new Car("BMW", 2023, 2000000);
            var clone = (Car)original.Clone();
            Assert.Equal(original.Brand, clone.Brand);
            Assert.Equal(original.Year, clone.Year);
            Assert.Equal(original.Price, clone.Price);
            Assert.NotSame(original, clone);
        }

        [Fact]
        public void Car_ShallowCopy_CreatesShallowCopy()
        {
            var original = new Car("Audi", 2021, 1800000);
            var shallowCopy = original.ShallowCopy();
            Assert.Equal(original.Brand, shallowCopy.Brand);
            Assert.Equal(original.Year, shallowCopy.Year);
            Assert.Equal(original.Price, shallowCopy.Price);
            Assert.NotSame(original, shallowCopy);
        }

        [Fact]
        public void Car_RandomInit_SetsValidValues()
        {
            var car = new Car();
            car.RandomInit();
            Assert.False(string.IsNullOrWhiteSpace(car.Brand));
            Assert.True(car.Year >= 1990 && car.Year <= 2023);
            Assert.True(car.Price >= 500000 && car.Price <= 5000000);
        }
    }

    public class ComparisonTests
    {
        [Fact]
        public void Compare_DifferentTypes_SortsByPriority()
        {
            var stateExam = new FinalExam("Госэкзамен", 180, 60, "Математика", true);
            var finalExam = new FinalExam("Выпускной", 120, 50, "Физика", false);
            var exam = new Exam("Экзамен", 90, 30, "Информатика");
            var test = new Test("Тест", 60, 20);
            var trial = new Trial("Испытание", 30);

            var trials = new List<Trial> { trial, test, exam, finalExam, stateExam };
            trials.Sort();

            Assert.Same(stateExam, trials[0]);  // Высший приоритет
            Assert.Same(finalExam, trials[1]);
            Assert.Same(exam, trials[2]);
            Assert.Same(test, trials[3]);
            Assert.Same(trial, trials[4]);     // Низший приоритет
        }

        [Fact]
        public void Compare_SameType_SortsByDuration()
        {
            var trial1 = new Trial("A", 30);
            var trial2 = new Trial("B", 60);
            var trial3 = new Trial("C", 90);

            var trials = new List<Trial> { trial3, trial1, trial2 };
            trials.Sort();

            Assert.Same(trial1, trials[0]);
            Assert.Same(trial2, trials[1]);
            Assert.Same(trial3, trials[2]);
        }
    }


    public class InterfaceTests
    {
        [Fact]
        public void AllClasses_ImplementIInit()
        {
            Assert.IsAssignableFrom<IInit>(new Trial());
            Assert.IsAssignableFrom<IInit>(new Test());
            Assert.IsAssignableFrom<IInit>(new Exam());
            Assert.IsAssignableFrom<IInit>(new FinalExam());
            Assert.IsAssignableFrom<IInit>(new Car());
        }

        [Fact]
        public void Trial_ImplementsIComparable()
        {
            Assert.IsAssignableFrom<IComparable<Trial>>(new Trial());
        }

        [Fact]
        public void IInit_Methods_ExecuteWithoutException()
        {
            IInit[] objects = {
                new Trial(),
                new Test(),
                new Exam(),
                new FinalExam(),
                new Car()
            };

            foreach (var obj in objects)
            {
                var exception1 = Record.Exception(() => obj.RandomInit());
                var exception2 = Record.Exception(() => obj.Show());

                Assert.Null(exception1);
                Assert.Null(exception2);
            }
        }
    }
}