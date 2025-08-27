using backend.Models;
using Xunit;

namespace backend.ModelsTest
{
    public class CalculationTest
    {
        [Theory]
        [InlineData(1, 2, '+')]
        [InlineData("I", "II", '+')]
        [InlineData(10, "V", '-')]
        [InlineData("X", 5, '-')]
        public void Validate_WithValidInputs_DoesNotThrow(object first, object second, char op)
        {
            var calc = new Calculation(first, second, op);
            calc.Validate();
        }

        [Theory]
        [InlineData(null, 2, '+')]
        [InlineData(1, null, '+')]
        public void Validate_NullNumbers_ThrowsArgumentNullException(object first, object second, char op)
        {
            var calc = new Calculation(first, second, op);
            Assert.Throws<ArgumentNullException>(() => calc.Validate());
        }

        [Theory]
        [InlineData("ABC", 2, '+')]
        [InlineData(1, "ABC", '+')]
        [InlineData(1, "2A", '+')]
        [InlineData("1I", 2, '+')]
        public void Validate_InvalidRomanNumbers_ThrowsArgumentException(object first, object second, char op)
        {
            var calc = new Calculation(first, second, op);
            Assert.Throws<ArgumentException>(() => calc.Validate());
        }

        [Theory]
        [InlineData(1, 2, '*')]
        [InlineData("I", "II", '/')]
        [InlineData(1, 2, 'x')]
        public void Validate_InvalidOperation_ThrowsArgumentException(object first, object second, char op)
        {
            var calc = new Calculation(first, second, op);
            Assert.Throws<ArgumentException>(() => calc.Validate());
        }


        [Theory]
        [InlineData(1, 2, '+', 3)]
        [InlineData(5, 2, '-', 3)]
        [InlineData("I", "II", '+', 3)]
        [InlineData("V", "II", '-', 3)]
        [InlineData(10, "V", '+', 15)]
        [InlineData("X", 5, '-', 5)]
        [InlineData("X", "V", '+', 15)]
        [InlineData("XX", "X", '-', 10)]
        public void Calculate_WithValidInputs_ReturnsExpectedResult(object first, object second, char op, int expected)
        {
            var calc = new Calculation(first, second, op);
            int result = calc.calculate();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, '*')]
        [InlineData("I", "II", '/')]
        [InlineData(1, 2, 'x')]
        public void Calculate_WithInvalidOperation_ThrowsArgumentException(object first, object second, char op)
        {
            var calc = new Calculation(first, second, op);
            Assert.Throws<ArgumentException>(() => calc.calculate());
        }

        [Theory]
        [InlineData("I", "VV", '+')] 
        [InlineData("IIII", "II", '+')] 
        public void Calculate_WithInvalidRomanNumeral_ThrowsArgumentException(object first, object second, char op)
        {
            var calc = new Calculation(first, second, op);
            Assert.Throws<ArgumentException>(() => calc.calculate());
        }

    }
}