using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Программа для обработки команд ===");
        var fileSystem = new FileSystemStub();

        while (true)
        {
            Console.Write("\nВведите команду (или 'exit' для выхода): ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: Команда не может быть пустой.");
                continue;
            }

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Выход из программы.");
                break;
            }

            try
            {
                // Парсим команду
                ICommand command = CommandParser.ParseCommand(input, fileSystem);

                // Исполняем команду
                command.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обработки команды: {ex.Message}");
            }
        }
    }
}

// Заглушка для интерфейса файловой системы
public class FileSystemStub : IFileSystem
{
    public void Connect(string path)
    {
        Console.WriteLine($"Подключение к каталогу: {path}");
    }

    public void Disconnect()
    {
        Console.WriteLine("Отключение от файловой системы.");
    }

    public string CurrentDirectory => "/mock/directory";

    public void ChangeRoot(string newRoot)
    {
        Console.WriteLine($"Смена корня на: {newRoot}");
    }

    public void CopyFile(string sourcePath, string destinationPath)
    {
        Console.WriteLine($"Копирование файла из {sourcePath} в {destinationPath}");
    }

    public void DeleteFile(string filePath)
    {
        Console.WriteLine($"Удаление файла: {filePath}");
    }

    public void MoveFile(string sourcePath, string destinationPath)
    {
        Console.WriteLine($"Перемещение файла из {sourcePath} в {destinationPath}");
    }

    public void RenameFile(string filePath, string newName)
    {
        Console.WriteLine($"Переименование файла {filePath} на {newName}");
    }

    public string ReadFile(string filePath)
    {
        Console.WriteLine($"Чтение файла: {filePath}");
        return "Содержимое файла";
    }

    public void GoToDirectory(string path)
    {
        Console.WriteLine($"Переход в каталог: {path}");
    }
}
