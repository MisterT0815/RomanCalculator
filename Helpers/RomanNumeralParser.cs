namespace Helpers;
public static class RomanNumeralParser
{
    private static readonly Dictionary<char, int> RomanToIntMap = new Dictionary<char, int>
    {
        {'I', 1},
        {'V', 5},
        {'X', 10},
        {'L', 50},
        {'C', 100},
        {'D', 500},
        {'M', 1000}
    };

    private static readonly List<KeyValuePair<int, string>> IntToRomanMap = new List<KeyValuePair<int, string>>
    {
        new KeyValuePair<int, string>(1000, "M"),
        new KeyValuePair<int, string>(900, "CM"),
        new KeyValuePair<int, string>(500, "D"),
        new KeyValuePair<int, string>(400, "CD"),
        new KeyValuePair<int, string>(100, "C"),
        new KeyValuePair<int, string>(90, "XC"),
        new KeyValuePair<int, string>(50, "L"),
        new KeyValuePair<int, string>(40, "XL"),
        new KeyValuePair<int, string>(10, "X"),
        new KeyValuePair<int, string>(9, "IX"),
        new KeyValuePair<int, string>(5, "V"),
        new KeyValuePair<int, string>(4, "IV"),
        new KeyValuePair<int, string>(1, "I")
    };

    public static int RomanToInt(string roman)
    {
        if (string.IsNullOrWhiteSpace(roman))
            throw new ArgumentException("Input cannot be null or empty.");
        try
        {
            return RomanToInt(roman.ToUpper(), 0, 0, false, 0);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("Invalid Roman numeral: \"" + roman + "\"");
        }
    }

    public static int RomanToInt(string roman, int previousSum, int previousCharValue, bool previousWasSubtractive, int numberOfSamePreviousChar)
    {
        //Default Case
        if (string.IsNullOrEmpty(roman)) return previousSum;

        // Get the rightmost character
        char currentChar = roman[roman.Length - 1];

        if (RomanToIntMap.TryGetValue(currentChar, out int currentValue))
        {
            if (currentValue < previousCharValue)
            {
                // Double subtraction is not allowed
                if (previousWasSubtractive)
                    throw new ArgumentException("Invalid Roman numeral.");
                // Subtraction after double of same character is not allowed
                if (numberOfSamePreviousChar > 1)
                    throw new ArgumentException("Invalid Roman numeral.");
                // Subtractive notation, give previousCharValue to mitigate Double subtraction
                return RomanToInt(roman[..^1], previousSum - currentValue, previousCharValue, true, 0);
            }
            else if (currentValue == previousCharValue)
            {
                // Quadruple addition of same number is not allowed
                if (numberOfSamePreviousChar == 3)
                    throw new ArgumentException("Invalid Roman numeral.");
                // Double addition of numbers with 5 is not allowed, as there are already next numbers available
                if (currentChar == 'V' || currentChar == 'L' || currentChar == 'D')
                    throw new ArgumentException("Invalid Roman numeral.");
                // Additive notation, increase numberOfSamePreviousChar, keep previousWasSubtractive to ensure no double of same subtraction: IXIX
                return RomanToInt(roman[..^1], previousSum + currentValue, currentValue, previousWasSubtractive, numberOfSamePreviousChar + 1);
            }
            else
            {
                // Additive notation
                return RomanToInt(roman[..^1], previousSum + currentValue, currentValue, false, 1);
            }
        }

        throw new ArgumentException("Invalid Roman numeral.");
    }

    public static string IntToRoman(int number)
    {
        if (number <= 0)
            throw new ArgumentException("Input must be a positive integer.");

        var roman = new System.Text.StringBuilder();

        foreach (var pair in IntToRomanMap)
        {
            while (number >= pair.Key)
            {
                roman.Append(pair.Value);
                number -= pair.Key;
            }
        }

        return roman.ToString();
    }
}
