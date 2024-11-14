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
            string fullPath = _fileSystem.GetFullPath(_filePath);

            // Проверяем, существует ли файл
            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"File not found: {fullPath}");
                return;
            }

            // Определяем новый путь
            string directory = Path.GetDirectoryName(fullPath) ?? throw new InvalidOperationException("Directory not found");
            string newFilePath = Path.Combine(directory, _newName);

            // Проверяем, существует ли файл с новым именем
            if (File.Exists(newFilePath))
            {
                Console.WriteLine($"A file with the name '{_newName}' already exists in the directory.");
                return;
            }

            // Переименовываем файл
            File.Move(fullPath, newFilePath);
            Console.WriteLine($"File renamed successfully: {fullPath} -> {newFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error renaming file: {ex.Message}");
        }
    }
}