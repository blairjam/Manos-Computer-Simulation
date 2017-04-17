using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS
{
    public static class Command
    {
        public static void Run(string[] args, Action<string> appendText, Action<string> appendLine)
        {
            appendLine("Command running.");
        }
    }
}
