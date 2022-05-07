namespace UnitTestExamples;

public class Calculator
{
    private readonly IStr2IntConverter _converter;

    public Calculator(IStr2IntConverter converter)
    {
        _converter = converter;
    }
    
    public (string message, int result) Add(string numbers)
    {
        try
        {
            var splitNumbers = numbers.Split(',', StringSplitOptions.RemoveEmptyEntries);

            if (!splitNumbers.Any())
            {
                return ("No ideas? Here's a random one from 0 to 100: ", new Random().Next(0, 101));
            }

            if (splitNumbers.Length != 2)
            {
                return ("I can only sum 2 int numbers. Which one to combine with: ", _converter.ToInt(splitNumbers[0]));
            }

            return ("This is the result: ", _converter.Sum(_converter.ToInt(splitNumbers[0]), _converter.ToInt(splitNumbers[1])));
        }
        catch (Exception e)
        {
            throw new Exception("That's not a number I'm afraid...");
        }
    }
}