public class FileShowCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly string _filePath;
    private readonly string _mode;

    public FileShowCommand(IFileSystem fileSystem, string mode, string filePath)
    {
        _fileSystem = fileSystem;
        _filePath = fileSystem.GetFullPath(filePath);
        _mode = mode.ToLower();
    }

    public void Execute()
    {
        try
        {
            var files = Directory.GetFiles(_fileSystem.CurrentDirectory);
            var resolvedFile = CollisionChecker.CollisionCheck(files, Path.GetFileName(_filePath));

            var fileContent = _fileSystem.ReadFile(resolvedFile);
            WriteFileToConsole(fileContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void WriteFileToConsole(string content)
    {
        Console.WriteLine("File content:");
        Console.WriteLine(content);
    }
}
