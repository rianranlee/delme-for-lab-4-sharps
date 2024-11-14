public class DisconnectCommand : ICommand
{
    private readonly IFileSystem _fileSystem;

    public DisconnectCommand(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void Execute()
    {
        try
        {
            _fileSystem.Disconnect();
            Console.WriteLine("Disconnected from the file system.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
