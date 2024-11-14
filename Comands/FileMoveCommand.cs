public class FileMoveCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public FileMoveCommand(IFileSystem fileSystem, string sourcePath, string destinationPath)
    {
        _fileSystem = fileSystem;
        _sourcePath = fileSystem.GetFullPath(sourcePath);
        _destinationPath = fileSystem.GetFullPath(destinationPath);
    }

    public void Execute()
    {
        try
        {
            var files = Directory.GetFiles(_fileSystem.CurrentDirectory);
            var resolvedSourcePath = CollisionChecker.CollisionCheck(files, Path.GetFileName(_sourcePath));

            _fileSystem.MoveFile(resolvedSourcePath, _destinationPath);
            Console.WriteLine($"File moved to {_destinationPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error moving file: {ex.Message}");
        }
    }
}
