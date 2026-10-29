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
        	.Select(f => new DirectoryEntry(Path.GetFileName(f), false));
    	return dirs.Concat(files);
	}
}

