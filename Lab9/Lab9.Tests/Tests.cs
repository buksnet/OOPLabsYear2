using System;
using System.IO;
using Xunit;

namespace Lab9.Tests
{
    public class Tests
    {
        private StringWriter? _stringWriter;
        private StringReader? _stringReader;
        private TextWriter? _originalOutput;
        private TextReader? _originalInput;

        public Tests()
        {
            _originalOutput = Console.Out;
            _originalInput = Console.In;
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        private static void SetInput(string input)
        {
            var _stringReader = new StringReader(input);
            Console.SetIn(_stringReader);
        }

        [Fact]
        public void ObjectCount_IntegrationTest()
        {
            // Act - создаем несколько объектов
            var diapason1 = new Diapason(1.0, 2.0);
            var diapason2 = new Diapason(3.0, 4.0);
            _ = new DiapasonArray([diapason1, diapason2]);

            // 5, т.к. при создании каждого diapason и diapasonArray вызывается конструктор diapason
            Assert.Equal(5, Diapason.ObjectCount);
            // 3, т.к. каждый отдельный diapason считается отдельным массивом
            Assert.Equal(3, DiapasonArray.ObjectCount);
        }
    }
}