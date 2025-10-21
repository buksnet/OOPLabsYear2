using System;
using Xunit;

namespace Lab9.Tests
{
    public class DiapasonArrayTests
    {
        [Fact]
        public void Constructor_Default_CreatesSingleElementArray()
        {
            // Arrange & Act
            var array = new DiapasonArray();

            // Assert
            Assert.NotNull(array.Array);
            Assert.Single(array.Array);
            Assert.Equal(1, array.Length);
        }

        [Fact]
        public void Constructor_WithPositiveAmount_CreatesArrayWithRandomValues()
        {
            // Arrange & Act
            var array = new DiapasonArray(3);

            // Assert
            Assert.NotNull(array.Array);
            Assert.Equal(3, array.Length);
            Assert.All(array.Array, diapason => Assert.NotNull(diapason));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void Constructor_WithInvalidAmount_ThrowsArgumentException(int amount)
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new DiapasonArray(amount));
        }

        [Fact]
        public void Constructor_WithDiapasonArray_CreatesCorrectArray()
        {
            // Arrange
            var diapasons = new[]
            {
                new Diapason(1.0, 2.0),
                new Diapason(3.0, 4.0)
            };

            // Act
            var array = new DiapasonArray(diapasons);

            // Assert
            Assert.NotNull(array.Array);
            Assert.Equal(2, array.Length);
            Assert.Equal(diapasons, array.Array);
        }

        [Fact]
        public void Constructor_WithNullArray_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
            Assert.Throws<ArgumentNullException>(() => new DiapasonArray(null));
#pragma warning restore CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
        }

        [Fact]
        public void ArrayProperty_SetNull_ThrowsArgumentNullException()
        {
            // Arrange
            var array = new DiapasonArray();

            // Act & Assert
#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
            Assert.Throws<ArgumentNullException>(() => array.Array = null);
#pragma warning restore CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
        }

        [Fact]
        public void ArrayProperty_SetValidArray_UpdatesCorrectly()
        {
            // Arrange
            var array = new DiapasonArray();
            var newDiapasons = new[] { new Diapason(1.0, 2.0) };

            // Act
            array.Array = newDiapasons;

            // Assert
            Assert.Equal(newDiapasons, array.Array);
        }

        [Fact]
        public void LengthProperty_ReturnsCorrectLength()
        {
            // Arrange
            var diapasons = new[] { new Diapason(1.0, 2.0), new Diapason(3.0, 4.0) };
            var array = new DiapasonArray(diapasons);

            // Act & Assert
            Assert.Equal(2, array.Length);
        }

        [Fact]
        public void ToString_EmptyArray_ReturnsCorrectFormat()
        {
            // Arrange
            var array = new DiapasonArray(1);

            // Act
            var result = array.ToString();

            // Assert
            Assert.Contains("DiapasonArray: {", result);
        }

        [Fact]
        public void ToString_SingleElement_ReturnsCorrectFormat()
        {
            // Arrange
            var array = new DiapasonArray(1);

            // Act
            var result = array.ToString();

            // Assert
            Assert.StartsWith("DiapasonArray: {Diapason[Start:", result);
            Assert.EndsWith("]};", result);
        }

        [Fact]
        public void ToString_MultipleElements_ReturnsCorrectFormat()
        {
            // Arrange
            var diapasons = new[] { new Diapason(1.0, 2.0), new Diapason(3.0, 4.0) };
            var array = new DiapasonArray(diapasons);

            // Act
            var result = array.ToString();

            // Assert
            Assert.Contains("DiapasonArray: {", result);
            Assert.Contains("Diapason[Start:1,00, End:2,00, Length:1,00]", result);
            Assert.Contains("Diapason[Start:3,00, End:4,00, Length:1,00]", result);
            Assert.EndsWith("};", result);
        }

        [Fact]
        public void Indexer_GetValidIndex_ReturnsCorrectElement()
        {
            // Arrange
            var diapason1 = new Diapason(1.0, 2.0);
            var diapason2 = new Diapason(3.0, 4.0);
            var array = new DiapasonArray([diapason1, diapason2]);

            // Act & Assert
            Assert.Equal(diapason1, array[0]);
            Assert.Equal(diapason2, array[1]);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(2)]
        [InlineData(100)]
        public void Indexer_GetInvalidIndex_ThrowsArgumentOutOfRangeException(int index)
        {
            // Arrange
            var array = new DiapasonArray(2);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => array[index]);
        }

        [Fact]
        public void Indexer_SetValidIndex_UpdatesElement()
        {
            // Arrange
            var array = new DiapasonArray(2);
            var newDiapason = new Diapason(10.0, 20.0);

            // Act
            array[0] = newDiapason;

            // Assert
            Assert.Equal(newDiapason, array[0]);
        }

        [Fact]
        public void Indexer_SetNull_ThrowsArgumentNullException()
        {
            // Arrange
            var array = new DiapasonArray(2);

            // Act & Assert
#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
            Assert.Throws<ArgumentNullException>(() => array[0] = null);
#pragma warning restore CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(2)]
        [InlineData(100)]
        public void Indexer_SetInvalidIndex_ThrowsArgumentOutOfRangeException(int index)
        {
            // Arrange
            var array = new DiapasonArray(2);
            var diapason = new Diapason(1.0, 2.0);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => array[index] = diapason);
        }

        [Fact]
        public void ObjectCount_IncrementsOnEachCreation()
        {
            // Arrange
            var initialCount = DiapasonArray.ObjectCount;

            // Act
            _ = new DiapasonArray();
            _ = new DiapasonArray(2);
            _ = new DiapasonArray([new Diapason(1.0, 2.0)]);

            // Assert
            Assert.Equal(initialCount + 3, DiapasonArray.ObjectCount);
        }
    }
}