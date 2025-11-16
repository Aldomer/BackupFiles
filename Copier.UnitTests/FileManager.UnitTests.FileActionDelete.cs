using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Copier.UnitTests
{
    public partial class FileManager
    {
        [TestMethod]
        public void FileActionDelete()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionDelete\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\FileActionDelete\Destination\";

            FileCollection fileCollection = TestBackup(settings);

            Assert.IsTrue(fileCollection.fileContainer["FileActionDeleteTestFile.doc"].recommendedAction == FileAction.Delete, $"Recommended Action {fileCollection.fileContainer["FileActionDeleteTestFile.doc"].recommendedAction.ToString()}");
        }

        // Need to validate actually working
        [TestMethod]
        public void SingleFileActionDelete()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"C:\Projects\BackupFiles\BackupTest\Compare\SingleFileActionDelete\Source\";
            settings.Watch[0].Backup = @"C:\Projects\BackupFiles\BackupTest\Compare\SingleFileActionDelete\Destination\";

            FileCollection fileCollection = TestBackup(settings, "FileActionDeleteTestFile.doc");

            Assert.IsTrue(fileCollection.fileContainer["FileActionDeleteTestFile.doc"].recommendedAction == FileAction.Delete, $"Recommended Action {fileCollection.fileContainer["FileActionDeleteTestFile.doc"].recommendedAction.ToString()}");
        }
    }
}
