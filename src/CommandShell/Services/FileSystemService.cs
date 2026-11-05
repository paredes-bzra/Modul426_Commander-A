namespace CommandShell.Services;

public class FileSystemService : IFileSystemService
{
    public bool FileExists(string path) => File.Exists(path);
    public bool DirectoryExists(string path) => Directory.Exists(path);
    public string ReadAllText(string path) => File.ReadAllText(path);
    public void WriteAllText(string path, string content) => File.WriteAllText(path, content);
    public void DeleteFile(string path) => File.Delete(path);
    public void CreateDirectory(string path) => Directory.CreateDirectory(path);
    public void DeleteDirectory(string path) => Directory.Delete(path, true);
    public string CurrentDirectory
    {
	    get => Directory.GetCurrentDirectory();
	    set => Directory.SetCurrentDirectory(value);
    }
    
    
	public IEnumerable<DirectoryEntry> GetEntries(string path)
	{
    	var dirs = Directory.GetDirectories(path)
        	.Select(d => new DirectoryEntry(Path.GetFileName(d), true));
    	var files = Directory.GetFiles(path)
		    .Select(f => new DirectoryEntry(Path.GetFileName(f), false, new FileInfo(f).Length));
    	return dirs.Concat(files);
	}

    public void Rename(string path, string newName)
    {
        if (string.IsNullOrWhiteSpace(path) ||
            string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("Ungültiger Name.");
        }

        if (newName == "." || newName == ".." ||
            newName.Contains('/') || newName.Contains('\\'))
        {
            throw new ArgumentException("Ungültiger neuer Name.");
        }

        string source = Path.GetFullPath(path);
        string? parent = Path.GetDirectoryName(source);

        if (parent == null)
        {
            throw new IOException("Ungültiger Quellpfad.");
        }

        string target = Path.Combine(parent, newName);

        if (File.Exists(target) || Directory.Exists(target))
        {
            throw new IOException("Das Ziel existiert bereits.");
        }

        if (File.Exists(source))
        {
            File.Move(source, target);
            return;
        }

        if (Directory.Exists(source))
        {
            Directory.Move(source, target);
            return;
        }

        throw new FileNotFoundException("Quelle nicht gefunden.");
    }
}

