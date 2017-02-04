using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if(CommandLine.Parser.Default.ParseArgumentsStrict(args,options)  == false)
            {
                return;
            }

            var cleanFiles = new CleanFiles(options);
            cleanFiles.Clean();
        }
    }
}
