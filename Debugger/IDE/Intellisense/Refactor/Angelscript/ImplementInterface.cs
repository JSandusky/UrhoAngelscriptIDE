using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugger.IDE.Intellisense.Refactor.Angelscript
{
    public class ImplementInterface : RefactoringAction
    {
        string targetClassName;
        string implTypeName;
        TypeInfo implType;

        public ImplementInterface(Globals globals, String clazzTarget, String toImpl)
        {
            implTypeName = toImpl;

        }

        public String RefactoredCode(String file, TextEditor editor)
        {
            if (implType == null)
            {
                return null;
            }

            Tokenizer.StringTokenizer tokenizer = new Tokenizer.StringTokenizer(file);

            Tokenizer.Token lastToken = null;
            Tokenizer.Token token = null;
            bool foundClass = false;
            bool inClass = false;
            int braceCt = 0;
            int line = -1;
            do 
            {
                token = tokenizer.Next();
                if (lastToken != null)
                {
                    if (foundClass == false && token.Value.Equals(targetClassName) && lastToken.Value.Equals("class"))
                    {
                        foundClass = true;
                    }
                    else if (foundClass)
                    {
                        if (!inClass && token.Kind == Tokenizer.TokenKind.Symbol && token.Value == "{")
                        {
                            inClass = true;
                            ++braceCt;
                        }
                        else if (inClass)
                        {
                            if (token.Kind == Tokenizer.TokenKind.Symbol && token.Value == "{")
                            {
                                ++braceCt;
                            }
                            else if (token.Kind == Tokenizer.TokenKind.Symbol && token.Value == "{")
                            {
                                --braceCt;
                                if (braceCt == 0)
                                {
                                    line = token.Line;
                                    break;
                                }
                            }
                        }
                    }
                }
            } while (token != null);

            if (line != -1)
            {
                StringBuilder ret = new StringBuilder();


                return ret.ToString();
            }

            return null;
        }
    }
}
