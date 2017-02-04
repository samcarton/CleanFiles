using CommandLine;

namespace CleanFiles
{
    public class Options
    {
        [Option('p', "path", Required = true, HelpText = "The path to the files to clean up.")]
        public string Path { get; set; }

        [Option('d', "days", DefaultValue = 14, HelpText = "The number of days to retain files for.")]
        public int Days { get; set; }

        [Option('r', "recurse", DefaultValue = false, HelpText = "Whether to recursively search directories.")]
        public bool Recurse { get; set; }
        
        [Option('f', "filemask", HelpText = "The file mask to use when searching for files.", DefaultValue = "*.*")]
        public string FileMask { get; set; }

        [Option('t', "test", HelpText = "Just output files that match the search - does not delete them.", DefaultValue = false)]
        public bool Test { get; set; }
    }
}
