using System;
using System.IO;

namespace Copier
{
    public class FileInfoPlus
    {
        public FileInfo _fileInfo = null;

        public FileInfo GetFileInfo()
        {
            return _fileInfo;
        }

        public void SetFileInfo(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }

        private string _destinationPath;

        public string DestinationPath
        {
            get
            { 
                return _destinationPath;
            }
            set
            {
                _destinationPath = value;
            }
        }

        public string DirectoryName
        {
            get
            {
                return _fileInfo.DirectoryName;
            }
        }

        public bool Exists
        {
            get
            {
                return (_fileInfo != null);
            }
        }

        public string Extension
        {
            get
            {
                return _fileInfo.Extension;
            }
        }

        public string FullName
        {
            get
            {
                return _fileInfo.FullName;
            }
        }

        public DateTime DateTaken;

        public DateTime LastWriteTime
        {
            get
            {
                return _fileInfo.LastWriteTime;
            }
        }

        public long Length
        {
            get
            {
                return _fileInfo.Length;
            }
        }

        public string Name
        {
            get
            {
                return _fileInfo.Name;
            }
        }

        public string SourcePath
        {
            get
            {
                return _fileInfo.FullName;
            }
        }
    }
}
