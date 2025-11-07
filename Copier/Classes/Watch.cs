namespace Copier
{
    public class Watch
    {
        public string Name;
        public string Original;
        public string Backup;
        public string BackupChanges;
        public string fileName;
        public monitortype Type = monitortype.Folder;
        public bool Run; // For a future change
    }
}
