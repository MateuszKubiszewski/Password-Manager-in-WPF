using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Manager
{
    public class File
    {
        public string Name { get; set; }
        public bool isbold = true;
        public bool isdir = false;
        public bool rightclick = false;
    }

    public class Directory : File
    {
        static int i = 0;
        public List<File> files = new List<File>();
        public Directory()
        {
            isdir = true;
            Name = "Directory" + i;
            i++;
        }

        public void add(File f)
        {
            files.Add(f);
        }
    }

    public class Image : File
    {
        static int i = 0;
        public Image()
        {
            isbold = false;
            Name = "Image" + i;
            i++;
        }
    }
}
