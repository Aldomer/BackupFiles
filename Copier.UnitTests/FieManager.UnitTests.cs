using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Copier.UnitTests
{
    [TestClass]
    public partial class FileManager
    {
        private FileCollection TestBackup(Settings settings, string fileName = null)
        {
            FileCollection fileCollection = null;

            foreach (Copier.Watch foldersToBackup in settings.Watch)
            {
                if (fileName == null)
                    fileCollection = Copier.FileManager.Backup(foldersToBackup.Original, foldersToBackup.Backup, foldersToBackup.BackupChanges, foldersToBackup.Name, string.Empty, false);
                else
                    fileCollection = Copier.FileManager.Backup(foldersToBackup.Original, foldersToBackup.Backup, foldersToBackup.BackupChanges, foldersToBackup.Name, fileName, false);
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
