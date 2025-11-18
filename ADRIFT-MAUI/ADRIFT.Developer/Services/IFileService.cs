namespace ADRIFT.Services;

public interface IFileService
{
    Task<string?> PickFileAsync(FilePickerOptions options);
    Task<string?> SaveFileAsync(FileSaverOptions options);
    Task<Stream?> OpenReadAsync(string filePath);
    Task<Stream> OpenWriteAsync(string filePath);
    Task<bool> FileExistsAsync(string filePath);
    Task<string> ReadAllTextAsync(string filePath);
    Task WriteAllTextAsync(string filePath, string content);
}

public class FilePickerOptions
{
    public string Title { get; set; } = "Select a file";
    public IEnumerable<string>? FileTypes { get; set; }
}

public class FileSaverOptions
{
    public string Title { get; set; } = "Save file";
    public string? SuggestedFileName { get; set; }
    public IEnumerable<string>? FileTypes { get; set; }
}
