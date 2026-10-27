using CommandShell.Services;
namespace CommandShell.Tests.Fakes;

public class FakeFileSystemService : IFileSystemService
{
  	public Dictionary<string, string> Files {get;} = new();
	public HashSet<string> Directories {get;} = new();

	public bool FileExists(string path) => Files.ContainsKey(path);
	public bool DirectoryExists(string path) =>  Directories.Contains(path);

	public string ReadAllText(string path) =>
        Files.TryGetValue(path, out var c) ? c : throw new FileNotFoundException(path);

    public void WriteAllText(string path, string content) => Files[path] = content;
    public void DeleteFile(string path) => Files.Remove(path);
    public void CreateDirectory(string path) => Directories.Add(path);
    public void DeleteDirectory(string path) => Directories.Remove(path);

    public IEnumerable<DirectoryEntry> GetEntries(string path)
    {
        var prefix = path.TrimEnd('/', '\\') + "/";
        var dirs = Directories
            .Where(d => d.StartsWith(prefix) && !d[prefix.Length..].Contains('/'))
            .Select(d => new DirectoryEntry(d[prefix.Length..], true));
        var files = Files.Keys
            .Where(f => f.StartsWith(prefix) && !f[prefix.Length..].Contains('/'))
            .Select(f => new DirectoryEntry(f[prefix.Length..], false));
        return dirs.Concat(files);
    }
}