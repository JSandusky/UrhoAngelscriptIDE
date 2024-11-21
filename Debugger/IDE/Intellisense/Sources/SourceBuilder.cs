using Debugger.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugger.IDE.Intellisense.Sources
{
    /// <summary>
    /// \todo: consider switching to interogating the assembly for IntellisenseSources and using an annotation
    /// </summary>
    public static class SourceBuilder
    {
        public static bool HandlesExtension(string ext)
        {
            if (ext.ToLowerInvariant().Equals(".as"))
                return true;
            else if (ext.ToLowerInvariant().Equals(".glsl"))
                return true;
            else if (ext.ToLowerInvariant().Equals(".hlsl"))
                return true;
            else if (ext.ToLowerInvariant().Equals(".txt"))
                return true;
            return false;
        }

        public static bool HandlesExtensionAsFallback(string ext)
        {
            if (ext.ToLowerInvariant().Equals(".xml"))
                return true;
            return false;
        }

        public static IntellisenseSource GetSourceForExtension(string ext)
        {
            if (ext.ToLowerInvariant().Equals(".as"))
                return new AngelscriptSource();
            else if (ext.ToLowerInvariant().Equals(".glsl"))
                return new GLSLSource();
            else if (ext.ToLowerInvariant().Equals(".hlsl"))
                return new HLSLSource();
            else if (ext.ToLowerInvariant().Equals(".txt"))
                return new NullSource();
            else if (ext.ToLowerInvariant().Equals(".lua"))
                return new LuaSource();
            else if (ext.ToLowerInvariant().Equals(".xml"))
                return new XmlSource();
            return null;
        }


        static readonly string[] BRACE_FOLDED_EXTENSIONS = { ".java", ".as", ".cpp", ".c", ".h", ".cs" };
        public static FoldingStrategy GetFoldingStrategyForExtension(string ext) 
        {
            if (BRACE_FOLDED_EXTENSIONS.Contains(ext.ToLowerInvariant()))
                return new BraceFoldingStrategy();
            else if (ext.ToLowerInvariant().Equals(".xml"))
                return new XMLFoldingStrategy();
            return null;
        }
    }
}
