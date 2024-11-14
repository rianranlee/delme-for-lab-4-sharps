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
            string fullPath = _fileSystem.GetFullPath(_filePath);

            // Проверяем, существует ли файл
            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"File not found: {fullPath}");
                return;
            }

            // Удаляем файл
            File.Delete(fullPath);
            Console.WriteLine($"File successfully deleted: {fullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting file: {ex.Message}");
        }
    }
}