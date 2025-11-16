using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Copier.UnitTests
{
    public partial class FileManager
    {
        [TestMethod]
        public void SourcePathDoesNotExist()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Original = @"c:\IDoNotExist\";

            FileCollection fileCollection = TestBackup(settings);

            if (fileCollection.Result.StartsWith("Source Path does not exist"))
                Assert.IsTrue(true);
            else
                Assert.IsFalse(true, fileCollection.Result);
        }

        [TestMethod]
        public void DesinationRootDoesNotExist()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Backup = @"x:\IDoNotExist\";

            FileCollection fileCollection = TestBackup(settings);

            if (fileCollection.Result.StartsWith("Destination Drive does not exist"))
                Assert.IsTrue(true);
            else
                Assert.IsFalse(true, fileCollection.Result);
        }

        [TestMethod]
        public void DestinationPathDoesNotExist()
        {
            Settings settings = GetTestSettings();

            settings.Watch[0].Backup = @"c:\IDoNotExist\";

            FileCollection fileCollection = TestBackup(settings);

            if (fileCollection.Result.StartsWith("Destination Path does not exist"))
                Assert.IsTrue(true);
            else
                Assert.IsTrue(true, fileCollection.Result);
        }
    }
}
