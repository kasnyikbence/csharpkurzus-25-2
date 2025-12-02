namespace Calculator.Core;

public interface ICalculator
{
    Result<double, string> Calculate(string expression);
}
