public class ConnectCommand : ICommand
{
    private readonly string _pathOrRoot; // Может быть абсолютным путем или новым корнем (например, D:)
    private readonly string _mode;
    private readonly IFileSystem _fileSystem;

    public ConnectCommand(string pathOrRoot, string mode, IFileSystem fileSystem)
    {
        _pathOrRoot = pathOrRoot;
        _mode = mode.ToLower();
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        try
        {
            if (_mode != "local")
                throw new ArgumentException($"Only 'local' mode can be used.");

            // Проверяем, является ли входная строка диском (например, "D:")
            if (_pathOrRoot.Length == 2 && _pathOrRoot.EndsWith(":"))
            {
                _fileSystem.ChangeRoot(_pathOrRoot);
                Console.WriteLine($"Switched to {_pathOrRoot}");
                return;
            }

            // Проверяем существование пути
            if (!Directory.Exists(_pathOrRoot))
            {
                Console.WriteLine($"Error: Directory not found: {_pathOrRoot}");
                return;
            }

            // Разрешаем коллизии
            var matchingDirectories = Directory.GetDirectories(
                Path.GetDirectoryName(_pathOrRoot) ?? string.Empty, 
                Path.GetFileName(_pathOrRoot));
            
            var resolvedPath = matchingDirectories.Length > 1
                ? CollisionChecker.CollisionCheck(matchingDirectories, Path.GetFileName(_pathOrRoot))
                : _pathOrRoot;

            _fileSystem.Connect(resolvedPath);
            Console.WriteLine($"Connected to {resolvedPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
