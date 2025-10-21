using System;
using System.IO;
using Xunit;

namespace Lab9.Tests
{
    public class ProgramTests
    {
        private StringWriter? _stringWriter;
        private StringReader? _stringReader;
        private TextWriter? _originalOutput;
        private TextReader? _originalInput;

        public ProgramTests()
        {
            _originalOutput = Console.Out;
            _originalInput = Console.In;
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }
    }
}