using CommandShell.Services;
namespace CommandShell.Tests.Fakes;

public class FakeFileSystemService : IFileSystemService
{
    public string CurrentDirectory { get; set; } = @"C:\Users\Test";

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

    public void Rename(string path, string newName)
    {
        if (string.IsNullOrWhiteSpace(path) ||
            string.IsNullOrWhiteSpace(newName) ||
            newName == "." || newName == ".." ||
            newName.Contains('/') || newName.Contains('\\'))
        {
            throw new ArgumentException("Ungültiger Name.");
        }

        int separator = Math.Max(
            path.LastIndexOf('/'),
            path.LastIndexOf('\\'));

        string target = path[..(separator + 1)] + newName;

        if (Files.ContainsKey(target) ||
            Directories.Contains(target))
        {
            throw new IOException("Das Ziel existiert bereits.");
        }

        if (Files.TryGetValue(path, out var content))
        {
            Files.Remove(path);
            Files[target] = content;
            return;
        }

        if (Directories.Contains(path))
        {
            var childDirs = Directories
                .Where(d => d.StartsWith(path + "/") ||
                            d.StartsWith(path + "\\"))
                .ToList();

            var childFiles = Files
                .Where(f => f.Key.StartsWith(path + "/") ||
                            f.Key.StartsWith(path + "\\"))
                .ToList();

            Directories.Remove(path);
            Directories.Add(target);

            foreach (var directory in childDirs)
            {
                Directories.Remove(directory);
                Directories.Add(
                    target + directory[path.Length..]);
            }

            foreach (var file in childFiles)
            {
                Files.Remove(file.Key);
                Files[target + file.Key[path.Length..]] = file.Value;
            }

            return;
        }

        throw new FileNotFoundException("Quelle nicht gefunden.");
    }
}