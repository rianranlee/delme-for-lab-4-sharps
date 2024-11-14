using System;
using System.Collections.Generic;

public static class CommandParser
{
    public static ICommand ParserCommands(string input, IFileSystem fileSystem)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Command cannot be empty.");
        }

        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var commandName = parts[0].ToLower();
        var arguments = parts.Length > 1 ? parts[1..] : Array.Empty<string>();

        return commandName switch
        {
            "connect" => ParseConnectCommand(arguments, fileSystem),
            "disconnect" => new DisconnectCommand(fileSystem),
            "copy" => ParseFileCopyCommand(arguments, fileSystem),
            "delete" => ParseFileDeleteCommand(arguments, fileSystem),
            "move" => ParseFileMoveCommand(arguments, fileSystem),
            "rename" => ParseFileRenameCommand(arguments, fileSystem),
            "show" => ParseFileShowCommand(arguments, fileSystem),
            "goto" => ParseTreeGotoCommand(arguments, fileSystem),
            "list" => ParseTreeListCommand(arguments, fileSystem),
            _ => throw new InvalidOperationException($"Unknown command: {commandName}")
        };
    }

    private static ICommand ParseConnectCommand(string[] args, IFileSystem fileSystem)
    {
        if (args.Length < 2)
        {
            throw new ArgumentException("Usage: connect <path> <mode>");
        }

        return new ConnectCommand(args[0], args[1], fileSystem);
    }

    private static ICommand ParseFileCopyCommand(string[] args, IFileSystem fileSystem)
    {
        if (args.Length < 2)
        {
            throw new ArgumentException("Usage: copy <sourcePath> <destinationPath>");
        }

        return new FileCopyCommand(fileSystem, args[0], args[1]);
    }

    private static ICommand ParseFileDeleteCommand(string[] args, IFileSystem fileSystem)
    {
        if (args.Length < 1)
        {
            throw new ArgumentException("Usage: delete <filePath>");
        }

        return new FileDeleteCommand(fileSystem, args[0]);
    }

    private static ICommand ParseFileMoveCommand(string[] args, IFileSystem fileSystem)
    {
        if (args.Length < 2)
        {
            throw new ArgumentException("Usage: move <sourcePath> <destinationPath>");
        }

        return new FileMoveCommand(fileSystem, args[0], args[1]);
    }

    private static ICommand ParseFileRenameCommand(string[] args, IFileSystem fileSystem)
    {
        if (args.Length < 2)
        {
            throw new ArgumentException("Usage: rename <filePath> <newName>");
        }

        return new FileRenameCommand(fileSystem, args[0], args[1]);
    }

    private static ICommand ParseFileShowCommand(string[] args, IFileSystem fileSystem)
    {
        if (args.Length < 2)
        {
            throw new ArgumentException("Usage: show <mode> <filePath>");
        }

        return new FileShowCommand(fileSystem, args[0], args[1]);
    }

    private static ICommand ParseTreeGotoCommand(string[] args, IFileSystem fileSystem)
    {
        if (args.Length < 1)
        {
            throw new ArgumentException("Usage: goto <path>");
        }

        return new TreeGotoCommand(args[0], fileSystem);
    }

    private static ICommand ParseTreeListCommand(string[] args, IFileSystem fileSystem)
    {
        int depth = args.Length > 0 && int.TryParse(args[0], out var parsedDepth) ? parsedDepth : 1;
        return new TreeListCommand(fileSystem, depth);
    }
}
