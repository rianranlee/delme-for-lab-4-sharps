public class ConnectCommand : ICommand
{
    private readonly string _absoultePath;
    private readonly string _mode;
    private readonly IFileSystem _fileSystem;

    public ConnectCommand(string absoultePath, string mode, IFileSystem fileSystem)
    {
        _absoultePath = absoultePath;
        _mode = mode.ToLower();
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        if (_mode != "local")
            throw new ArgumentException($"Only 'local' mode can be used.");
        _fileSystem.Connect(_path);
        Console.WriteLine($"Connected to {_path}");
    }
}