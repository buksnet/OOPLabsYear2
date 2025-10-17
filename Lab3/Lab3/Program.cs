using System.Globalization;

Taylor solvation = new();
Console.WriteLine("Вычисление функции y=e^x".PadLeft(50));
Console.WriteLine(solvation.ToString());
Console.WriteLine("\nНажмите любую клавишу для закрытия программы...");
Console.ReadKey();


// Класс для вычисления значения суммы ряда тейлора для функции y=e^x
public class Taylor
{
    #region Constraints
    private const double _EPS = 0.0001;
    private const int _N = 15;
    #endregion

    #region Calculations
    public double CalculateY(double x, bool useEpsilon)
    {
        double result = 1.0;
        int iterationNumber = 1;
        Dictionary<byte, double> resultsDict = new Dictionary<byte, double>() // Словарь, хранящий в себе предыдущее и текущее значения функции
        {
            {0, 0.0},
            {1, 1.0},
        };

        if (useEpsilon)
        {
            while (Math.Abs(resultsDict[1] - resultsDict[0]) > _EPS)
            {
                resultsDict[0] = resultsDict[1];
                resultsDict[1] = Math.Pow(x, iterationNumber) / _Factorial(iterationNumber);
                result += resultsDict[1];
                iterationNumber++;
            }

        }
        else
        {
            for (int i = 1; i <= _N; i++)
            {
                resultsDict[0] = resultsDict[1];
                resultsDict[1] = Math.Pow(x, i) / _Factorial(i);
                result += resultsDict[1];
            }
        }
        return result;
    }
    #endregion

    #region Overloads

    override public string ToString()
    {
        string result = "";
        result += "X".PadRight(24)+"SN".PadRight(24)+"SE".PadRight(24)+"Y".PadRight(24) + '\n';
        result += _Separator(84) + '\n';
        for (double x = 1; x < 2; x += 0.1)
        {
            result += $"X={_XToString(x)}".PadRight(24);
            result += $"SN={CalculateY(x, false)}".PadRight(24);
            result += $"SE={CalculateY(x, true)}".PadRight(24);
            result += $"Y={Math.Exp(x)}".PadRight(24);
            result += '\n';
        }

        return result;
    }

    #endregion

    #region Utils
    private static double _Factorial(double x)
    {
        if (x <= 1)
            return 1;
        return x * _Factorial(x - 1);
    }

    private static string _Separator(int count)
    {
        if (count > 1)
            return "-" + _Separator(count - 1);
        else return "";
    }

    private string _XToString(double x)
    {
        if (x == 1.0)
        {
            return "1.0";
        }
        char possibleDot = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
        string[] number = x.ToString().Split(possibleDot);
        return number[0] + possibleDot + (number.Length > 1 ? (number[1].Length > 1 ? number[1].Substring(0, 1) : number[1]) : "0000");
    }
    #endregion

}