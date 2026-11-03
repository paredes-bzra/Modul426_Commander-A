namespace CommandShell.FileSystem;

public abstract class FileSystemItem
{
    public string Name { get; set; }
    public VirtualDirectory? Parent { get; internal set; }
    public DateTime LastModified { get; set; } = DateTime.Now;
    
    

}