using lib;

namespace backend.Models
{
    public class Calculation
    {
        public object FirstNumber { get; set; }
        public object SecondNumber { get; set; }
        public char Operation { get; set; }

        public Calculation(object firstNumber, object secondNumber, char operation)
        {
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;
            Operation = operation;
        }

        public void Validate()
        {
            if (FirstNumber == null)
                throw new ArgumentNullException(nameof(FirstNumber), "FirstNumber cannot be null.");

            if (SecondNumber == null)
                throw new ArgumentNullException(nameof(SecondNumber), "SecondNumber cannot be null.");

            if (FirstNumber.ToString() == null && !int.TryParse(FirstNumber.ToString(), out _))
                throw new ArgumentException("FirstNumber must be an int or a string.");

            if (SecondNumber.ToString() == null && !int.TryParse(SecondNumber.ToString(), out _))
                throw new ArgumentException("SecondNumber must be an int or a string.");

            // Int32 conversion because isEnumDefined does not work on Chars
            if (!typeof(Operations).IsEnumDefined((Int32)Operation))
                throw new ArgumentException("Operation must be '+' or '-'.");

            if (!int.TryParse(FirstNumber.ToString(), out _))
                RomanNumeralParser.RomanToInt(FirstNumber.ToString());

            if (!int.TryParse(SecondNumber.ToString(), out _))
                RomanNumeralParser.RomanToInt(SecondNumber.ToString());
        }

        public int calculate()
        {
            int first = int.TryParse(FirstNumber.ToString(), out _) ? int.Parse(FirstNumber.ToString()) : RomanNumeralParser.RomanToInt(FirstNumber.ToString());
            int second = int.TryParse(SecondNumber.ToString(), out _) ? int.Parse(SecondNumber.ToString()) : RomanNumeralParser.RomanToInt(SecondNumber.ToString());

            return ((Int32)Operation) switch
            {
                (Int32)Operations.Addition => first + second,
                (Int32)Operations.Subtraction => first - second,
                _ => throw new ArgumentException("Unsupported operation."),
            };
        }
    }
}