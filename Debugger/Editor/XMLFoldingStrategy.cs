using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugger.Editor
{
    public class XMLFoldingStrategy : FoldingStrategy
    {
        XmlFoldingStrategy xmlFolding_;

        public XMLFoldingStrategy()
        {
            xmlFolding_ = new XmlFoldingStrategy();
        }



        public override void UpdateFoldings(FoldingManager manager, TextDocument document)
        {
            xmlFolding_.UpdateFoldings(manager, document);
        }
    }
}
