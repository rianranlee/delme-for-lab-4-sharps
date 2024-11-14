using System;
using System.IO;

public class FileSystem : IFileSystem
{
    private string _currentDirectory;

    public string CurrentDirectory => _currentDirectory;

    public void Connect(string path)
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException($"Directory does not exist: {path}");

        _currentDirectory = path;
        Console.WriteLine($"Connected to: {_currentDirectory}");
    }

    public void Disconnect()
    {
        Console.WriteLine("Disconnected from the file system.");
        _currentDirectory = null;
    }

    public void ChangeRoot(string newRoot)
    {
        if (!Directory.Exists(newRoot))
            throw new DirectoryNotFoundException($"Root does not exist: {newRoot}");

        _currentDirectory = newRoot;
        Console.WriteLine($"Switched to root: {newRoot}");
    }
}
