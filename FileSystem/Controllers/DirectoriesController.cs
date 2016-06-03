using FileSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FileSystem.Controllers
{
    public class DirectoriesController : ApiController
    {
        public IHttpActionResult GetDirectories(string dir = "")
        {
            Data data = new Data { Directories = new List<string>(), SizeCount = new List<int>(), Files = new List<string>() };
           
            dir = dir == "" ? Directory.GetCurrentDirectory() : dir;

            if (dir != "null")
            {
                data.Directories.Add(dir);
                data.Directories.Add(Path.GetDirectoryName(dir));

                data.Directories.AddRange(Directory.GetDirectories(dir).Select(d => Path.GetFileName(d)));

                data.Files.AddRange(Directory.GetFiles(dir).Select(f => Path.GetFileName(f)));

                DirectoryInfo di = new DirectoryInfo(dir);
                List<FileInfo> fileInfos = new List<FileInfo>();

                fileInfos.AddRange(di.GetFiles("*.*"));

                foreach (DirectoryInfo i_dir in di.EnumerateDirectories())
                {
                    try
                    {
                        fileInfos.AddRange(i_dir.GetFiles("*.*", SearchOption.AllDirectories));
                    }
                    catch { }
                }

                data.SizeCount.Add(fileInfos.Where(f => f.Length <= Math.Pow(1024, 2) * 10).Count());
                data.SizeCount.Add(fileInfos.Where(f => f.Length > Math.Pow(1024, 2) * 10 & f.Length <= Math.Pow(1024, 2) * 50).Count());
                data.SizeCount.Add(fileInfos.Where(f => f.Length >= Math.Pow(1024, 2) * 100).Count());
            }
            else
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                data.Directories.AddRange(new string[] { "drives", "" });
                data.Directories.AddRange(drives.Select(d => d.Name));
            }

            return Ok(data);
        }
    }
}
