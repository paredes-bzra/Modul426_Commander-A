namespace CommandShell.FileSystem;

public abstract class FileSystemItem
{
    public string Name { get; set; }
    
    public VirtualDirectory? Parent { get; internal set; }
    public DateTime LastModified { get; set; } = DateTime.Now;

    protected FileSystemItem(string name, VirtualDirectory? parent, DateTime lastModified)
    {
        Name = name;
        Parent = parent;
        LastModified = lastModified;
    }

    public FileSystemItem()
    {
        
    }

    public string GetFullPath()
    {
        return Parent == null ? Name : Parent.GetFullPath() + "\\" + Name;
    }

    public abstract long GetSize();
}