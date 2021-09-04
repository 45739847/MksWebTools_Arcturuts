using System;
using System.Collections.Generic;
using System.IO;

namespace App
{
    public class ListKeys
    {
        public string[] KeyReturns;
        public int contLines { get; set; }

        public void openList(string path)
        {
            string[] returnList;
            if (File.Exists(path))
            {
                returnList = Convert.ToString(File.ReadAllText(path)).Split('\n');
                KeyReturns = returnList;
            }
        }
    }
}