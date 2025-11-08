using System;
using System.Collections.Generic;
using System.IO;

namespace Copier
{
    public class FileCollection
    {
        public SortedDictionary<string, FileContainer> fileContainer = new SortedDictionary<string, FileContainer>();
        public string sourcePath;
        public string destPath;

        public string DestinationDrive;

        public string fileName = string.Empty;
        public bool Successful = false;

        public string Result = string.Empty;

        public FileCollection(string pi_sourcePath, string pi_destPath, string pi_fileName)
        {
            sourcePath = pi_sourcePath;
            destPath = pi_destPath;
            fileName = pi_fileName;
        }

        public void Load(string[] sourceFiles, string[] destFiles, string Name)
        {
            System.IO.FileInfo fileInfo;

            if (sourceFiles != null)
            {
                foreach (string path in sourceFiles)
                {
                    fileInfo = new System.IO.FileInfo(path);

                    if (Name == null)
                        fileContainer.Add(path.Replace(sourcePath, ""), new FileContainer(fileInfo, AssignToWhichFile.Source));
                    else
                        fileContainer.Add(Name + " " + path.Replace(sourcePath, ""), new FileContainer(fileInfo, AssignToWhichFile.Source));
                }
            }

            if (destFiles != null)
            {
                string keyPath;
                foreach (string path in destFiles)
                {
                    if (Name == null)
                        keyPath = path.Replace(destPath, "");
                    else
                        keyPath = Name + " " + path.Replace(destPath, "");

                    fileInfo = new System.IO.FileInfo(path);
                    if (fileContainer.ContainsKey(keyPath))
                        fileContainer[keyPath].AddData(fileInfo, AssignToWhichFile.Destination);
                    else
                        fileContainer.Add(keyPath, new FileContainer(fileInfo, AssignToWhichFile.Destination));
                }
            }
        }

        public void Load(string[] sourceFiles, string name)
        {
            FileInfo fileInfo;
            foreach (string path in sourceFiles)
            {
                fileInfo = new System.IO.FileInfo(path);
                fileContainer.Add(name + " " + path.Replace(sourcePath, ""), new FileContainer(fileInfo, AssignToWhichFile.Source));
            }
        }

        //Not used but leaving in case I ever need to do this
        public void Append(FileCollection fileCollectionToAppend)
        {
            foreach (string Key in fileCollectionToAppend.fileContainer.Keys)
            {
                this.fileContainer.Add(Key, fileCollectionToAppend.fileContainer[Key]);
            }
        }

        public bool DestinationDriveExists()
        {
            if (destPath.Length > 2)
            {
                string destRoot;
                if (destPath.IndexOf(':') == 1)
                    destRoot = destPath.Substring(0, 3);
                else
                {
                    int end = 0;
                    end = destPath.IndexOf("\\", 2) + 1;
                    end = destPath.IndexOf("\\", end) + 1;
                    destRoot = destPath.Substring(0, end);
                }

                DestinationDrive = destRoot;

                if (!Directory.Exists(destRoot))
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        public void GetSourceFiles(string name)
        {
            string[] sourceFiles;

            if (fileName != String.Empty)
                sourceFiles = new string[1] { sourcePath + fileName };
            else
                sourceFiles = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories);

            Load(sourceFiles, name);
        }
    }
}
