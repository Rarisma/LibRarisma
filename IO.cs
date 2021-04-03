using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Compression;
namespace LibRarisma
{
    public class IO
    {
        public static string DownloadFile(string URL, string Directory, string Filename)
        {
            try // This will download the resources.zip
            {
                using (var client = new System.Net.WebClient()) { client.DownloadFile(URL, Directory + "//" + Filename); }
            }
            catch { return "Error Code 1\nFailed to download file?\nAre you connected to the internet?"; } //This should only happen if the user cannot access github

            return "Success!";
        }
    }
}
