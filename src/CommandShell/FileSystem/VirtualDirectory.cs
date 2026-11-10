namespace CommandShell.FileSystem;


public class  VirtualDirectory: FileSystemItem
{
    private readonly List<FileSystemItem> children = new();
    public IReadOnlyList<FileSystemItem> Children => children;
    

    public void AddChild(FileSystemItem child)
    {
        if (GetChild(child.Name) != null)
        {
            throw new IOException("Existing child already exists");
        }
        children.Add(child);
        child.Parent = this;
    }

    public void RemoveChild(FileSystemItem child)
    {
        children.Remove(child);
        child.Parent = null;
    }

    public FileSystemItem? GetChild(string name)
    {
        
        return children.FirstOrDefault(child => string.Equals(child.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public override long GetSize()
    {
        return children.Sum(child => child.GetSize());
    }
}