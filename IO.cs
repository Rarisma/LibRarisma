using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRarisma
{
    class IO
    {
        /// <summary>
        /// Opens a directory, will default to documents if not found
        /// </summary>
        /// <param name="path"></param>
        public static void OpenDirectory(string path)
        {
            try
            {
                Process.Start(new ProcessStartInfo() { FileName = path, UseShellExecute = true, Verb = "open" });
            }
            catch { }
        }

        /// <summary>
        /// Copies a directory, and subdirectories if Recursive is set to true
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Destination"></param>
        /// <param name="Recursive"></param>
        public static void CopyDirectory(string Source, string Destination, bool Recursive = true)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(Source);

            if (!dir.Exists) { throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + Source); }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(Destination);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files) { file.CopyTo(Path.Combine(Destination, file.Name), true); }
            foreach (DirectoryInfo subdir in dirs) { CopyDirectory(subdir.FullName, Path.Combine(Destination, subdir.Name), Recursive); }
        }

        /// <summary>
        /// Deletes a directory and all its contents and then remakes it a blank directory
        /// </summary>
        /// <param name="DirectoryPath"></param>
        public static void RecreateDirectory(string DirectoryPath)
        {
            Directory.CreateDirectory(DirectoryPath);
            Directory.Delete(DirectoryPath, true);
            Directory.CreateDirectory(DirectoryPath);
        }

    }
}
