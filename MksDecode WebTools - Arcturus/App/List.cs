using System.Collections.Generic;

namespace App
{
    public class List
    {
        public List<string> listS = new List<string>();

        public void addToList(string[] content)
        {
            foreach (var i in content)
                listS.Add(i);
        }
    }
}