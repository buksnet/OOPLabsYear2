using System;
using Xunit;

namespace Lab9.Tests
{
    public class DiapasonTests
    {
        [Fact]
        public void Constructor_Default_CreatesZeroRange()
        {
            // Arrange & Act
            var diapason = new Diapason();

            // Assert
            Assert.Equal(0, diapason.Start);
            Assert.Equal(0, diapason.End);
        }

        [Fact]
        public void Constructor_WithParameters_CreatesCorrectRange()
        {
            // Arrange & Act
            var diapason = new Diapason(1.5, 5.5);

            // Assert
            Assert.Equal(1.5, diapason.Start);
            Assert.Equal(5.5, diapason.End);
        }

        [Fact]
        public void Constructor_WithReversedParameters_SwapsValues()
        {
            // Arrange & Act
            var diapason = new Diapason(10.0, 5.0);

            // Assert
            Assert.Equal(5.0, diapason.Start);
            Assert.Equal(10.0, diapason.End);
        }

        [Fact]
        public void Start_SetValueGreaterThanEnd_SwapsValues()
        {
            // Arrange
            var diapason = new Diapason(1.0, 5.0)
            {
                // Act
                Start = 10.0
            };

            // Assert
            Assert.Equal(5.0, diapason.Start);
            Assert.Equal(10.0, diapason.End);
        }

        [Fact]
        public void End_SetValueLessThanStart_SwapsValues()
        {
            // Arrange
            var diapason = new Diapason(5.0, 10.0)
            {
                // Act
                End = 3.0
            };

            // Assert
            Assert.Equal(3.0, diapason.Start);
            Assert.Equal(5.0, diapason.End);
        }

        [Fact]
        public void GetRangeBorders_ReturnsCorrectValues()
        {
            // Arrange
            var diapason = new Diapason(2.0, 8.0);

            // Act
            var borders = diapason.GetRangeBorders();

            // Assert
            Assert.Equal(2, borders.Length);
            Assert.Equal(2.0, borders[0]);
            Assert.Equal(8.0, borders[1]);
        }

        [Theory]
        [InlineData(5.0, true)]
        [InlineData(2.0, true)]
        [InlineData(8.0, true)]
        [InlineData(1.0, false)]
        [InlineData(9.0, false)]
        [InlineData(0.0, false)]
        public void IsInRange_ReturnsCorrectResult(double value, bool expected)
        {
            // Arrange
            var diapason = new Diapason(2.0, 8.0);

            // Act
            var result = diapason.IsInRange(value);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var diapason = new Diapason(1.23, 4.56);

            // Act
            var result = diapason.ToString();

            // Assert
            Assert.Contains("Diapason[Start:1,23", result);
            Assert.Contains("End:4,56", result);
            Assert.Contains("Length:3,33", result);
        }

        [Fact]
        public void IncrementOperator_IncreasesBothValues()
        {
            // Arrange
            var diapason = new Diapason(1.0, 3.0);

            // Act
            diapason++;

            // Assert
            Assert.Equal(2.0, diapason.Start);
            Assert.Equal(4.0, diapason.End);
        }

        [Fact]
        public void DecrementOperator_DecreasesBothValues()
        {
            // Arrange
            var diapason = new Diapason(5.0, 8.0);

            // Act
            diapason--;

            // Assert
            Assert.Equal(4.0, diapason.Start);
            Assert.Equal(7.0, diapason.End);
        }

        [Fact]
        public void LengthOperator_ReturnsCorrectLength()
        {
            // Arrange
            var diapason = new Diapason(2.0, 7.0);

            // Act
            var length = !diapason;

            // Assert
            Assert.Equal(5.0, length);
        }

        [Fact]
        public void ExplicitCastToInt_ReturnsStartAsInt()
        {
            // Arrange
            var diapason = new Diapason(3.7, 8.2);

            // Act
            int result = (int)diapason;

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void ImplicitCastToDouble_ReturnsEndAsDouble()
        {
            // Arrange
            var diapason = new Diapason(1.5, 9.8);

            // Act
            double result = diapason;

            // Assert
            Assert.Equal(9.8, result);
        }

        [Fact]
        public void AdditionOperator_AddsValueToBothEnds()
        {
            // Arrange
            var diapason = new Diapason(1.0, 4.0);

            // Act
            var result = diapason + 3;

            // Assert
            Assert.Equal(4.0, result.Start);
            Assert.Equal(7.0, result.End);
        }

        [Theory]
        [InlineData(3.0, true)]
        [InlineData(1.0, true)]
        [InlineData(4.0, true)]
        [InlineData(0.0, false)]
        [InlineData(5.0, false)]
        public void LessThanOperator_ChecksValueInRange(double value, bool expected)
        {
            // Arrange
            var diapason = new Diapason(1.0, 4.0);

            // Act
            var result = diapason < value;

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(3.0, false)]
        [InlineData(1.0, false)]
        [InlineData(4.0, false)]
        [InlineData(0.0, true)]
        [InlineData(5.0, true)]
        public void GreaterThanOperator_ChecksValueNotInRange(double value, bool expected)
        {
            // Arrange
            var diapason = new Diapason(1.0, 4.0);

            // Act
            var result = diapason > value;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ObjectCount_IncrementsOnEachCreation()
        {
            // Arrange
            var initialCount = Diapason.ObjectCount;

            // Act
            _ = new Diapason();
            _ = new Diapason(1.0, 2.0);
            _ = new Diapason(3.0, 4.0);

            // Assert
            Assert.Equal(initialCount + 3, Diapason.ObjectCount);
        }
    }
}