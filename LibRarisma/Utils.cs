using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRarisma
{
    public static class Utils
    {
        public static string GetJavaVersion() //Winblows only (For now...)
        {
            try
            {
                ProcessStartInfo Java = new ProcessStartInfo();
                Java.FileName = "java.exe";
                Java.Arguments = " -version";
                Java.RedirectStandardError = true;
                Java.UseShellExecute = false;

                Process pr = Process.Start(Java);
                string strOutput = pr.StandardError.ReadLine().Split(' ')[2].Replace("\"", "");

                return strOutput;
            }
            catch { return "Not found"; }
        }


        /// <summary>
        /// Opens a folder, will default to documents if not found
        /// </summary>
        /// <param name="path"></param>
        public static void OpenFolder(string path) { Process.Start(new ProcessStartInfo() { FileName = path, UseShellExecute = true, Verb = "open" }); }

        /// <summary>
        /// Cleans empty elements in a list
        /// </summary>
        /// <param name="InputList"></param>
        public static void CleanList(this List<String> InputList) { InputList.RemoveAll(str => string.IsNullOrEmpty(str)); }


        /// <summary>
        /// Opens a link, eg https://github.com
        /// </summary>
        /// <param name="link"></param>
        public static void OpenLink(string link)
        {
            var LinkOpener = new ProcessStartInfo(link) { UseShellExecute = true, Verb = "open" };
            Process.Start(LinkOpener);
        }

        /// <summary>
        /// Copies a directory
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists) { throw new DirectoryNotFoundException ("Source directory does not exist or could not be found: " + sourceDirName); }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files) { file.CopyTo(Path.Combine(destDirName, file.Name), true); }
            foreach (DirectoryInfo subdir in dirs) { DirectoryCopy(subdir.FullName, Path.Combine(destDirName, subdir.Name), copySubDirs); }
        }

    }
}
