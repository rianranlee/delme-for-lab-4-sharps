public class FileRenameCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly string _filePath;
    private readonly string _newName;

    public FileRenameCommand(IFileSystem fileSystem, string filePath, string newName)
    {
        _fileSystem = fileSystem;
        _filePath = fileSystem.GetFullPath(filePath);
        _newName = newName;
    }

    public void Execute()
    {
        try
        {
            var files = Directory.GetFiles(_fileSystem.CurrentDirectory);
            var resolvedFile = CollisionChecker.CollisionCheck(files, Path.GetFileName(_filePath));

            string directory = Path.GetDirectoryName(resolvedFile);
            string newFilePath = Path.Combine(directory, _newName);

            if (File.Exists(newFilePath))
            {
                Console.WriteLine($"Error: A file with the name '{_newName}' already exists.");
                return;
            }

            _fileSystem.RenameFile(resolvedFile, newFilePath);
            Console.WriteLine($"File renamed successfully to {newFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error renaming file: {ex.Message}");
        }
    }
}
