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
            // Проверяем существование исходного файла
            if (!_fileSystem.FileExists(_sourcePath))
            {
                Console.WriteLine($"Error: Source file not found: {_sourcePath}");
                return;
            }

            // Проверяем существование папки назначения
            var destinationFolder = Path.GetDirectoryName(_destinationPath);
            if (destinationFolder == null || !_fileSystem.DirectoryExists(destinationFolder))
            {
                Console.WriteLine($"Error: Destination folder not found: {destinationFolder}");
                return;
            }

            // Выполняем перемещение файла
            _fileSystem.MoveFile(_sourcePath, _destinationPath);

            Console.WriteLine($"File moved successfully from {_sourcePath} to {_destinationPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error moving file: {ex.Message}");
        }
    }
}