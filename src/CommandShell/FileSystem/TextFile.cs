using System.Text;
namespace CommandShell.FileSystem;

public class TextFile : FileSystemItem
{
    public string Content { get; set; }
    public TextFile(string name, string content = ""):base(name)
    {
        Content = content;
    }

    public override long GetSize() => Encoding.UTF8.GetByteCount(Content);
}