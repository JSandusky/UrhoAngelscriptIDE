using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearch
{
    public static class SearchUtil
    {
        public static string MakeBold(this string source, string oldString)
        {
            int index = source.IndexOf(oldString, StringComparison.InvariantCultureIgnoreCase);
            while (index >= 0)
            {
                // Determine if we found a match
                bool MatchFound = index >= 0;

                if (MatchFound)
                {
                    // Remove the old text
                    string oldTerm = source.Substring(index, oldString.Length);
                    source = source.Remove(index, oldString.Length);

                    // Add the replacemenet text
                    source = source.Insert(index, "[b]" + oldTerm + "[/b]");
                }
                
                index = source.IndexOf(oldString, index + oldString.Length, StringComparison.InvariantCultureIgnoreCase);
            }

            return source;
        }
    }
}
