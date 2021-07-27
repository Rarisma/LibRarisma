using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LibRarisma
{
    class Connectivity
    {
        public static string DownloadFile(string URL, string Directory, string Filename, bool Extract = false)
        {
            try // This will download the resources.zip
            {
                System.IO.Directory.CreateDirectory(Directory);
                using var client = new System.Net.WebClient();
                client.DownloadFile(URL, Directory + "//" + Filename);
            }
            catch { return "Error\nFailed to download file.\nAre you connected to the internet?"; } //This should only happen if the user cannot access github

            if (Extract == true) { ZipFile.ExtractToDirectory(Directory + "//" + Filename, Directory); }


            return "Success!";
        }


        public static bool ConnectionCheck(string WebsiteToCheck = "www.Microsoft.com")
        {
            if (NetworkInterface.GetIsNetworkAvailable()) //This checks if the conputer has a wifi connection
            {
                Ping PingSender = new();
                PingReply PingStatus = PingSender.Send(WebsiteToCheck, 1000);

                if (PingStatus.Status == IPStatus.Success) { return true; } //if it gets a ping then user has a working net connection
                else { return false; } //Either Microsoft is down or user has VERY/No internet
            }
            else { return false; } //Returns false if there is no network
        }
    }
}
