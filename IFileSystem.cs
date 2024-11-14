public interface IFileSystem
{
    void Connect(string path);
    void Disconnect();
    string CurrentDirectory { get; }
    void ChangeRoot(string newRoot);
}