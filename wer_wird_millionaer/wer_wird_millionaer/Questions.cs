using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wer_wird_millionaer
{
    public class Questions
    {
        public string Prompt { get; set; }
        public List<string> Options { get; set; }


        public Questions()
        {

        }
        public Questions(string prompt, List<string> options)
        {
            Prompt = prompt;
            Options = options;
        }

    }
}
