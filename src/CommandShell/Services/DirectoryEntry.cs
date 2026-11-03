namespace CommandShell.Services;

public record DirectoryEntry(
    string Name,
    bool IsDirectory,
    long Size = 0
);