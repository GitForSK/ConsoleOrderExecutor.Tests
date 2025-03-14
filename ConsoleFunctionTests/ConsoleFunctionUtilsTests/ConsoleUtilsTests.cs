using ConsoleOrderExecutor.ConsoleFunction.Utils;

namespace ConsoleOrderExecutor.Tests.ConsoleFunctionTests.ConsoleFunctionUtilsTests
{
    public class ConsoleUtilsTests
    {

        [Fact]
        public void ConsoleUtils_GetParameter_ValuePassed_ReturnFalse()
        {
            //Arrange
            ConsoleUtils consoleUtils = new();
            string text = "text";
            static bool predicate(string? a) => a != null;
            var strReader = new StringReader("test");
            Console.SetIn(strReader);
            string outValue = "test";

            //Act
            var result = consoleUtils.GetParameter(text, predicate, out string? value);

            //Assert
            Assert.False(result);
            Assert.Equal(outValue, value);
        }

        [Fact]
        public void ConsoleUtils_GetParameter_ExitPassed_ReturnTrue()
        {
            //Arrange
            ConsoleUtils consoleUtils = new();
            string text = "text";
            static bool predicate(string? a) => a != null;
            var strReader = new StringReader("exit");
            Console.SetIn(strReader);

            //Act
            var result = consoleUtils.GetParameter(text, predicate, out string? value);

            //Assert
            Assert.Null(value);
            Assert.True(result);
        }
        [Fact]
        public void ConsoleUtils_GetParameter_InvalidFirstValue_ReturnFalse()
        {
            //Arrange
            ConsoleUtils consoleUtils = new();
            string text = "text";
            static bool predicate(string? a) => a != "text";
            var input = String.Join(Environment.NewLine, new[]
            {
                "text", "test"
            });
            var strReader = new StringReader(input);
            Console.SetIn(strReader);
            var output = String.Join(Environment.NewLine, new[]
           {
                text, "Invalid value.", ""
            });
            var strWriter = new StringWriter();
            Console.SetOut(strWriter);
            string outValue = "test";

            //Act
            var result = consoleUtils.GetParameter(text, predicate, out string? value);

            //Assert
            Assert.Equal(output, strWriter.ToString());
            Assert.Equal(outValue, value);
            Assert.False(result);
        }

    }
}
