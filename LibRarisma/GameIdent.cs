using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Net.NetworkInformation;
using System.Management;

namespace LibRarisma
{
    public class GameIdent
    {
        public static List<string[]> Wii(string path)
        {
            List<String> HexList = new();     //Stores the game ID list
            List<String> NameList = new();    //Stores the corresponding names
            List<String> WADNameList = new(); //Wads are dumb and use a 4 letter code instead of the 6 letter one
            List<String> DolphinList = new(); //Stores the raw list from the dolphin database
            int mode = 0;                     //Mode 0-RVZ 1-ISO 2-WBFS 3-WIA 4-WAD
            //Configures the ID and Name database
            IO.DownloadFile("https://raw.githubusercontent.com/dolphin-emu/dolphin/master/Data/Sys/wiitdb-en.txt", AppDomain.CurrentDomain.BaseDirectory, "WiiList");
            DolphinList.AddRange(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\WiiList"));
            for (int i = 1; i <= DolphinList.Count - 1; i++)
            {
                HexList.Add(DolphinList[i].Substring(0, 6));
                NameList.Add(DolphinList[i].Substring(9));
                WADNameList.Add(DolphinList[i].Substring(6));
            }

            //This scans for supported filetypes
            List<string> GameList = new();
            List<string[]> Output = new();
            GameList.AddRange(Directory.GetFiles(path, "*.wia", SearchOption.AllDirectories));
            GameList.AddRange(Directory.GetFiles(path, "*.wbfs", SearchOption.AllDirectories));
            GameList.AddRange(Directory.GetFiles(path, "*.iso", SearchOption.AllDirectories));
            GameList.AddRange(Directory.GetFiles(path, "*.rvz", SearchOption.AllDirectories));
            GameList.AddRange(Directory.GetFiles(path, "*.wad", SearchOption.AllDirectories));


            for (int i = 0; i <= GameList.Count - 1; i++)
            {//Mode 0-RVZ 1-ISO 2-WBFS 3-WIA
                if (GameList[i].Contains(".rvz")) { mode = 0; }
                else if (GameList[i].Contains(".iso"))  { mode = 1; }
                else if (GameList[i].Contains(".wbfs")) { mode = 2; }
                else if (GameList[i].Contains(".wia"))  { mode = 3; }
                else if (GameList[i].Contains(".wad"))  { mode = 4; }

                FileStream fs = new FileStream(GameList[i], FileMode.Open);
                int hexIn;
                string HexCode = "";
                int[] StartByte = { 87, 0, 512, 88, 3104 };
                int[] EndByte = { 94, 6, 518, 94, 3108 };


                for (int a = 0; a < EndByte[mode]; a++)
                {
                    hexIn = fs.ReadByte();
                    if (i > StartByte[mode]) { HexCode += string.Format("{0:X2}", hexIn) + " "; }
                }

                if (GameList[i].Contains(".wad")) { Output.Add(new string[] { path, HexCode, Utils.HexToText(HexCode), "WAD", WADNameList[HexList.IndexOf(Utils.HexToText(HexCode) + " =")] }); }
                else { Output.Add(new string[] { path, HexCode, Utils.HexToText(HexCode), new string[] { "RVZ", "ISO", "WBFS", "WIA" }[mode], NameList[HexList.IndexOf(HexCode)] }); }

            }
            return Output;
        }
    }
}
