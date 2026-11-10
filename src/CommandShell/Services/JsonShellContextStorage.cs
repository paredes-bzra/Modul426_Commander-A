using CommandShell.Core;
using CommandShell.FileSystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CommandShell.Services;

public class JsonShellContextStorage : IShellContextStorage
{
    private readonly string _filePath;

    private static readonly JsonSerializerSettings Settings = new()
    {
        Formatting = Formatting.Indented,
        Converters = { new StringEnumConverter() }
    };

    public JsonShellContextStorage(string filePath)
    {
        _filePath = filePath;
    }

    private class ContextDto
    {
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public NodeDto Root { get; set; } = new();
        
        public string CurrentDirectory { get; set; } = "C:\\";
    }

    private class NodeDto
    {
        public string Name { get; set; } = "";
        public bool IsDirectory { get; set; }
        public string Content { get; set; } = "";
        public DateTime LastModified { get; set; }
        public List<NodeDto> Children { get; set; } = new();
    }

    public ShellContext Load()
    {
        if (!File.Exists(_filePath))
        {
            return ShellContext.CreateDefault();
        }

        try
        {
            ContextDto? dto = JsonConvert.DeserializeObject<ContextDto>(File.ReadAllText(_filePath), Settings);
            if (dto == null || !dto.Root.IsDirectory)
            {
                throw new InvalidDataException("Ungültiger Inhalt.");
            }

            var root = new VirtualDirectory { Name = "C:", LastModified = dto.Root.LastModified };
            foreach (NodeDto child in dto.Root.Children)
            {
                AddFromDto(root, child);
            }

            return new ShellContext
            {
                CurrentDirectory = dto.CurrentDirectory,
                Root = root,
                ForegroundColor = dto.ForegroundColor,
                BackgroundColor = dto.BackgroundColor
            };
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Die Speicherdatei ist beschädigt.", ex);
        }
    }

    public void Save(ShellContext context)
    {
        var dto = new ContextDto
        {
            CurrentDirectory = context.CurrentDirectory,
            ForegroundColor = context.ForegroundColor,
            BackgroundColor = context.BackgroundColor,
            Root = ToDto(context.Root)
        };
        File.WriteAllText(_filePath, JsonConvert.SerializeObject(dto, Settings));
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
        }
        else if (item is VirtualDirectory folder)
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
