using NLog;
using System;
using System.IO;
using System.Linq;

namespace CleanFiles
{
    public class CleanFiles
    {
        private Options _options;
        static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">The options.</param>
        public CleanFiles(Options options)
        {
            _options = options;
            SearchOption = _options.Recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            ComparisonTime = DateTimeOffset.Now.AddDays(-_options.Days);
        }

        /// <summary>
        /// Perform the clean.
        /// </summary>
        public void Clean()
        {
            var path = Path.GetFullPath(_options.Path);
            if(Directory.Exists(path) == false)
            {
                _logger.Error($"Directory not found: {path}");
                return;
            }

            var directory = new DirectoryInfo(path);

            var files = directory.GetFiles(_options.FileMask, SearchOption);

            _logger.Info($"Cleaning Started - Deleting files ({_options.FileMask}) created before {ComparisonTime}");

            var count = 0;
            foreach (var file in files)
            {
                if(file.CreationTime > ComparisonTime)
                {
                    continue;
                }

                count++;

                _logger.Debug($"Creation time {file.CreationTime} - {file.FullName}");

                if (_options.Test)
                {
                    continue;
                }
                
                file.Delete();
            }
            
            _logger.Info($"Cleaning Finished - Deleted {count} of {files.Count()} files");
        }

        /// <summary>
        /// The directory search options.
        /// </summary>
        SearchOption SearchOption { get; }

        /// <summary>
        /// The time to compare file creation time against.
        /// </summary>
        DateTimeOffset ComparisonTime { get; }
    }
}
