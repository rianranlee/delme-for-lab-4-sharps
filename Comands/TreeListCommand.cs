public class TreeListCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly int _depth;
    private readonly string _folderSymbol;
    private readonly string _fileSymbol;
    private readonly string _indentationSymbol;

    public TreeListCommand(IFileSystem fileSystem, int depth = 1, 
                           string folderSymbol = "📁", string fileSymbol = "📄", 
                           string indentationSymbol = "--")
    {
        _fileSystem = fileSystem;
        _depth = depth;
        _folderSymbol = folderSymbol;
        _fileSymbol = fileSymbol;
        _indentationSymbol = indentationSymbol;
    }

    public void Execute()
    {
        try
        {
            var currentDirectory = _fileSystem.CurrentDirectory;

            // Проверяем коллизии, если путь неоднозначен
            var matchingDirectories = Directory.GetDirectories(
                Path.GetDirectoryName(currentDirectory) ?? string.Empty, 
                Path.GetFileName(currentDirectory));
            
            var resolvedPath = matchingDirectories.Length > 1
                ? CollisionChecker.CollisionCheck(matchingDirectories, Path.GetFileName(currentDirectory))
                : currentDirectory;

            ListDirectoryTree(resolvedPath, 0);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void ListDirectoryTree(string path, int currentDepth)
    {
        if (currentDepth >= _depth) return;

        try
        {
            var directories = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);

            Console.WriteLine($"{new string(_indentationSymbol[0], currentDepth * 2)}{_folderSymbol} {Path.GetFileName(path)}\\");

            foreach (var directory in directories)
            {
                ListDirectoryTree(directory, currentDepth + 1);
            }

            foreach (var file in files)
            {
                Console.WriteLine($"{new string(_indentationSymbol[0], (currentDepth + 1) * 2)}{_fileSymbol} {Path.GetFileName(file)}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{new string(_indentationSymbol[0], currentDepth * 2)}Error reading directory {path}: {ex.Message}");
        }
    }
}
