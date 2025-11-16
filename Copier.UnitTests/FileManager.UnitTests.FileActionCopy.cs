using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Copier.UnitTests
{
    public partial class FileManager
    {
        [TestMethod]
        public void FileActionCopyFileLengthDiff()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileLengthDiff\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileLengthDiff\Destination\";

            FileCollection fileCollection = TestBackup(settings);

            Assert.IsTrue(fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction == FileAction.Copy, $"Recommended Action {fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction.ToString()}");
        }

        [TestMethod]
        public void FileActionCopyFileLastWriteTimeDiff()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileLastWriteTimeDiff\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileLastWriteTimeDiff\Destination\";

            FileCollection fileCollection = TestBackup(settings);

            Assert.IsTrue(fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction == FileAction.Copy, $"Recommended Action {fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction.ToString()}");
        }

        [TestMethod]
        public void FileActionCopyFileNotInDestination()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileNotInDestination\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionCopy\FileNotInDestination\Destination\";

            FileCollection fileCollection = TestBackup(settings);

            Assert.IsTrue(fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction == FileAction.Copy, $"Recommended Action {fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction.ToString()}");
        }

        // Need to validate actually working
        [TestMethod]
        public void SingleFileActionCopyFileNotInDestination()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\SingleFileActionCopy\FileNotInDestination\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\SingleFileActionCopy\FileNotInDestination\Destination\";

            FileCollection fileCollection = TestBackup(settings, "FileActionCopyTestFile.doc");

            Assert.IsTrue(fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction == FileAction.Copy, $"Recommended Action {fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction.ToString()}");
        }

        // Need to validate actually working
        [TestMethod]
        public void SingleFileActionCopyFileLengthDiff()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\SingleFileActionCopy\FileLengthDiff\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\SingleFileActionCopy\FileLengthDiff\Destination\";

            FileCollection fileCollection = TestBackup(settings, "FileActionCopyTestFile.doc");

            Assert.IsTrue(fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction == FileAction.Copy, $"Recommended Action {fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction.ToString()}");
        }

        // Need to validate actually working
        [TestMethod]
        public void SingleFileActionCopyFileLastWriteTimeDiff()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\SingleFileActionCopy\FileLastWriteTimeDiff\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\SingleFileActionCopy\FileLastWriteTimeDiff\Destination\";

            FileCollection fileCollection = TestBackup(settings, "FileActionCopyTestFile.doc");

            Assert.IsTrue(fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction == FileAction.Copy, $"Recommended Action {fileCollection.fileContainer["FileActionCopyTestFile.doc"].recommendedAction.ToString()}");
        }
    }
}
