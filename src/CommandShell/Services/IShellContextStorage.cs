using CommandShell.Core;
using CommandShell.FileSystem;
namespace CommandShell.Services;

public interface IShellContextStorage
{
    ShellContext Load();
    void Save(ShellContext context);
}