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
            var directories = Directory.GetDirectories(_fileSystem.CurrentDirectory);
            var resolvedPath = CollisionChecker.CollisionCheck(directories, Path.GetFileName(_path));

            _fileSystem.GoToDirectory(resolvedPath);
            Console.WriteLine($"Changed directory to {resolvedPath}");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error: {exception.Message}");
        }
    }
}
