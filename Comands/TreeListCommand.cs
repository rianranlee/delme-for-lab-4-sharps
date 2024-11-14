 public class TreeListCommand : ICommand
{
        private readonly IFileSystem _fileSystem;
        private readonly int _depth;
        private readonly string _folderSymbol;
        private readonly string _fileSymbol;
        private readonly string _indentationSymbol;

        // Конструктор с параметрами для глубины, символов для файлов и папок, и символа отступа
        public TreeListCommand(IFileSystem fileSystem, int depth = 1, 
                               string folderSymbol = "📁", string fileSymbol = "📄", string indentationSymbol = "--")
        {
            _fileSystem = fileSystem;
            _depth = depth;  // Устанавливаем глубину, по умолчанию 1
            _folderSymbol = folderSymbol;
            _fileSymbol = fileSymbol;
            _indentationSymbol = indentationSymbol;
        }

        public void Execute()
        {
            try
            {
                ListDirectoryTree(_fileSystem.CurrentDirectory, 0);  // Начинаем с корня (глубина 0)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Метод для рекурсивного вывода содержимого каталога
        private void ListDirectoryTree(string path, int currentDepth)
        {
            // Если текущая глубина больше максимальной глубины, не продолжаем обход
            if (currentDepth >= _depth) return;

            // Получаем содержимое каталога
            var directories = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);

            // Отображаем текущий каталог
            Console.WriteLine($"{new string(_indentationSymbol[0], currentDepth * 2)}{_folderSymbol} {Path.GetFileName(path)}\\");

            // Рекурсивно выводим содержимое подкаталогов, увеличиваем глубину
            foreach (var directory in directories)
            {
                ListDirectoryTree(directory, currentDepth + 1);  // Увеличиваем глубину при переходе в подкаталог
            }

            // Выводим файлы
            foreach (var file in files)
            {
                Console.WriteLine($"{new string(_indentationSymbol[0], (currentDepth + 1) * 2)}{_fileSymbol} {Path.GetFileName(file)}");
            }
        }
}