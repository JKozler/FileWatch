using System;
using System.IO;
using System.Security.Permissions;

namespace FileWatch
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }
        private static void Run()
        {
            string[] args = Environment.GetCommandLineArgs();
            args[0] = "Z:\\";
            using (FileSystemWatcher fsw = new FileSystemWatcher())
            {
                fsw.Path = args[0];
                fsw.NotifyFilter = NotifyFilters.LastAccess
                                | NotifyFilters.LastWrite
                                | NotifyFilters.FileName
                                | NotifyFilters.DirectoryName;

                fsw.Renamed += OnRenamed;
                fsw.Created += OnChanged;
                fsw.Deleted += OnChanged;
                fsw.Changed += OnChanged;

                fsw.EnableRaisingEvents = true;

                Console.WriteLine("Press 'q' and ENTER to quit.");
                while (Console.Read() != 'q') ;
            }
        }
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File " + e.FullPath + " " + e.ChangeType);
        }
        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            Console.WriteLine("File was renamed form " + e.OldFullPath + " to " + e.FullPath);
        }
    }
}
