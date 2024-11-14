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
        if (_mode != "console")
            throw new ArgumentException($"Only 'console' mode can be used.");
        try
        {
            // Проверяем существование файла
            if (!_fileSystem.FileExists(_filePath))
            {
                Console.WriteLine($"Error: File not found: {_filePath}");
                return;
            }

            // Получаем содержимое файла
            var fileContent = _fileSystem.ReadFile(_filePath);

            // Выводим содержимое файла
            WriteFileToConsole(fileContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file {_filePath}: {ex.Message}");
        }
    }

    private void WriteFileToConsole(string content)
    {
        Console.WriteLine("File content:");
        Console.WriteLine(content);
    }
}