using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Copier
{
    public class File
    {
        public FileInfo info;
        public FileAction action;

        public File(string path)
        {
            info = new FileInfo(path);
            action = FileAction.Copy;
        }
    }

    public enum FileAction
    { 
        Copy, Delete, Undefined
    }
}
