using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wer_wird_millionaer
{
    /// <summary>
    /// Objekt zur Bündelung von Fragen und deren Antwortmöglichkeiten
    /// </summary>
    public class Questions
    {
        /// <summary>
        /// Fragen Text
        /// </summary>
        public string Prompt { get; set; }
        /// <summary>
        /// Antworten Liste
        /// </summary>
        public List<string> Options { get; set; }

        /// <summary>
        /// Konstruktor mit allen Parametern zur Bündelung von Fragen und deren Antwortmöglichkeiten
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="options"></param>
        public Questions(string prompt, List<string> options)
        {
            Prompt = prompt;
            Options = options;
        }

    }
}
