using CommandShell.FileSystem;
namespace CommandShell.Services;

public interface IFileSystemStorage
{
    VirtualDirectory Load();
    void Save(VirtualDirectory root);
}