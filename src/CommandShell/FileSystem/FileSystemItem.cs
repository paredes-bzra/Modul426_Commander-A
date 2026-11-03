namespace CommandShell.FileSystem;

public abstract class FileSystemItem
{
    public string Name { get; set; }
    public VirtualDirectory? Parent { get; internal set; }
    public DateTime LastModified { get; set; } = DateTime.Now;

    protected FileSystemItem(string name)
    {
        Name = name;
    }

    public string GetFullPath()
    {
        if (Parent == null)
            return Name;
        
        return Parent.Parent == null
        ? Parent.Name + "\\" + Name
        : Parent.GetFullPath() + "\\" + Name;
    }

    public abstract long GetSize();
}