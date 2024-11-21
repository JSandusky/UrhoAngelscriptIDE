using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugger.Editor
{
    public abstract class FoldingStrategy
    {
        public abstract void UpdateFoldings(FoldingManager manager, TextDocument document);
    }
}
