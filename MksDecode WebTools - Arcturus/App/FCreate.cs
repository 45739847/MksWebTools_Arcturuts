using System.IO;

namespace App
{
    public class FCreate
    {
        public static string Dir { get; set; }
        public static string Config { get; set; }
        public static string Path { get; set; }
        public string PathReturn { get; set; }


        public FCreate(string dir, string config, string path)
        {
            Dir = dir;
            Config = config;
            Path = path;
        }
        public void FolderCreate()
        {
            try
            {
                if (!Directory.Exists(Dir)) Directory.CreateDirectory(Dir);
                else if (!Directory.Exists(Config)) Directory.CreateDirectory(Config);
                else if (!File.Exists(Path)) File.Create(Path);
            } catch { }

            try { if (File.Exists(Path)) PathReturn = Path; } catch { }
        }
        public string writePath()
        {
            return this.PathReturn;
        }
    }
    public class ProxyCreateFolder
    {
        public static string Dir { get; set; }
        public static string Config { get; set; }
        public static string Path { get; set; }
        public string PathReturn { get; set; }

        public ProxyCreateFolder(string dir, string config, string path)
        {
            Dir = dir;
            Config = config;
            Path = path;
        }

        public void pFolderCreate()
        {
            try
            {
                if (!Directory.Exists(Dir)) Directory.CreateDirectory(Dir);
                else if (!Directory.Exists(Config)) Directory.CreateDirectory(Config);
                else if (!File.Exists(Path)) File.Create(Path);
            } catch { }

            try { if (File.Exists(Path)) PathReturn = Path; } catch { }
        }
        public string writePathProxy()
        {
            return this.PathReturn;
        }
    }
}