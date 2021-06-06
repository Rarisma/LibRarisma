using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRarisma
{
    class Wii
    {
        public static List<String> HexList = new();
        public static List<String> NameList = new();
        public static List<String> WADNameList = new(); //Wads are dumb and use a 4 letter code instead of the 6 letter one

        private static void Setup(string path)
        {
            List<String> DolphinList = new();
            IO.DownloadFile("https://raw.githubusercontent.com/dolphin-emu/dolphin/master/Data/Sys/wiitdb-en.txt", AppDomain.CurrentDomain.BaseDirectory, "WiiList");
            DolphinList.AddRange(File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\WiiList"));

            for (int i = 1; i <= DolphinList.Count - 1; i++)
            {
                HexList.Add(DolphinList[i].Substring(0, 6));
                NameList.Add(DolphinList[i].Substring(9));
                WADNameList.Add(DolphinList[i].Substring(6));
            }
        }

        public static List<string[]> Scan(string path)
        {
            Setup(path);

            List<string> GameList = new();
            List<string[]> Output = new();
            GameList.AddRange(Directory.GetFiles(path, "*.wia" , SearchOption.AllDirectories));
            GameList.AddRange(Directory.GetFiles(path, "*.wbfs", SearchOption.AllDirectories));
            GameList.AddRange(Directory.GetFiles(path, "*.iso" , SearchOption.AllDirectories));
            GameList.AddRange(Directory.GetFiles(path, "*.rvz" , SearchOption.AllDirectories));
            GameList.AddRange(Directory.GetFiles(path, "*.wad", SearchOption.AllDirectories));

            for (int i = 0; i <= GameList.Count - 1; i++) 
            {
                if (GameList[i].Contains(".rvz")) { Output.Add(Identify(GameList[i], 0)); } 
                else if (GameList[i].Contains(".iso")) { Output.Add(Identify(GameList[i], 1)); }
                else if (GameList[i].Contains(".wbfs")) { Output.Add(Identify(GameList[i], 2)); }
                else if (GameList[i].Contains(".wia")) { Output.Add(Identify(GameList[i], 3)); }
                else if (GameList[i].Contains(".wad")) { Output.Add(WADIdent(GameList[i])); } 
            }

            return Output;
        }


        private static string[] Identify(string path,int mode)
        { //Mode 0-RVZ 1-ISO 2-WBFS 3-WIA
            FileStream fs = new FileStream(path, FileMode.Open);
            int hexIn;
            string HexCode = "";
            int[] StartByte = { 87,0,512,88};
            int[] EndByte = { 94,6,518,94};


            for (int i = 0; i < EndByte[mode]; i++)
            {
                hexIn = fs.ReadByte();
                if (i > StartByte[mode]) { HexCode += string.Format("{0:X2}", hexIn) + " "; }
            }

            return new string[] { path, HexCode, Utils.HexToText(HexCode), new string[] { "RVZ", "ISO", "WBFS", "WIA" }[mode], NameList[HexList.IndexOf(HexCode)] };
        }


        private static string[] WADIdent(string path)
        { //Why are wads like this?
            FileStream fs = new FileStream(path, FileMode.Open);
            int hexIn;
            string HexCode = "";

            for (int i = 0; i < 3108; i++)
            {
                hexIn = fs.ReadByte();
                if (i >= 3104) { Console.Write(string.Format("{0:X2}", hexIn)); HexCode += string.Format("{0:X2}", hexIn) + " "; }
            }

            return new string[] { path, HexCode, Utils.HexToText(HexCode), "WAD", WADNameList[HexList.IndexOf(Utils.HexToText(HexCode) + " =")] };
        }
    }
}