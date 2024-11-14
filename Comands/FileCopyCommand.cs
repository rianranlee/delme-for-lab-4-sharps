public class FileCopyCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public FileCopyCommand(IFileSystem fileSystem, string sourcePath, string destinationPath)
    {
        _fileSystem = fileSystem;
        _sourcePath = fileSystem.GetFullPath(sourcePath);
        _destinationPath = fileSystem.GetFullPath(destinationPath);
    }

    public void Execute()
    {
        try
        {
            // Получаем полный путь для исходного файла
            string sourceFullPath = _fileSystem.GetFullPath(_sourcePath);
            if (!File.Exists(sourceFullPath))
                throw new FileNotFoundException("Source file not found.", sourceFullPath);

            // Получаем полный путь для директории назначения
            string destinationFullPath = _fileSystem.GetFullPath(_destinationPath);
            string destinationDirectory = Path.GetDirectoryName(destinationFullPath)
                                          ?? throw new DirectoryNotFoundException("Destination directory is invalid.");

            if (!Directory.Exists(destinationDirectory))
                throw new DirectoryNotFoundException("Destination directory does not exist.");

            // Копируем файл
            File.Copy(sourceFullPath, destinationFullPath, overwrite: true);

            Console.WriteLine($"File successfully copied to: {destinationFullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error copying file: {ex.Message}");
        }
    }
}
