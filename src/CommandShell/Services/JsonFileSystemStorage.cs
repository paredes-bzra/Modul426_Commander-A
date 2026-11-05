using System.Text.Json;
using CommandShell.FileSystem;
namespace CommandShell.Services;

public class JsonFileSystemStorage : IFileSystemStorage
{
    private readonly string _filePath;
    
    public JsonFileSystemStorage(string filePath)
    {
        _filePath = filePath;
    }

    private class NodeDto
    {
        public string Name { get; set; }
        public bool IsDirectory { get; set; }
        public string Content { get; set; }
        public DateTime LastModified { get; set; }
        public List<NodeDto> Children { get; set; } = new();
    }

    public VirtualDirectory Load()
    {
        if (!File.Exists(_filePath))
        {
            return new VirtualDirectory { Name = "C:" };
        }

        try
        {
            NodeDto? dto = JsonSerializer.Deserialize<NodeDto>(File.ReadAllText(_filePath));
            if (dto == null || !dto.IsDirectory)
            {
                throw new InvalidCastException("Ungültiger Inhalt.");
            }

            var root = new VirtualDirectory { Name = "C:", LastModified = dto.LastModified };
            foreach (NodeDto child in dto.Children)
            {
                AddFromDto(root, child);
            }

            return root;
        }
        catch (Exception ex)
        {
            throw new InvalidCastException("Die Datei ist beschädigt", ex);
        }
    }

    public void Save(VirtualDirectory root)
    {
        string json = JsonSerializer.Serialize(
            ToDto (root),
            new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
    }

    private static NodeDto ToDto(FileSystemItem item)
    {
        var dto = new NodeDto
        {
            Name = item.Name,
            LastModified = item.LastModified,
            IsDirectory = item is VirtualDirectory,
        };

        if (item is TextFile file)
        {
            dto.Content = file.Content;
        }else if (item is VirtualDirectory folder)
        {
            dto.Children = folder.Children.Select(ToDto).ToList();
        }

        return dto;
    }

    private static void AddFromDto(VirtualDirectory parent, NodeDto dto)
    {
        if (dto.IsDirectory)
        {
            var folder = new VirtualDirectory { Name = dto.Name, LastModified = dto.LastModified };
            parent.AddChild(folder);
            foreach (NodeDto child in dto.Children)
            {
                AddFromDto(folder, child);
            }
        }
        else
        {
            parent.AddChild(new TextFile(dto.Name, parent, dto.LastModified, dto.Content));
        }
    }
}