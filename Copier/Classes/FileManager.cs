using System.IO;
using System.Xml;
using System.ComponentModel;

namespace Copier
{
    public static class FileManager
    {
        public static readonly bool DoAction = true;

        public static void LoadSettingsFile(XmlDocument xSettings)
        {
            TextReader tr = new StreamReader("settings.xml");
            xSettings.LoadXml(tr.ReadToEnd());
            tr.Close();
        }

        public static void SaveSettingsFile(XmlDocument xSettings)
        {
            TextWriter tw = new StreamWriter("settings.xml");
            tw.Write(xSettings.InnerXml);
            tw.Close();
        }

        //displayCompareFileResults doesn't do anything.
        public static FileCollection Backup(string sourcePath, string destPath, string destPathChanges, string name, BackgroundWorker worker, bool displayCompareFilesResults, string fileName = "")
        {
            FileCollection fileCollection = new FileCollection(sourcePath, destPath, fileName);
            fileCollection.Result = CompareFolders(fileCollection, name);

            if (fileCollection.Result == "Compare Complete")
            {
                if (!displayCompareFilesResults)
                    DoFileActions(fileCollection, true, destPath, destPathChanges, worker);

                fileCollection.Successful = true;
            }
            else
                worker.ReportProgress(1, fileCollection.Result);

            worker.ReportProgress(1, ""); //Just for fomatting to put an extra line between Backups

            return fileCollection;
        }

        private static void CheckandCreateBackupFolder(Watch watch)
        {
            if (!Directory.Exists(watch.Backup))
                Directory.CreateDirectory(watch.Backup);
        }

        private static string CompareFolders(FileCollection fileCollection, string name)
        {
            if (!Directory.Exists(fileCollection.sourcePath))
                return "Source Path does not exist " + fileCollection.sourcePath;

            if (!fileCollection.DestinationDriveExists())
                return "Destination Drive does not exist " + fileCollection.DestinationDrive;

            if (!Directory.Exists(fileCollection.destPath))
                return "Destination Path does not exist " + fileCollection.destPath;

            string[] sourceFiles = null, destFiles = null;

            if (fileCollection.fileName != "")
            {
                if (File.Exists(fileCollection.sourcePath + fileCollection.fileName))
                    sourceFiles = new string[1] { fileCollection.sourcePath + fileCollection.fileName };

                if (File.Exists(fileCollection.destPath + fileCollection.fileName))
                    destFiles = new string[1] { fileCollection.destPath + fileCollection.fileName };
            }
            else
            {
                sourceFiles = Directory.GetFiles(fileCollection.sourcePath, "*", SearchOption.AllDirectories);

                if (Directory.Exists(fileCollection.destPath))
                    destFiles = Directory.GetFiles(fileCollection.destPath, "*", SearchOption.AllDirectories);
            }

            fileCollection.Load(sourceFiles, destFiles, name);

            foreach (string key in fileCollection.fileContainer.Keys)
            {
                FileContainer fileContainer = fileCollection.fileContainer[key];

                if (fileContainer.FileSource.FileInfoSet && fileContainer.FileDestination.FileInfoSet)
                    CompareFiles(fileContainer);
                else if (fileContainer.FileSource.FileInfoSet && !fileContainer.FileDestination.FileInfoSet)
                    fileContainer.recommendedAction = FileAction.Copy;
                else if (!fileContainer.FileSource.FileInfoSet && fileContainer.FileDestination.FileInfoSet)
                    fileContainer.recommendedAction = FileAction.Delete;
            }

            return "Compare Complete";
        }

        private static void CompareFiles(FileContainer fileContainer)
        {
            if (fileContainer.FileSource.Length != fileContainer.FileDestination.Length)
                fileContainer.recommendedAction = FileAction.Copy;
            else if (fileContainer.FileSource.LastWriteTime != fileContainer.FileDestination.LastWriteTime)
                fileContainer.recommendedAction = FileAction.Copy;
        }

        //fileContainer.action isn't used always using recommendedAction. Maybe the start of asking the user do you want to do the action
        public static void DoFileActions(FileCollection fileCollection, bool useRecommendedAction, string destPath, string destPathChanges, BackgroundWorker worker)
        {
            bool noChanges = true;

            foreach (string key in fileCollection.fileContainer.Keys)
            {
                FileContainer fileContainer = fileCollection.fileContainer[key];

                FileAction action = (useRecommendedAction) ? fileContainer.recommendedAction : fileContainer.action;

                if (action == FileAction.Copy)
                {
                    string FileBFullPath;
                    string FileBDirectory;

                    if (fileContainer.FileDestination.FileInfoSet)
                    {
                        FileBDirectory = fileContainer.FileDestination.DirectoryName.Replace(destPath, destPathChanges);
                        FileBFullPath = fileContainer.FileDestination.FullName;
                    }
                    else
                    {
                        FileBDirectory = fileCollection.destPath + fileContainer.FileSource.DirectoryName.Replace(fileCollection.sourcePath.TrimEnd('\\'), "");
                        FileBFullPath = fileCollection.destPath + fileContainer.FileSource.FullName.Replace(fileCollection.sourcePath, "");
                    }
                        
                    if (DoAction)
                    {
                        Directory.CreateDirectory(FileBDirectory);

                        //Save Changes
                        //File.Copy(fileContainer.FileDestination.FullName, fileContainer.FileDestination.FullName.Replace(destPath, destPathChanges), true);

                        File.Copy(fileContainer.FileSource.FullName, FileBFullPath, true);
                    }

                    worker.ReportProgress(1, "Copied " + fileContainer.FileSource.FullName + " to " + fileCollection.destPath);

                    noChanges = false;
                }
                else if (action == FileAction.Delete)
                {
                    if (DoAction)
                    {
                        //For making a backup copy in case delete wasn't what was wanted
                        //Directory.CreateDirectory(fileContainer.FileDestination.DirectoryName.Replace(destPath, destPathChanges));
                        //File.Copy(fileContainer.FileDestination.FullName, fileContainer.FileDestination.FullName.Replace(destPath, destPathChanges), true);

                        File.Delete(fileContainer.FileDestination.FullName);
                    }

                    worker.ReportProgress(1, "Deleted " + fileContainer.FileDestination.FullName);

                    noChanges = false;
                }
            }

            if (noChanges)
                worker.ReportProgress(1, fileCollection.sourcePath + ": No files updated");
        }
    }

    public enum PublishType
    { 
        Backup, Revert, Normal
    }
}
