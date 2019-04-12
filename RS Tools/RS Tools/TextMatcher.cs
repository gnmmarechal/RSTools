using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_Tools
{
    public class TextMatcher
    {
        string[] targetText = null;
        public TextMatcher(string[] targetText)
        {
            this.targetText = targetText;
        }

        public int findMatch(string text)
        {
            int similarityIndex = 0;
            foreach (string s in targetText)
            {


                if (text.Contains(s)) similarityIndex = 100;
            }

            return similarityIndex;
        }

        public Boolean certainMatch(string text)
        {
            return findMatch(text) == 100;
        }
    }
}
