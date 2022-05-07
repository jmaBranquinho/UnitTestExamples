using System;
using FluentAssertions;
using Moq;
using NSubstitute;
using NUnit.Framework;
using UnitTestExamples;

namespace NUnitExample;
//https://fluentassertions.com/introduction
public class CalculatorTestsNUnit
{
    private Calculator _sut;
    
    [SetUp]
    public void Setup()
    {
        _sut = new Calculator(new Str2IntConverter());
    }

    [Test]
    public void Test1()
    {
        // arrange
        var input = string.Empty;

        // act
        var result = _sut.Add(input);

        // assert
        result.message.Should().Contain("No ideas? Here's a random one from 0 to 100");
        result.result.Should().BeGreaterOrEqualTo(0);
        result.result.Should().BeLessThan(101);
    }
    
    [TestCase("1")]
    [TestCase("99")]
    [TestCase("0")]
    [TestCase("-33")]
    public void Add_WhenOnly1ValueIsProvided_ReturnsTheNumberAndRespectiveMessage(string value)
    {
        // act
        var result = _sut.Add(value);

        // assert
        result.message.Should().Contain("I can only sum 2 int numbers");
        result.result.Should().Be(int.Parse(value));
    }
    
    [TestCase(1, "1,1,1")]
    [TestCase(99, "99, 88, 77, 66")]
    [TestCase(0, "0,0, 0, 0, 0,0")]
    [TestCase(-1, "-1, 1, -1, -1, 1")]
    public void Add_When3OrMoreValuesAreProvided_ReturnsFirstNumberAndRespectiveMessage(int firstValue, string value)
    {
        // act
        var result = _sut.Add(value);

        // assert
        result.message.Should().Contain("I can only sum 2 int numbers");
        result.result.Should().Be(firstValue);
    }
    
    [TestCase(12, "11, 1")]
    [TestCase(100, "96, 4")]
    [TestCase(0, "0,0")]
    [TestCase(-1, "-10, 9")]
    public void Add_When2ValuesAreProvided_ReturnsSumAndRespectiveMessageMOQ(int sum, string values)
    {
        // arrange
        var converterMock = new Mock<IStr2IntConverter>();

        var numbers = values.Split(',', StringSplitOptions.RemoveEmptyEntries);
        converterMock.Setup(conv => conv.ToInt(It.Is<string>(x => x == numbers[0]))).Returns(int.Parse(numbers[0]));
        converterMock.Setup(conv => conv.ToInt(It.Is<string>(x => x == numbers[1]))).Returns(int.Parse(numbers[1]));
        converterMock.Setup(conv => conv.Sum(It.IsAny<int>(), It.IsAny<int>())).Returns(sum);
        
        var sut = new Calculator(converterMock.Object);

        // act
        var result = sut.Add(values);

        // assert
        result.message.Should().Contain("This is the result");
        result.result.Should().Be(sum);
    }
    
    [TestCase(12, "11, 1")]
    [TestCase(100, "96, 4")]
    [TestCase(0, "0,0")]
    [TestCase(-1, "-10, 9")]
    public void Add_When2ValuesAreProvided_ReturnsSumAndRespectiveMessageNSubstitute(int sum, string values)
    {
        //https://nsubstitute.github.io/help/getting-started/
        // arrange
        var converterMock = Substitute.For<IStr2IntConverter>();

        var numbers = values.Split(',', StringSplitOptions.RemoveEmptyEntries);
        converterMock.ToInt(It.Is<string>(x => x == numbers[0])).Returns(int.Parse(numbers[0]));
        converterMock.ToInt(It.Is<string>(x => x == numbers[1])).Returns(int.Parse(numbers[1]));
        converterMock.Sum(It.IsAny<int>(), It.IsAny<int>()).Returns(sum);
        
        var sut = new Calculator(converterMock);

        // act
        var result = sut.Add(values);

        // assert
        result.message.Should().Contain("This is the result");
        result.result.Should().Be(sum);
    }
    
    [TestCase("a, b")]
    [TestCase("a, 1")]
    [TestCase("3, b")]
    public void Add_WhenNonValuesAreProvided_ReturnsRespectiveMessage(string values)
    {
        // act
        Action act = () => _sut.Add(values);

        // assert
        act.Should().Throw<Exception>().WithMessage("That's not a number*");
    }
}