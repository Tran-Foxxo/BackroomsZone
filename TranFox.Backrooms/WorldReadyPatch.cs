using GDWeave.Godot.Variants;
using GDWeave.Godot;
using GDWeave.Modding;

namespace TranFox.Backrooms
{
    internal class WorldReadyPatch : IScriptMod
    {
        public bool ShouldRun(string path) => path == "res://Scenes/World/world.gdc";

        // returns a list of tokens for the new script, with the input being the original script's tokens
        public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
        {
            var waiter = new MultiTokenWaiter([
                t => t.Type is TokenType.Identifier && t is IdentifierToken && ((IdentifierToken)t).Name == "_ready",
                t => t.Type is TokenType.ParenthesisOpen,
                t => t.Type is TokenType.ParenthesisClose,
                t => t.Type is TokenType.Colon,
                t => t.Type is TokenType.Newline,
            ], allowPartialMatch: true);

            // loop through all tokens in the script
            foreach (var token in tokens)
            {
                if (waiter.Check(token))
                {
                    // found our match, return the original newline
                    yield return token;

                    // Add newline
                    yield return new Token(TokenType.Newline, 1);

                    // var modNode = get_node("/root/TranFoxBackrooms")
                    yield return new Token(TokenType.PrVar);
                    yield return new IdentifierToken("modNode");
                    yield return new Token(TokenType.OpAssign);
                    yield return new IdentifierToken("get_node");
                    yield return new Token(TokenType.ParenthesisOpen);
                    yield return new ConstantToken(new StringVariant("/root/TranFoxBackrooms"));
                    yield return new Token(TokenType.ParenthesisClose);
                    yield return new Token(TokenType.Newline, 1);

                    // modNode._addBackroomsZone()
                    yield return new IdentifierToken("modNode");
                    yield return new Token(TokenType.Period);
                    yield return new IdentifierToken("_addBackroomsZone");
                    yield return new Token(TokenType.ParenthesisOpen);
                    yield return new Token(TokenType.ParenthesisClose);
                    yield return new Token(TokenType.Newline, 1);
                }
                else
                {
                    // return the original token
                    yield return token;
                }
            }
        }
    }
}