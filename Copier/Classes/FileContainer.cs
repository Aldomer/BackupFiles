using System.IO;

namespace Copier
{
    public class FileContainer
    {
        public FileInfoPlus FileSource = new FileInfoPlus();
        public FileInfoPlus FileDestination = new FileInfoPlus();

        public FileAction action;
        public FileAction recommendedAction;

        public FileContainer(FileInfo fileInfo, AssignToWhichFile assignToWhichFile)
        {
            if (assignToWhichFile == AssignToWhichFile.Source)
                FileSource.SetFileInfo(fileInfo);
            else if (assignToWhichFile == AssignToWhichFile.Destination)
                FileDestination.SetFileInfo(fileInfo);
            action = FileAction.None;
        }

        public void AddData(FileInfo fileInfo, AssignToWhichFile assignToWhichFile)
        {
            if (assignToWhichFile == AssignToWhichFile.Source)
                FileSource.SetFileInfo(fileInfo);
            else if (assignToWhichFile == AssignToWhichFile.Destination)
                FileDestination.SetFileInfo(fileInfo);
        }
    }

    public enum AssignToWhichFile
    {
        Source, Destination
    }

    public enum FileAction
    {
        None, Copy, Delete
    }
}
