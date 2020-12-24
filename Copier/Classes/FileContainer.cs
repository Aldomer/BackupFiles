using System.IO;

namespace Copier
{
    public class FileContainer
    {
        public FileInfo FileSource;
        public FileInfo FileDestination;

        public FileAction action;
        public FileAction recommendedAction;

        public FileContainer(FileInfo fileInfo, AssignToWhichFile assignToWhichFile)
        {
            if (assignToWhichFile == AssignToWhichFile.Source)
                FileSource = fileInfo;
            else if (assignToWhichFile == AssignToWhichFile.Destination)
                FileDestination = fileInfo;
            action = FileAction.None;
        }

        public void AddData(FileInfo fileInfo, AssignToWhichFile assignToWhichFile)
        {
            if (assignToWhichFile == AssignToWhichFile.Source)
                FileSource = fileInfo;
            else if (assignToWhichFile == AssignToWhichFile.Destination)
                FileDestination = fileInfo;
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
