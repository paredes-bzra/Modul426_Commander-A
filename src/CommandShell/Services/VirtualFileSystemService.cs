using CommandShell.FileSystem;

namespace CommandShell.Services;

public class VirtualFileSystemService : IFileSystemService
{
    private readonly VirtualDirectory root;
    private VirtualDirectory current;
    private readonly IFileSystemStorage? storage;
    public string? LoadWarning { get; private set; }

    public VirtualFileSystemService(IFileSystemStorage? storage = null)
    {
        this.storage = storage;

        try
        {
            root = storage?.Load() ?? new VirtualDirectory { Name = "C:" };
        }
        catch(InvalidDataException)
        {
            LoadWarning = "Die Speicherdatei ist beschädigt. Dies Shell startet mit leerem C:\\";
            root = new VirtualDirectory { Name = "C:" };
        }

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
    
    private void Changed()
    {
        storage?.Save(root);
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

        FileSystemItem? item = Resolve(path);

        if (item == null)
        {
            throw new FileNotFoundException("Quelle nicht gefunden.");
        }

        VirtualDirectory? parent = item.Parent;

        if (parent == null)
        {
            throw new IOException("Das Hauptverzeichnis kann nicht umbenannt werden.");
        }

        FileSystemItem? existing = parent.GetChild(newName);

        if (existing != null && existing != item)
        {
            throw new IOException("Das Ziel existiert bereits.");
        }

        item.Name = newName;
        item.LastModified = DateTime.Now;

        Changed();
    }
}
