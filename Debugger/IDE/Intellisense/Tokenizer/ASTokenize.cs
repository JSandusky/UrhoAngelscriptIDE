using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugger.IDE.Intellisense.Tokenizer
{
    public class ASTokenize
    {
        static readonly string[] mathAssignmentTokens = { "+", "-", "*", "/" };

        public static bool IsLineAssignment(string lineText, out string estimatedType, out string assigneeName)
        {
            estimatedType = "";
            assigneeName = "";
            List<Token> tokens = new List<Token>();
            StringTokenizer tokenizer = new StringTokenizer(lineText);
            
            Token token = tokenizer.Next();
            while (token != null)
            {
                tokens.Add(token);
                token = tokenizer.Next();
            }

            // need at least two tokens
            if (tokens.Count > 1)
            {
                // Is last token an equals
                if (tokens[tokens.Count - 1].Value == "=" && tokens[tokens.Count - 1].Kind == TokenKind.Symbol)
                {
                    // Math style token
                    if (tokens[tokens.Count - 2].Kind == TokenKind.Symbol && mathAssignmentTokens.Contains(tokens[tokens.Count - 2].Value))
                    {
                        if (tokens.Count > 3)
                        {

                        }
                    }
                    else // must be simple assignment
                    {
                        assigneeName = tokens[tokens.Count - 2].Value;
                        if (tokens.Count > 2)
                        {
                            if (tokens[tokens.Count - 3].Kind == TokenKind.Word)
                                estimatedType = tokens[tokens.Count - 3].Value;
                        }
                    }
                }
            }
            return false;
        }
    }
}
