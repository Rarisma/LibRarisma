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
        public static string DownloadFile(string URL, string Directory, string Filename)
        {
            try // This will download the resources.zip
            {
                using var client = new System.Net.WebClient();
                client.DownloadFileTaskAsync(URL, Directory + "//" + Filename);
            }
            catch { return "Error\nFailed to download file.\nAre you connected to the internet?"; } //This should only happen if the user cannot access github

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

    }
}
