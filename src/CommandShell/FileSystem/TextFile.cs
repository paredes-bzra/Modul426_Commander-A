using System.Text;
namespace CommandShell.FileSystem;

public class TextFile : FileSystemItem
{
    public string Content { get; set; }
    public TextFile(string name,VirtualDirectory parent,DateTime lastModified, string content = ""):base(name,parent,lastModified) {
        Content = content;
    }

    public override long GetSize() => Encoding.UTF8.GetByteCount(Content);
}