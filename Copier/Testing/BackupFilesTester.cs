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

            SourcePathDoesNotExist();
            DesinationRootDoesNotExist();

            //Need to do all this but for a single file
            FileActionCopyFileLengthDiff();
            FileActionCopyFileLastWriteTimeDiff();
            FileActionCopyFileNotInDestination();
            FileActionDelete();

            SingleFileActionCopyFileLengthDiff();
            SingleFileActionCopyFileLastWriteTimeDiff();
            SingleFileActionCopyFileNotInDestination();
            SingleFileActionDelete();

            return _testResults;
        }

        private void SourcePathDoesNotExist()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"c:\IDoNotExist\";

            FileCollection fileCollection = TestBackup(settings);

            if (fileCollection.Result.StartsWith("Source Path does not exist"))
                _testResults.Add("Pass: SourcePathDoesNotExist");
            else
                _testResults.Add("Fail: SourcePathDoesNotExist");
        }

        private void DesinationRootDoesNotExist()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Backup = @"x:\IDoNotExist\";

            FileCollection fileCollection = TestBackup(settings);

            if (fileCollection.Result.StartsWith("Destination Drive does not exist"))
                _testResults.Add("Pass: DesinationRootDoesNotExist");
            else
                _testResults.Add("Fail: DesinationRootDoesNotExist");
        }

        private void FileActionCopyFileNotInDestination()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileNotInDestination\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileNotInDestination\Destination\";

            FileCollection fileCollection = TestBackup(settings);

            if (fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction == FileAction.Copy)
                _testResults.Add("Pass: FileActionCopy");
            else
                _testResults.Add("Fail: FileActionCopy");
        }

        private void FileActionCopyFileLengthDiff()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileLengthDiff\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileLengthDiff\Destination\";

            FileCollection fileCollection = TestBackup(settings);

            if (fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction == FileAction.Copy)
                _testResults.Add("Pass: FileActionCopyFileLengthDiff");
            else
                _testResults.Add("Fail: FileActionCopyFileLengthDiff");
        }

        private void FileActionCopyFileLastWriteTimeDiff()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileLastWriteTimeDiff\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileLastWriteTimeDiff\Destination\";

            FileCollection fileCollection = TestBackup(settings);

            if (fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction == FileAction.Copy)
                _testResults.Add("Pass: FileActionCopyFileLastWriteTimeDiff");
            else
                _testResults.Add("Fail: FileActionCopyFileLastWriteTimeDiff");
        }

        private void FileActionDelete()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionDelete\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionDelete\Destination\";

            FileCollection fileCollection = TestBackup(settings);

            if (fileCollection.fileContainer["FileActionDeleteTestFile.doc"].recommendedAction == FileAction.Delete)
                _testResults.Add("Pass: FileActionDelete");
            else
                _testResults.Add("Fail: FileActionDelete");
        }

        private void SingleFileActionCopyFileNotInDestination()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileNotInDestination\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileNotInDestination\Destination\";

            FileCollection fileCollection = TestBackup(settings, "FileActionCopyTestFile.doc");

            if (fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction == FileAction.Copy)
                _testResults.Add("Pass: SingleFileActionCopyFileNotInDestination");
            else
                _testResults.Add("Fail: SingleFileActionCopyFileNotInDestination");
        }

        private void SingleFileActionCopyFileLengthDiff()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileLengthDiff\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileLengthDiff\Destination\";

            FileCollection fileCollection = TestBackup(settings, "FileActionCopyTestFile.doc");

            if (fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction == FileAction.Copy)
                _testResults.Add("Pass: SingleFileActionCopyFileLengthDiff");
            else
                _testResults.Add("Fail: SingleFileActionCopyFileLengthDiff");
        }

        private void SingleFileActionCopyFileLastWriteTimeDiff()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileLastWriteTimeDiff\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileLastWriteTimeDiff\Destination\";

            FileCollection fileCollection = TestBackup(settings, "FileActionCopyTestFile.doc");

            if (fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction == FileAction.Copy)
                _testResults.Add("Pass: SingleFileActionCopyFileLastWriteTimeDiff");
            else
                _testResults.Add("Fail: SingleFileActionCopyFileLastWriteTimeDiff");
        }

        //Still need to do the following three
        private void SingleFileActionDelete()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionDelete\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionDelete\Destination\";

            FileCollection fileCollection = TestBackup(settings, "FileActionDeleteTestFile.doc");

            if (fileCollection.fileContainer["FileActionDeleteTestFile.doc"].recommendedAction == FileAction.Delete)
                _testResults.Add("Pass: SingleFileActionDelete");
            else
                _testResults.Add("Fail: SingleFileActionDelete");
        }

        private FileCollection TestBackup(Settings settings, string fileName = null)
        {
            FileCollection fileCollection = null;

            foreach (Copier.Watch foldersToBackup in settings.Watch)
            {
                if (fileName == null)
                    fileCollection = Copier.FileManager.Backup(foldersToBackup.Original, foldersToBackup.Backup, foldersToBackup.BackupChanges, foldersToBackup.Name, _backgroundWorker, false);
                else
                    fileCollection = Copier.FileManager.Backup(foldersToBackup.Original, foldersToBackup.Backup, foldersToBackup.BackupChanges, foldersToBackup.Name, _backgroundWorker, false, fileName);
            }

            return fileCollection;
        }

        private Settings GetTestSettings()
        {
            Settings settings = new Settings();

            Watch watch = new Watch();

            watch.Backup = @"c:\Projects\BackupFiles\BackupTest\Folder2\";
            watch.BackupChanges = @"c:\Projects\BackupFiles\BackupTest\Folder2Changes\";
            watch.Original = @"c:\Projects\BackupFiles\BackupTest\Folder1\";

            settings.Watch.Add(watch);

            return settings;
        }
    }
}
