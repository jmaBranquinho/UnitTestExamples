namespace UnitTestExamples;

public class Str2IntConverter : IStr2IntConverter
{
    public int ToInt(string value)
    {
        return int.Parse(value);
    }
    
    public int Sum(int x, int y)
    {
        return x + y;
    }
}