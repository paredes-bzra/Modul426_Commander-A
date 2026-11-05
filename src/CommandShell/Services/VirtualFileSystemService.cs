using CommandShell.FileSystem;

namespace CommandShell.Services;

// Eigenes Dateisystem im Speicher. Ersetzt die echte Festplatte.
public class VirtualFileSystemService : IFileSystemService
{
    private readonly VirtualDirectory root;
    private VirtualDirectory current;

    public VirtualFileSystemService()
    {
        root = new VirtualDirectory { Name = "C:" };
        current = root;
    }

    public string CurrentDirectory
    {
        get => GetPath(current);
        set
        {
            if (Resolve(value) is not VirtualDirectory folder)
            {
                throw new DirectoryNotFoundException(value);
            }
            current = folder;
        }
    }

    public bool FileExists(string path)
    {
        return Resolve(path) is TextFile;
    }

    public bool DirectoryExists(string path)
    {
        return Resolve(path) is VirtualDirectory;
    }

    public string ReadAllText(string path)
    {
        if (Resolve(path) is not TextFile file)
        {
            throw new FileNotFoundException(path);
        }
        return file.Content;
    }

    public void WriteAllText(string path, string content)
    {
        VirtualDirectory parent = GetParentFolder(path, out string name);
        FileSystemItem? existing = parent.GetChild(name);

        if (existing is VirtualDirectory)
        {
            throw new IOException($"\"{name}\" ist ein Ordner.");
        }

        if (existing is TextFile file)
        {
            file.Content = content;
            file.LastModified = DateTime.Now;
        }
        else
        {
            parent.AddChild(new TextFile(name, parent, DateTime.Now, content));
        }
        Changed();
    }

    public void DeleteFile(string path)
    {
        if (Resolve(path) is not TextFile file || file.Parent == null)
        {
            throw new FileNotFoundException(path);
        }
        file.Parent.RemoveChild(file);
        Changed();
    }

    public void CreateDirectory(string path)
    {
        // wie Resolve, aber fehlende Ordner werden unterwegs angelegt (mkdir Projekte\2026)
        VirtualDirectory folder = StartsAtRoot(path) ? root : current;

        foreach (string part in SplitPath(path))
        {
            if (part == ".")
            {
                continue;
            }
            if (part == "..")
            {
                folder = folder.Parent ?? root;
                continue;
            }

            FileSystemItem? child = folder.GetChild(part);
            if (child == null)
            {
                var newFolder = new VirtualDirectory { Name = part };
                folder.AddChild(newFolder);
                folder = newFolder;
            }
            else if (child is VirtualDirectory existingFolder)
            {
                folder = existingFolder;
            }
            else
            {
                throw new IOException($"\"{part}\" ist eine Datei.");
            }
        }
        Changed();
    }

    public void DeleteDirectory(string path)
    {
        if (Resolve(path) is not VirtualDirectory folder)
        {
            throw new DirectoryNotFoundException(path);
        }
        if (folder.Parent == null)
        {
            throw new IOException("Das Laufwerk kann nicht gelöscht werden.");
        }

        // steht man gerade im gelöschten Ordner (oder darunter), zurück zum Parent
        if (IsInside(current, folder))
        {
            current = folder.Parent;
        }

        folder.Parent.RemoveChild(folder);
        Changed();
    }

    public IEnumerable<DirectoryEntry> GetEntries(string path)
    {
        if (Resolve(path) is not VirtualDirectory folder)
        {
            throw new DirectoryNotFoundException(path);
        }

        return folder.Children
            .Select(item => new DirectoryEntry(
                item.Name,
                item is VirtualDirectory,
                item is TextFile ? item.GetSize() : 0))
            .ToList();
    }

    // Macht aus einem Pfad (relativ, absolut, mit ..) das Item im Baum. null = gibt es nicht.
    private FileSystemItem? Resolve(string path)
    {
        FileSystemItem item = StartsAtRoot(path) ? root : current;

        foreach (string part in SplitPath(path))
        {
            if (part == ".")
            {
                continue;
            }
            if (part == "..")
            {
                item = item.Parent ?? root;
                continue;
            }
            if (item is not VirtualDirectory folder)
            {
                return null;
            }

            FileSystemItem? child = folder.GetChild(part);
            if (child == null)
            {
                return null;
            }
            item = child;
        }
        return item;
    }

    // "Daten\notiz.txt" → Ordner "Daten" und name = "notiz.txt"
    private VirtualDirectory GetParentFolder(string path, out string name)
    {
        string trimmed = path.TrimEnd('\\', '/');
        int lastSlash = trimmed.LastIndexOfAny(new[] { '\\', '/' });

        string parentPath = lastSlash < 0 ? "" : trimmed.Substring(0, lastSlash + 1);
        name = lastSlash < 0 ? trimmed : trimmed.Substring(lastSlash + 1);

        if (Resolve(parentPath) is not VirtualDirectory parent)
        {
            throw new DirectoryNotFoundException(parentPath);
        }
        return parent;
    }

    private static bool StartsAtRoot(string path)
    {
        return path.StartsWith("C:", StringComparison.OrdinalIgnoreCase);
    }

    private static string[] SplitPath(string path)
    {
        if (StartsAtRoot(path))
        {
            path = path.Substring(2);
        }
        return path.Split(new[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
    }

    // Der Root heisst "C:", als Pfad wird aber "C:\" angezeigt
    private string GetPath(VirtualDirectory folder)
    {
        return folder == root ? root.Name + "\\" : folder.GetFullPath();
    }

    private static bool IsInside(VirtualDirectory folder, VirtualDirectory possibleParent)
    {
        VirtualDirectory? check = folder;
        while (check != null)
        {
            if (check == possibleParent)
            {
                return true;
            }
            check = check.Parent;
        }
        return false;
    }

    // Wird nach jeder Änderung aufgerufen.
    // SCRUM-29: hier kommt später storage.Save(root) hin.
    private void Changed()
    {
    }
}
