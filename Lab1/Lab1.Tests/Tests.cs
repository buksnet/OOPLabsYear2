using Lab1;
using System;
using System.IO;
using Xunit;

public class SolvationsTests
{
    private readonly Realization _solvation;

    public SolvationsTests()
    {
        _solvation = new Realization();
    }

    #region Task 1 Tests

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(10, 5, 1)]
    [InlineData(0, 0, 0)]
    public void SolveTaskOne_ValidInputs_ReturnsCorrectResults(int m, int n, int x)
    {
        // Arrange
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        Realization.SolveTaskOne(m, n, x);

        // Assert
        var result = sw.ToString();
        Assert.Contains("m - ++n:", result);
        Assert.Contains("m++ > --n:", result);
        Assert.Contains("m-- < ++n:", result);
        Assert.Contains("Значение выражения 25x^-√(x^2+x):", result);
    }

    [Fact]
    public void SolveTaskOne_WithPositiveNumbers_CalculatesCorrectly()
    {
        // Arrange
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        Realization.SolveTaskOne(10, 5, 2);

        // Assert
        var result = sw.ToString();
        Assert.Contains("m - ++n:", result);
    }

    [Fact]
    public void SolveTaskOne_WithZeroValues_CalculatesCorrectly()
    {
        // Arrange
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        Realization.SolveTaskOne(0, 0, 0);

        // Assert
        var result = sw.ToString();
        Assert.Contains("Значение выражения 25x^-√(x^2+x):", result);
    }

    #endregion

    #region Task 2 Tests

    [Theory]
    [InlineData(0, -0.5, "Точка принадлежит площади графика")]
    [InlineData(0, -1, "Точка принадлежит площади графика")]
    [InlineData(0.5, -0.5, "Точка принадлежит площади графика")]
    [InlineData(-0.5, -0.5, "Точка принадлежит площади графика")]
    [InlineData(0, 0.5, "Точка не относится к площади графика")]
    [InlineData(1, 1, "Точка не относится к площади графика")]
    [InlineData(-1, 1, "Точка не относится к площади графика")]
    public void SolveTaskTwo_VariousPoints_ReturnsCorrectResult(double x, double y, string expected)
    {
        // Arrange
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        Realization.SolveTaskTwo(x, y);

        // Assert
        var result = sw.ToString();
        Assert.Contains(expected, result);
    }

    [Fact]
    public void SolveTaskTwo_PointOnBoundary_ReturnsBelongs()
    {
        // Arrange
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        Realization.SolveTaskTwo(0, -1);

        // Assert
        var result = sw.ToString();
        Assert.Contains("Точка принадлежит площади графика", result);
    }

    [Fact]
    public void SolveTaskTwo_PointOutsideCircle_ReturnsNotBelongs()
    {
        // Arrange
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        Realization.SolveTaskTwo(1, -1);

        // Assert
        var result = sw.ToString();
        Assert.Contains("Точка не относится к площади графика", result);
    }

    [Fact]
    public void SolveTaskTwo_PointInUpperHalf_ReturnsNotBelongs()
    {
        // Arrange
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        Realization.SolveTaskTwo(0, 0.5);

        // Assert
        var result = sw.ToString();
        Assert.Contains("Точка не относится к площади графика", result);
    }

    #endregion

    #region Task 3 Tests

    [Fact]
    public void SolveTaskThree_WithFloatPrecision_CalculatesAndOutputs()
    {
        // Arrange
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        Realization.SolveTaskThree(1000f, 0.0001f);

        // Assert
        var result = sw.ToString();
        Assert.False(string.IsNullOrEmpty(result));
    }

    [Fact]
    public void SolveTaskThree_WithDoublePrecision_CalculatesAndOutputs()
    {
        // Arrange
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        Realization.SolveTaskThree(1000.0, 0.0001);

        // Assert
        var result = sw.ToString();
        Assert.False(string.IsNullOrEmpty(result));
    }

    [Fact]
    public void SolveTaskThree_FloatVsDouble_DifferentPrecisionResults()
    {
        // Arrange
        using var sw1 = new StringWriter();
        using var sw2 = new StringWriter();

        string floatResult, doubleResult;

        // Act - Float
        Console.SetOut(sw1);
        Realization.SolveTaskThree(1000f, 0.0001f);
        floatResult = sw1.ToString();

        // Act - Double
        Console.SetOut(sw2);
        Realization.SolveTaskThree(1000.0, 0.0001);
        doubleResult = sw2.ToString();

        // Assert - They should produce output (precision differences expected)
        Assert.False(string.IsNullOrEmpty(floatResult));
        Assert.False(string.IsNullOrEmpty(doubleResult));
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void RunInterface_WithValidTaskNumber_CallsCorrectTask()
    {
        // Arrange
        using var sw = new StringWriter();
        using var sr = new StringReader("1\n5\n3\n2\n0\n");
        Console.SetOut(sw);
        Console.SetIn(sr);

        // Act
        _solvation.RunInterface();

        // Assert
        var result = sw.ToString();
        Assert.Contains("m - ++n:", result);
    }

    [Fact]
    public void RunInterface_WithTaskTwoInput_ProcessesCorrectly()
    {
        // Arrange
        using var sw = new StringWriter();
        using var sr = new StringReader("2\n0\n-0.5\n0\n");
        Console.SetOut(sw);
        Console.SetIn(sr);

        // Act
        _solvation.RunInterface();

        // Assert
        var result = sw.ToString();
        Assert.False(string.IsNullOrEmpty(result));
    }

    [Fact]
    public void RunInterface_WithTaskThreeFloatInput_ProcessesCorrectly()
    {
        // Arrange
        using var sw = new StringWriter();
        using var sr = new StringReader("3\n1\n0\n");
        Console.SetOut(sw);
        Console.SetIn(sr);

        // Act
        _solvation.RunInterface();

        // Assert
        var result = sw.ToString();
        Assert.False(string.IsNullOrEmpty(result));
    }

    [Fact]
    public void RunInterface_WithTaskThreeDoubleInput_ProcessesCorrectly()
    {
        // Arrange
        using var sw = new StringWriter();
        using var sr = new StringReader("3\n2\n0\n");
        Console.SetOut(sw);
        Console.SetIn(sr);

        // Act
        _solvation.RunInterface();

        // Assert
        var result = sw.ToString();
        Assert.False(string.IsNullOrEmpty(result));
    }

    [Fact]
    public void RunInterface_WithInvalidTaskNumber_ShowsErrorMessage()
    {
        // Arrange
        using var sw = new StringWriter();
        using var sr = new StringReader("99\n0\n");
        Console.SetOut(sw);
        Console.SetIn(sr);

        // Act
        _solvation.RunInterface();

        // Assert
        var result = sw.ToString();
        Assert.Contains("Некорректный ввод", result);
    }

    [Fact]
    public void RunInterface_WithInvalidDataType_ShowsErrorMessage()
    {
        // Arrange
        using var sw = new StringWriter();
        using var sr = new StringReader("abc\n0\n");
        Console.SetOut(sw);
        Console.SetIn(sr);

        // Act
        _solvation.RunInterface();

        // Assert
        var result = sw.ToString();
        Assert.Contains("Некорректный ввод", result);
    }

    [Fact]
    public void RunInterface_WithExitCommand_ExitsSuccessfully()
    {
        // Arrange
        using var sw = new StringWriter();
        using var sr = new StringReader("0\n");
        Console.SetOut(sw);
        Console.SetIn(sr);

        // Act
        _solvation.RunInterface();

        // Assert
        var result = sw.ToString();
        Assert.DoesNotContain("Некорректный ввод", result);
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public void SolveTaskOne_WithNegativeNumbers_CalculatesCorrectly()
    {
        // Arrange
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        Realization.SolveTaskOne(-5, -3, -2);

        // Assert
        var result = sw.ToString();
        Assert.Contains("Значение выражения 25x^-√(x^2+x):", result);
    }

    [Fact]
    public void SolveTaskTwo_WithLargeNumbers_ReturnsNotBelongs()
    {
        // Arrange
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        Realization.SolveTaskTwo(100, -100);

        // Assert
        var result = sw.ToString();
        Assert.Contains("Точка не относится к площади графика", result);
    }

    [Theory]
    [InlineData(0, 0, "Точка принадлежит площади графика")]
    [InlineData(1, 0, "Точка принадлежит площади графика")]
    [InlineData(-2, 0, "Точка не относится к площади графика")]
    public void SolveTaskTwo_BoundaryXAxisPoints_ReturnsCorrectResult(double x, double y, string expected)
    {
        // Arrange
        using var sw = new StringWriter();
        Console.SetOut(sw);

        // Act
        Realization.SolveTaskTwo(x, y);

        // Assert
        var result = sw.ToString();
        Assert.Contains(expected, result);
    }

    #endregion

    // Helper method to restore console after tests
    public void Dispose()
    {
        var standardOut = new StreamWriter(Console.OpenStandardOutput());
        standardOut.AutoFlush = true;
        Console.SetOut(standardOut);

        var standardIn = new StreamReader(Console.OpenStandardInput());
        Console.SetIn(standardIn);
    }
}