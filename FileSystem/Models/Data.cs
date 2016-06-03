using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystem.Models
{
    public class Data
    {
        public List<string> Directories { get; set; }
        public List<string> Files { get; set; }
        public List<int> SizeCount { get; set; } 
    }
}