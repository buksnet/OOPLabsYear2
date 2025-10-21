using System;
using System.IO;
using Xunit;

namespace Lab9.Tests
{
    public class InterfaceTests
    {
        private StringReader? _stringReader;

        public StringWriter StringWriter { get; set; }
        public TextWriter OriginalOutput { get; set; }
        public TextReader OriginalInput { get; set; }

        public InterfaceTests()
        {
            OriginalOutput = Console.Out;
            OriginalInput = Console.In;
            StringWriter = new StringWriter();
            Console.SetOut(StringWriter);
        }

        private void SetInput(string input)
        {
            _stringReader = new StringReader(input);
            Console.SetIn(_stringReader);
        }

        [Fact]
        public void CreateDiapasonManually_ValidInput_ReturnsDiapason()
        {
            // Arrange
            SetInput("2,5\n7,8\n");

            // Act
            var result = Interface.CreateDiapasonManually();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2.5, result.Start);
            Assert.Equal(7.8, result.End);
        }

        [Fact]
        public void CreateDiapasonManually_InvalidStartInput_ReturnsNull()
        {
            // Arrange
            SetInput("invalid\n7.8\n");

            // Act
            var result = Interface.CreateDiapasonManually();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CreateDiapasonManually_InvalidEndInput_ReturnsNull()
        {
            // Arrange
            SetInput("2.5\ninvalid\n");

            // Act
            var result = Interface.CreateDiapasonManually();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CreateManualArray_InvalidCountInput_ReturnsNull()
        {
            // Arrange
            SetInput("invalid\n");

            // Act
            var result = Interface.CreateManualArray();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CreateManualArray_InvalidDiapasonInput_ReturnsNull()
        {
            // Arrange
            SetInput("1\ninvalid\n5.0\n");

            // Act
            var result = Interface.CreateManualArray();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void FindMaxDiapason_WithValidArray_ReturnsMaxDiapason()
        {
            // Arrange
            var diapasons = new[]
            {
                new Diapason(1.0, 2.0),  // length 1.0
                new Diapason(1.0, 5.0),  // length 4.0
                new Diapason(3.0, 4.0)   // length 1.0
            };
            var array = new DiapasonArray(diapasons);

            // Act
            var result = Interface.FindMaxDiapason(array);

            // Assert
            Assert.Equal(diapasons[1], result);
            Assert.Equal(4.0, !result);
        }

        [Fact]
        public void FindMaxDiapason_WithNullArray_ThrowsArgumentException()
        {
            // Arrange, Act & Assert
#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
            Assert.Throws<ArgumentException>(() => Interface.FindMaxDiapason(null));
#pragma warning restore CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
        }

        [Fact]
        public void FindMaxDiapason_WithSingleElement_ReturnsThatElement()
        {
            // Arrange
            var singleDiapason = new Diapason(1.0, 10.0);
            var array = new DiapasonArray([singleDiapason]);

            // Act
            var result = Interface.FindMaxDiapason(array);

            // Assert
            Assert.Equal(singleDiapason, result);
        }

        [Fact]
        public void FindMaxDiapason_WithEqualLengths_ReturnsFirstMax()
        {
            // Arrange
            var diapasons = new[]
            {
                new Diapason(1.0, 4.0),  // length 3.0
                new Diapason(2.0, 5.0),  // length 3.0
                new Diapason(3.0, 4.0)   // length 1.0
            };
            var array = new DiapasonArray(diapasons);

            // Act
            var result = Interface.FindMaxDiapason(array);

            // Assert
            Assert.Equal(diapasons[0], result);
        }
    }
}