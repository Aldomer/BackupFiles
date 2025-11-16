using System.Collections.Generic;
using System.ComponentModel;

namespace Copier.Testing
{
    public class BackupFilesTester
    {
        private BackgroundWorker _backgroundWorker;

        private List<string> _testResults = new List<string>();

        public List<string> Run(BackgroundWorker backgroundWorker)
        {
            _backgroundWorker = backgroundWorker;

            //SingleFileActionCopyFileLengthDiff();
            //SingleFileActionCopyFileLastWriteTimeDiff();
            //SingleFileActionCopyFileNotInDestination();
            //SingleFileActionDelete();

            return _testResults;
        }

        //Still need to do the following three
        

        private FileCollection TestBackup(Settings settings, string fileName = null)
        {
            FileCollection fileCollection = null;

            foreach (Copier.Watch foldersToBackup in settings.Watch)
            {
                if (fileName == null)
                    fileCollection = Copier.FileManager.Backup(foldersToBackup.Original, foldersToBackup.Backup, foldersToBackup.BackupChanges, foldersToBackup.Name, string.Empty, false, _backgroundWorker);
                else
                    fileCollection = Copier.FileManager.Backup(foldersToBackup.Original, foldersToBackup.Backup, foldersToBackup.BackupChanges, foldersToBackup.Name, fileName, false, _backgroundWorker);
            }

            return fileCollection;
        }

        private Settings GetTestSettings()
        {
            Settings settings = new Settings();

            Watch watch = new Watch();

            watch.Backup = @"c:\Projects\BackupFiles\BackupTest\Folder2\";
            watch.BackupChanges = @"c:\Projects\BackupFiles\BackupTest\Compare\Changes\";
            watch.Original = @"c:\Projects\BackupFiles\BackupTest\Folder1\";

            settings.Watch.Add(watch);

            return settings;
        }
    }
}
