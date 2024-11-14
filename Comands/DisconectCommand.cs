public class DisconnectCommand : ICommand
{
    private readonly IFileSystem _fileSystem;

    public DisconnectCommand(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        _fileSystem.Disconnect();
        Console.WriteLine("Disconnected from the file system.");
    }
}