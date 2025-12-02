using Calculator.Core.Tokens;

namespace Calculator.Core;

internal class Calculator(ITokenizer tokenizer, INumberStack numberStack) : ICalculator
{
    public Result<double, string> Calculate(string expression)
    {
        try
        {
            foreach (IToken token in tokenizer.Tokenize(expression))
            {
                token.Apply(numberStack);
            }
            return new Result<double, string>(numberStack.Pop());
        }
        catch (InvalidOperationException ex)
        {
            return new Result<double, string>(ex.Message);
        }
    }
}