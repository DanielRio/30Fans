using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace _30Fans.Web.Misc
{
    public class FileSystemService {
        public void CreateFolder(string folderPath) {
            if (!Directory.Exists(folderPath)) {
                Directory.CreateDirectory(folderPath);
            }
        }
    } // class
}