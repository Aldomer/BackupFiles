public class Monitor
{
    public monitortype Type;
    public string Path;

    public Monitor(monitortype passedinType, string passedinPath)
    {
        Type = passedinType;
        Path = passedinPath;
    }
}

public enum monitortype
{
    Folder, File
}
