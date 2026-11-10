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
            newName.IndexOfAny(new[] { '/', '\\', ':', '*', '?', '"', '<', '>', '|' }) >= 0)
        {
            throw new ArgumentException("Ungültiger Name.");
        }

        string oldPath = path.TrimEnd('/', '\\');
        int separator = oldPath.LastIndexOfAny(new[] { '/', '\\' });
        string target = oldPath[..(separator + 1)] + newName;

        if (Files.Keys.Any(f => string.Equals(f, target, StringComparison.OrdinalIgnoreCase)) ||
            Directories.Any(d => string.Equals(d, target, StringComparison.OrdinalIgnoreCase)))
        {
            throw new IOException("Das Ziel existiert bereits.");
        }

        if (Files.TryGetValue(oldPath, out string? content))
        {
            Files.Remove(oldPath);
            Files[target] = content;
            return;
        }

        if (Directories.Contains(oldPath))
        {
            var folders = Directories
                .Where(d => d == oldPath ||
                            d.StartsWith(oldPath + "/") ||
                            d.StartsWith(oldPath + "\\"))
                .ToList();

            var files = Files
                .Where(f => f.Key.StartsWith(oldPath + "/") ||
                            f.Key.StartsWith(oldPath + "\\"))
                .ToList();

            foreach (string folder in folders)
            {
                Directories.Remove(folder);
            }

            foreach (var file in files)
            {
                Files.Remove(file.Key);
            }

            foreach (string folder in folders)
            {
                Directories.Add(target + folder[oldPath.Length..]);
            }

            foreach (var file in files)
            {
                Files[target + file.Key[oldPath.Length..]] = file.Value;
            }

            return;
        }

        throw new FileNotFoundException("Quelle nicht gefunden.");
    }

    public void Move(string source, string destination)
    {
        if (!Files.TryGetValue(source, out string? content))
        {
            throw new FileNotFoundException("Quelle nicht gefunden.");
        }

        string target = Directories.Contains(destination)
            ? destination.TrimEnd('/', '\\') + "\\" + Path.GetFileName(source)
            : destination;

        if (Files.ContainsKey(target))
        {
            throw new IOException("Das Ziel existiert bereits.");
        }

        Files.Remove(source);
        Files[target] = content;
    }
}