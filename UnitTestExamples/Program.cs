using UnitTestExamples;

var converter = new Str2IntConverter();
var calculator = new Calculator(converter);
Console.WriteLine("Let's add 2 numbers. Use a comma (,) to separate them. Which ones do you choose?");
var values = Console.ReadLine();

var result = calculator.Add(values);

Console.WriteLine(result.message + result.result);
Console.ReadKey();