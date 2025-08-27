using Helpers;

namespace HelperUnitTest;

public class RomanNumeralParserUnitTest
{
    [Theory]
    [InlineData("I", 1)]
    [InlineData("V", 5)]
    [InlineData("X", 10)]
    [InlineData("L", 50)]
    [InlineData("C", 100)]
    [InlineData("D", 500)]
    [InlineData("M", 1000)]
    public void testConvertsSimpleChars(string roman, int expectedResult)
    {
        int actualResult = RomanNumeralParser.RomanToInt(roman);

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData("IV", 4)]
    [InlineData("IX", 9)]
    [InlineData("XL", 40)]
    [InlineData("XC", 90)]
    [InlineData("CD", 400)]
    [InlineData("CM", 900)]
    public void testConvertSubtractedStrings(string roman, int expectedResult)
    {
        int actualResult = RomanNumeralParser.RomanToInt(roman);

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData("III", 3)]
    [InlineData("XX", 20)]
    [InlineData("MMCXII", 2112)]
    public void testConvertAdditiveStrings(string roman, int expectedResult)
    {
        int actualResult = RomanNumeralParser.RomanToInt(roman);

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData("IIX")]
    [InlineData("CCD")]
    public void testInvalidDoubleSubtraction(string roman)
    {
        Assert.Throws<ArgumentException>(() => RomanNumeralParser.RomanToInt(roman));
    }

    [Theory]
    [InlineData("VV")]
    [InlineData("LL")]
    [InlineData("DD")]
    public void testInvalidDoubleAdditionOfNumbersWith5(string roman)
    {
        Assert.Throws<ArgumentException>(() => RomanNumeralParser.RomanToInt(roman));
    }

    [Theory]
    [InlineData("XXXX")]
    [InlineData("IIII")]
    public void testInvalidQuadrupleAdditionOfNumbers(string roman)
    {
        Assert.Throws<ArgumentException>(() => RomanNumeralParser.RomanToInt(roman));
    }

    [Theory]
    [InlineData("IXIX")]
    [InlineData("XCXC")]
    public void testInvalidDoubleSameSubtraction(string roman)
    {
        Assert.Throws<ArgumentException>(() => RomanNumeralParser.RomanToInt(roman));
    }

    [Theory]
    [InlineData("LXXXIX", 89)]
    [InlineData("MCMXC", 1990)]
    [InlineData("MMXXIV", 2024)]
    public void testValidComplexRomanNumerals(string roman, int expectedResult)
    {
        int actualResult = RomanNumeralParser.RomanToInt(roman);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void testEmptyStringFails()
    {
        Assert.Throws<ArgumentException>(() => RomanNumeralParser.RomanToInt(""));
    }

    [Theory(Skip = "Not implemented")]
    [InlineData(1, "I")]
    [InlineData(5, "V")]
    [InlineData(10, "X")]
    [InlineData(50, "L")]
    [InlineData(100, "C")]
    [InlineData(500, "D")]
    [InlineData(1000, "M")]
    public void testConvertsSimpleInts(int number, string expectedResult)
    {
        string actualResult = RomanNumeralParser.IntToRoman(number);

        Assert.Equal(expectedResult, actualResult);
    }


    [Theory(Skip = "Not implemented")]
    [InlineData(4, "IV")]
    [InlineData(9, "IX")]
    [InlineData(40, "XL")]
    [InlineData(90, "XC")]
    [InlineData(400, "CD")]
    [InlineData(900, "CM")]
    public void testConvertSubtractedInts(int number, string expectedResult)
    {
        string actualResult = RomanNumeralParser.IntToRoman(number);

        Assert.Equal(expectedResult, actualResult);
    }

}
