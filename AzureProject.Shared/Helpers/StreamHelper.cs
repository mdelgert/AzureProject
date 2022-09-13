namespace AzureProject.Shared.Helpers;

public static class StreamHelper
{
    public static MemoryStream Convert(string contents)
    {
        //convert string to stream
        var byteArray = Encoding.UTF8.GetBytes(contents);
        var memoryStream = new MemoryStream(byteArray);
        return memoryStream;
    }

    public static string Revert(MemoryStream stream)
    {
        //convert stream to string
        var reader = new StreamReader(stream);
        var contents = reader.ReadToEnd();
        return contents;
    }

    public static async Task SaveStream(string filePath, Stream stream)
    {
        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await stream.CopyToAsync(fileStream);
    }
    
    public static async Task SaveToFile(string? filePath, MemoryStream memoryStream)
    {
        var directoryPath = Path.GetDirectoryName(filePath);

        if (!Directory.Exists(directoryPath))
            if (directoryPath != null)
                Directory.CreateDirectory(directoryPath);

        if (filePath != null)
        {
            await using var file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            var bytes = new byte[memoryStream.Length];
            var read = memoryStream.Read(bytes, 0, (int) memoryStream.Length);
            file.Write(bytes, 0, bytes.Length);
        }

        memoryStream.Close();
    }

    public static async Task<MemoryStream> ReadFile(string filePath)
    {
        using var ms = new MemoryStream();
        await using var file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var bytes = new byte[file.Length];
        var read = file.Read(bytes, 0, (int) file.Length);
        ms.Write(bytes, 0, (int) file.Length);
        return ms;
    }
}

//https://stackoverflow.com/questions/8047064/convert-string-to-system-io-stream
//https://stackoverflow.com/questions/8624071/save-and-load-memorystream-to-from-a-file
