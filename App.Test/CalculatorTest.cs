using App.Application.Testables;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Test
{

    public class CalculatorTest
    {
        public Calculator Calculator { get; set; }
        public Mock<ICalculatorService> Mymock { get; set; }
        public CalculatorTest()
        {
            Mymock = new Mock<ICalculatorService>();
            Calculator = new Calculator(Mymock.Object);
            //Calculator = new Calculator(new CalculatorService()); // it enters multiply method in calculator service class and returns the result without using mock setup
        }

        [Fact]
        public void AddTest()
        {
            ////Arrange
            //int a = 1;
            //int b = 2;
            //var calculator = new App.Application.Testables.Calculator();
            ////Act
            //int result = calculator.Add(a, b);

            ////Assert
            //Assert.Equal<int>(3, result);

            Assert.Contains("Hello", "Hello World");
            Assert.DoesNotContain("Hellom", "Hello World");

            var names = new List<string>() { "Alice", "Bob", "Charlie" };
            Assert.Contains("Bob", names);
            Assert.Contains(names, x => x.StartsWith("C"));

            Assert.True(5 > 3);
            Assert.False("".GetType() == typeof(int));

            Assert.Matches(@"^\d{3}-\d{2}-\d{4}$", "123-45-6789");

            Assert.NotEmpty(new List<string>() { "Bob" });

            Assert.NotInRange(11, 2, 10);

            Assert.Single(new List<string>() { "OnlyOne" });

            Assert.IsType<string>("Hello");

            Assert.IsAssignableFrom<IEnumerable<string>>(new List<string>());

            Assert.IsAssignableFrom<object>(new List<string>());

            Assert.Null(null);
        }

        [Theory]
        [InlineData(1, 2, -1)]
        public void SubtractTest(int a, int b, int expectedResult)
        {
            //var calculator = new App.Application.Testables.Calculator();
            int c = Calculator.Subtract(a, b);
            Assert.Equal(expectedResult, c);
        }


        [Theory]
        [InlineData(3, 5, 15)]
        public void Multiply_simpleValues_ReturnZeroValue(int a, int b, int expectedResult)
        {
            //Mymock.Setup(x => x.Multiply(a, b)).Returns(expectedResult);
            //Assert.Equal(15,Calculator.Multiply(a, b));


            //callback, isAny()
            int actualResult = 0;
            Mymock.Setup(x => x.Multiply(It.IsAny<int>(), It.IsAny<int>()))
                .Callback<int, int>((x, y) => actualResult = x * y);

            Calculator.Multiply(a, b);
            Assert.Equal(expectedResult, actualResult);

            Calculator.Multiply(5, 20);
            Assert.Equal(100, actualResult);
        }


        [Theory]
        [InlineData(2, 5, 7)]
        [InlineData(12, 5, 17)]
        public void Add_simpleValues_ReturnTotalValue(int a, int b, int expectedResult)
        {
            //var mymock = new Mock<ICalculatorService>();
            Mymock.Setup(x => x.Add(a, b)).Returns(expectedResult);
            var actualValue = Calculator.Add(a, b);
            Assert.Equal(expectedResult, actualValue);
            Mymock.Verify(x => x.Add(a, b), Times.Once);// it must work once, if it works more than once, it will throw an error
            Mymock.Verify(x => x.Add(a, b), Times.AtLeast(1));
        }

        [Theory]
        [InlineData(3, 0)]
        public void Multiply_zeroValues_ReturnZeroValue(int a, int b)
        {
            Mymock.Setup(x => x.Multiply(a, b)).Throws(new Exception("Multiplication by zero is not allowed."));
            var ex = Assert.Throws<Exception>(() => Calculator.Multiply(a, b));
            Assert.Equal("Multiplication by zero is not allowed.", ex.Message);
        }
    }
}
