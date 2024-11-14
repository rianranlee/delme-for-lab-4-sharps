public class FileDeleteCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly string _filePath;

    public FileDeleteCommand(IFileSystem fileSystem, string filePath)
    {
        _fileSystem = fileSystem;
        _filePath = fileSystem.GetFullPath(filePath);
    }

    public void Execute()
    {
        try
        {
            var files = Directory.GetFiles(_fileSystem.CurrentDirectory);
            var resolvedFile = CollisionChecker.CollisionCheck(files, Path.GetFileName(_filePath));

            _fileSystem.DeleteFile(resolvedFile);
            Console.WriteLine($"File deleted: {resolvedFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting file: {ex.Message}");
        }
    }
}
