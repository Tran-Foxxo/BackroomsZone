using GDWeave.Godot.Variants;
using GDWeave.Godot;
using GDWeave.Modding;

public class StuckPatch : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var waiter = new MultiTokenWaiter([
            t => t.Type is TokenType.Constant && t.AssociatedData == 211,
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline && t.AssociatedData == 2,
            t => t.Type is TokenType.Identifier && ((IdentifierToken)t).Name == "_kill",
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
        ], allowPartialMatch: true);

        // loop through all tokens in the script
        foreach (var token in tokens)
        {
            if (waiter.Check(token))
            {
                // found our match, return the original newline
                yield return token;

                // Add newline
                yield return new Token(TokenType.Newline, 2);

                // print("WE ARE STUCK AAAAAAAAAAAAA")
                yield return new Token(TokenType.BuiltInFunc, (uint?)BuiltinFunction.TextPrint);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("WE ARE STUCK AAAAAAAAAAAAA"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 2);

                // var modNode = get_node("/root/TranFoxBackrooms")
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("modNode");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("/root/TranFoxBackrooms"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 2);

                // modNode.MaybeGoToBackroooms()
                yield return new IdentifierToken("modNode");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("MaybeGoToBackroooms");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 2);

                // Extra lol
                yield return new Token(TokenType.Newline, 2);
            }
            else
            {
                // return the original token
                yield return token;
            }
        }
    }
}