using Calculator.Core.Tokens;

namespace Calculator.Core;

public interface ITokenizer
{
    IEnumerable<IToken> Tokenize(string expression);
}
