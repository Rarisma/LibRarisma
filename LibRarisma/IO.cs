using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Net.NetworkInformation;
using System.Management;
//Updated to 1.3 for SSM
namespace LibRarisma
{
    public class IO //IO handles functions that are releated to file operations or hardware, if this becomes too large it will split to Hardware for the hardware functions
    {
        public static void RecreateDirectory(string DirectoryPath)
        {
            System.IO.Directory.CreateDirectory(DirectoryPath);
            System.IO.Directory.Delete(DirectoryPath, true);
            System.IO.Directory.CreateDirectory(DirectoryPath);
        }


        public static string DownloadFile(string URL, string Directory, string Filename, bool Extract = false)
        {
            try // This will download the resources.zip
            {
                System.IO.Directory.CreateDirectory(Directory);
                using var client = new System.Net.WebClient();
                client.DownloadFile(URL, Directory + "//" + Filename);
            }
            catch { return "Error\nFailed to download file.\nAre you connected to the internet?"; } //This should only happen if the user cannot access github

            if (Extract == true) { System.IO.Compression.ZipFile.ExtractToDirectory(Directory + "//" + Filename, Directory); }


            return "Success!";
        }

        public static string AsyncDownloadFile(string URL, string Directory, string Filename, bool Extract = false)
        {
            try // This will download the resources.zip
            {
                System.IO.Directory.CreateDirectory(Directory);
                using var client = new System.Net.WebClient();
                client.DownloadFileTaskAsync(URL, Directory + "//" + Filename);
            }
            catch { return "Error\nFailed to download file.\nAre you connected to the internet?"; } //This should only happen if the user cannot access github

            if (Extract == true) { System.IO.Compression.ZipFile.ExtractToDirectory(Directory + "//" + Filename, Directory); }

            return "Success!";
        }


        //Defealts to microsoft, if by chance it falls and doesnt exist and LibRarisma is still maintained then I will update it to somthing like DuckDuckGo
        public static bool Connectivity_Check(string WebsiteToCheck = "www.Microsoft.com")
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) //This checks if the conputer has a wifi connection
            {
                Ping PingSender = new();
                PingReply PingStatus = PingSender.Send(WebsiteToCheck, 1000);

                if (PingStatus.Status == IPStatus.Success) { return true; } //if it gets a ping then user has a working net connection
                else { return false; } //Either Microsoft is down or user has VERY/No internet
            }
            else { return false; } //Returns false if there is no network
        }


        /// <summary>
        /// Gets the ammount of RAM on the device
        /// May not return the full amount of RAM but should get a close number
        /// Do not run this on crossplatform applications.
        /// </summary>
        /// <param name="ReturnInMB"></param>
        /// <returns>Ammount of RAM in MB if true otherwise returns it in GB, rounded down to the nearist GB</returns>
        public static Int64 GetRAM(bool ReturnInMB = true)
        {
            Int64 RamInMB = 0;
            
            ObjectQuery wql = new("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new(wql);
            ManagementObjectCollection results = searcher.Get();
        
            foreach (ManagementObject result in results) { RamInMB = Convert.ToInt64(result["TotalVisibleMemorySize"]) / 1024; }

            if (ReturnInMB) { return RamInMB ; }
            else { return Convert.ToInt64(RamInMB / 1024); }
        }


        public static List<string> ReadINIFile(string PathToFile)
        {
            List<string> INI_File = new();
            INI_File.AddRange(System.IO.File.ReadAllLines(PathToFile));
            INI_File.CleanList();

            for (int i = 0; i <= INI_File.Count; i++)
            {
                if (INI_File[i][0] == Convert.ToChar("#"))
                {
                    INI_File.RemoveAt(i);
                    i = 0; //Resets I to prevent skipping a line
                }
            }

            return INI_File;
        }

        public static string SanitizeString(string Input)
        {
            char[] DisallowedSymbols = { '|', ',' , '/' , ':', '*', '|', '?', '<', '>', Convert.ToChar("\\") };
            string Output = Input;
            for(int i =0; i <= DisallowedSymbols.Length; i++) { Output = Output.Replace(DisallowedSymbols[i], ' '); i++; }
            return Output;
        }


    }
}
