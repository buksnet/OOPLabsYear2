using Trials;

namespace Lab10
{
    public class Requests
    {
        /// <summary>
        /// Находит все государственные экзамены, длящиеся дольше 3 часов
        /// </summary>
        /// <param name="allTrials"> массив объектов для поиска </param>
        /// <returns> массив объектов FinalExam </returns>
        public static object[] GetFinalExamsGreaterThanThreeHours(object[] allTrials)
        {
            var finalExams = new FinalExam[allTrials.Length];
            FinalExam? tmp;

            foreach (object trial in allTrials)
            {
                if (trial is FinalExam)
                {
                    tmp = trial as FinalExam;
                    if (tmp is not null)
                    {
                        if (tmp.IsStateExam && tmp.Duration > 180)
                        finalExams.Append(tmp);
                    }
                }
            }
            if (finalExams.Length > 0) return finalExams;
            else return []; // Возвращает пустой массив если ничего не было найдено
        }

        public static int GetTotalQuestionCount(object[] allTrials)
        {
            var totalQuestionCount = 0;
            Test? test;
            foreach (object trial in allTrials)
            {
                // Узнаём, является ли объект наследником Test (т.е. содержит поле QuestionCount)
                if (trial is Test)
                {
                    test = trial as Test;
                    if (test is not null)
                    {
                        totalQuestionCount += test.QuestionCount;
                    }
                }
            }
            return totalQuestionCount;
        }

        /// <summary>
        /// Возвращает количество объектов, сгруппированное по типам.
        /// </summary>
        /// <param name="allTrials"> массив объектов </param>
        /// <returns> словарь `название класса - число объектов` </returns>
        public static Dictionary<string, int> GroupByTypes(object[] allTrials)
        {
            var result = new Dictionary<string, int>
            {
                { "Trial", 0 },
                { "Test", 0 },
                { "Exam", 0 },
                { "FinalExam", 0 }
            };

            foreach (object trial in allTrials)
            {
                if (trial is Trial) result["Trial"]++;
                if (trial is Test) result["Test"]++;
                if (trial is Exam) result["Exam"]++;
                if (trial is FinalExam) result["FinalExam"]++;
            }

            return result;
        }
    }
}