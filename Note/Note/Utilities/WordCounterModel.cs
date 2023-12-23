using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Utilities
{
    public class WordCounterModel
    {
        public int CountWords(string text)
        {
            int wordCount = 0;
            bool isWord = false;

            foreach (char c in text)
            {
                if (char.IsWhiteSpace(c) || char.IsPunctuation(c))
                {
                    if (isWord)
                    {
                        wordCount++;
                        isWord = false;
                    }
                }
                else
                {
                    isWord = true;
                }
            }

            if (isWord) // Check if the last word is counted
            {
                wordCount++;
            }

            return wordCount;
        }
    }
}
