using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace LibRarisma
{
    public class Tools
    {
        /// <summary>
        /// Returns the version of java if installed or not found if the user does not have a global version of java
        /// </summary>
        /// <returns>Java Version</returns>
        public static string GetJavaVersion()
        {
            try
            {
                Process Java = new Process();
                Java.StartInfo.FileName = "java.exe";
                Java.StartInfo.Arguments = " -version";
                Java.StartInfo.RedirectStandardError = true;
                Java.StartInfo.RedirectStandardOutput = true;
                Java.StartInfo.UseShellExecute = false;

                Java.Start();
                Java.WaitForExit();

                return Java.StandardError.ReadToEnd() + " " + Java.StandardOutput.ReadToEnd();
            }
            catch { return "Not found"; }
        }


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
        /// Converts Hexadecimal string to a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HexToText(string InputString)
        {
            string[] hexValuesSplit = InputString.Split(' ');
            string final = "";
            foreach (string hex in hexValuesSplit)
            {
                try
                {
                    // Convert the number expressed in base-16 to an integer.
                    int value = Convert.ToInt32(hex, 16);
                    // Get the character corresponding to the integral value.
                    string stringValue = Char.ConvertFromUtf32(value);
                    char charValue = (char)value;
                    final += charValue;
                }
                catch { }

            }
            return final;
        }

        /// <summary>
        /// Gets the ammount of RAM on the device
        /// </summary>
        /// <param name="ReturnInMB"></param>
        /// <returns>Ammount of RAM in MB</returns>
        public static Int64 GetRAM()
        {
            var client = new MemoryMetricsClient();
            var metrics = client.GetMetrics();
            return Convert.ToInt64(metrics.Total);
        }


        /// <summary>
        /// Sanitises a given string
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string SanitizeString(string Input)
        {
            char[] DisallowedSymbols = { '|', ',', '/', ':', '*', '|', '?', '<', '>', Convert.ToChar("\\") };
            string Output = Input;
            for (int i = 0; i <= DisallowedSymbols.Length; i++) { Output = Output.Replace(DisallowedSymbols[i], ' '); i++; }
            return Output;
        }


        public static string MD5Hash(string fileName, int RomID)
        {
            try
            {
                bool zip = false;
                if (fileName.Contains(".zip") || fileName.Contains(".7z") || fileName.Contains(".rar"))
                {
                    zip = true;
                    ZipFile.ExtractToDirectory(fileName, AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\" + RomID + "\\");
                    string[] Files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\" + RomID + "\\", "*", SearchOption.AllDirectories);
                    fileName = Files[0];
                }

                FileStream file = new(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new();
                for (int i = 0; i < retVal.Length; i++) { sb.Append(retVal[i].ToString("x2")); }

                if (zip == true) { Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\" + RomID + "\\", true); }

                return sb.ToString();
            }
            catch //Just incase read access is denied or file decides to cease to exist 
            {
                return "ERROR";
            }

        }

    }
}
