public class TreeGotoCommand : ICommand
{
    private readonly string _path;
    private readonly IFileSystem _fileSystem;

    public TreeGotoCommand(string path, IFileSystem fileSystem)
    {
        _path = fileSystem.GetFullPath(path);
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        try
        {
            _fileSystem.GoToDirectory(_path);
            Console.WriteLine($"Changed directory to {_path}");
        }
        catch (DirectoryNotFoundException exception)
        {
            Console.WriteLine($"Error: {exception.Message}");
        }
    }
}