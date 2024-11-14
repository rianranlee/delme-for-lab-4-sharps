public class ConnectCommand : ICommand
{
    private readonly string _absolutePath;
    private readonly string _mode;
    private readonly IFileSystem _fileSystem;

    public ConnectCommand(string absolutePath, string mode, IFileSystem fileSystem)
    {
        _absolutePath = absolutePath;
        _mode = mode.ToLower();
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        try
        {
            if (_mode != "local")
                throw new ArgumentException($"Only 'local' mode can be used.");

            // Проверяем существование пути
            if (!Directory.Exists(_absolutePath))
            {
                Console.WriteLine($"Error: Directory not found: {_absolutePath}");
                return;
            }

            // Разрешаем коллизии, если есть несколько подходящих путей
            var matchingDirectories = Directory.GetDirectories(
                Path.GetDirectoryName(_absolutePath) ?? string.Empty, 
                Path.GetFileName(_absolutePath));
            
            var resolvedPath = matchingDirectories.Length > 1
                ? CollisionChecker.CollisionCheck(matchingDirectories, Path.GetFileName(_absolutePath))
                : _absolutePath;

            _fileSystem.Connect(resolvedPath);
            Console.WriteLine($"Connected to {resolvedPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
