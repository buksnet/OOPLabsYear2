namespace Lab5.Tests
{
    public class DynamicArrayTests
    {
        /// <summary>
        /// Тестирование корректности создания одномерного массива
        /// </summary>
        [Fact]
        public void MakeArray_SingleDimension_CreatesCorrectly()
        {
            // Arrange
            var array = new DynamicArray();

            // Act
            array.MakeArray(1, 5);

            // Assert
            Assert.True(array.IsSingleDismension());
        }

        /// <summary>
        /// Тестирование корректности создания двумерного массива
        /// </summary>
        [Fact]
        public void MakeArray_TwoDimension_CreatesCorrectly()
        {
            // Arrange
            var array = new DynamicArray();

            // Act
            array.MakeArray(3, 4);

            // Assert
            Assert.False(array.IsSingleDismension());
        }

        /// <summary>
        /// Тестирование корректности создания рваного массива
        /// </summary>
        [Fact]
        public void MakeArray_JaggedArray_CreatesCorrectly()
        {
            // Arrange
            var array = new DynamicArray();
            var elemCounts = new List<int> { 2, 5, 3 };

            // Act
            array.MakeArray(3, elemCounts);

            // Assert
            Assert.False(array.IsSingleDismension());
        }

        /// <summary>
        /// Проверка на обработку ошибки для команды на создание массива с 0 строк
        /// </summary>
        [Fact]
        public void MakeArray_InvalidRows_ThrowsException()
        {
            // Arrange
            var array = new DynamicArray();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => array.MakeArray(0, 5));
        }

        /// <summary>
        /// Проверка обработки ошибки при передачи списка с некорректным числом аргументов
        /// </summary>
        [Fact]
        public void MakeArray_JaggedInvalidCounts_ThrowsException()
        {
            // Arrange
            var array = new DynamicArray();
            var elemCounts = new List<int> { 2, 3 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => array.MakeArray(3, elemCounts));
        }

        /// <summary>
        /// Проверка заполнения массива случайными числами
        /// </summary>
        [Fact]
        public void RandomFill_NotEmptyArray_FillsWithValues()
        {
            // Arrange
            var array = new DynamicArray();
            array.MakeArray(2, 3);

            // Act
            array.RandomFill();

            // Assert
            Assert.False(array.IsSingleDismension());
        }

        /// <summary>
        /// Тест удаления элемента
        /// </summary>
        [Fact]
        public void RemoveThisElement_SingleDimension_RemovesElement()
        {
            // Arrange
            var array = new DynamicArray();
            array.MakeArray(1, 3);
            array.RandomFill();

            // Act
            array.RemoveThisElement(2);

            // Assert
            Assert.True(array.IsSingleDismension());
        }

        /// <summary>
        /// Тест добавления строки в начало пустого массива
        /// </summary>
        [Fact]
        public void AddRowAtBeginning_EmptyArray_CreatesNewArray()
        {
            // Arrange
            var array = new DynamicArray();
            int[] newRow = [1, 2, 3];

            // Act
            array.AddRowAtBeginning(newRow);

            // Assert
            Assert.True(array.IsSingleDismension());
        }

        /// <summary>
        /// Добавление строки в начало существующего массива
        /// </summary>
        [Fact]
        public void AddRowAtBeginning_ExistingArray_AddsRow()
        {
            // Arrange
            var array = new DynamicArray();
            array.MakeArray(2, 3);
            array.RandomFill();
            int initialRowCount = array.RowCount;
            int[] newRow = [7, 8, 9];

            // Act
            array.AddRowAtBeginning(newRow);

            // Assert
            Assert.Equal(initialRowCount + 1, array.RowCount);
        }

        /// <summary>
        /// Тест удаления строк с указаной строки
        /// </summary>
        [Fact]
        public void RemoveRowsStartingFrom_ValidRange_RemovesRows()
        {
            // Arrange
            var array = new DynamicArray();
            array.MakeArray(5, 2);
            array.RandomFill();
            int initialRowCount = array.RowCount;

            // Act
            array.RemoveRowsStartingFrom(2, 2);

            // Assert
            Assert.Equal(initialRowCount - 2, array.RowCount);
        }

        /// <summary>
        /// Тест удаления всех строк в массиве
        /// </summary>
        [Fact]
        public void RemoveRowsStartingFrom_RemoveAllRows_ArrayBecomesEmpty()
        {
            // Arrange
            var array = new DynamicArray();
            array.MakeArray(3, 2);
            array.RandomFill();

            // Act
            array.RemoveRowsStartingFrom(1, 5);

            // Assert
            Assert.Equal(0, array.RowCount);
        }

        /// <summary>
        /// Тесты создания массивов различных размеров
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 5)]
        [InlineData(10, 2)]
        public void MakeArray_VariousSizes_CreatesCorrectly(int rows, int columns)
        {
            // Arrange
            var array = new DynamicArray();

            // Act
            array.MakeArray(rows, columns);

            // Assert
            if (rows == 1)
                Assert.True(array.IsSingleDismension());
            else
                Assert.False(array.IsSingleDismension());
        }
    }
}