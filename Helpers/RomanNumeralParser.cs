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

    private static readonly Dictionary<int, string> IntToRomanMap = new Dictionary<int, string>
    {
        {1000, "M"},
        { 900, "CM"},
        { 500, "D"},
        { 400, "CD"},
        { 100, "C"},
        { 90, "XC"},
        { 50, "L"},
        { 40, "XL"},
        { 10, "X"},
        { 9, "IX"},
        { 5, "V"},
        { 4, "IV"},
        { 1, "I"}
    };

    public static int RomanToInt(string roman)
    {
        if (string.IsNullOrWhiteSpace(roman))
            throw new ArgumentException("Input cannot be null or empty.");
        try
        {
            return RomanToInt(roman.ToUpper(), 0, 0, false);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("Invalid Roman numeral: \"" + roman + "\"");
        }
    }

    public static int RomanToInt(string roman, int previousSum, int previousCharValue, bool previousWasSubtractive)
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
                // Subtractive notation, give previousCharValue to mitigate Double subtraction
                return RomanToInt(roman[..^1], previousSum - currentValue, previousCharValue, true);
            }
            else
            {
                // Double addition of numbers with 5 is not allowed, as there are already next numbers available
                if (currentValue == previousCharValue && (currentChar == 'V' || currentChar == 'L' || currentChar == 'D'))
                    throw new ArgumentException("Invalid Roman numeral.");
                // Additive notation
                return RomanToInt(roman[..^1], previousSum + currentValue, currentValue, false);
            }
        }

        throw new ArgumentException("Invalid Roman numeral.");
    }

    public static string IntToRoman(int number)
    {
        return "TODO";
    }
}
