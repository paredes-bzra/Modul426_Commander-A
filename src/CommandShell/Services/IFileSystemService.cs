namespace CommandShell.Services;

public class IFilesSystemService : IFileSystemService
{
    bool FileExists(string path);
    bool DirectoryExists(string path);
    string ReadAllText(string path);
    void WriteAllText(string path, string content);
    void DeleteFile(string path);
    void CreateDirectory(string path);
    IEnumerable<DirectoryEntry> GetEntries(string path);
}