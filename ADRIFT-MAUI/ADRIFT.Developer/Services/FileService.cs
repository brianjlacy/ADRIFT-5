using CommunityToolkit.Maui.Storage;

namespace ADRIFT.Services;

public class FileService : IFileService
{
    public async Task<string?> PickFileAsync(FilePickerOptions options)
    {
        try
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, options.FileTypes ?? new[] { "public.item" } },
                    { DevicePlatform.Android, options.FileTypes ?? new[] { "*/*" } },
                    { DevicePlatform.WinUI, options.FileTypes ?? new[] { "*.*" } },
                    { DevicePlatform.macOS, options.FileTypes ?? new[] { "*" } },
                });

            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = options.Title,
                FileTypes = customFileType
            });

            return result?.FullPath;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<string?> SaveFileAsync(FileSaverOptions options)
    {
        try
        {
            // Use CommunityToolkit.Maui FileSaver
            using var stream = new MemoryStream();
            var result = await FileSaver.Default.SaveAsync(
                options.SuggestedFileName ?? "file.taf",
                stream,
                CancellationToken.None);

            return result.FilePath;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Stream?> OpenReadAsync(string filePath)
    {
        try
        {
            return await Task.Run(() => File.OpenRead(filePath));
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Stream> OpenWriteAsync(string filePath)
    {
        return await Task.Run(() => File.OpenWrite(filePath));
    }

    public async Task<bool> FileExistsAsync(string filePath)
    {
        return await Task.Run(() => File.Exists(filePath));
    }

    public async Task<string> ReadAllTextAsync(string filePath)
    {
        return await File.ReadAllTextAsync(filePath);
    }

    public async Task WriteAllTextAsync(string filePath, string content)
    {
        await File.WriteAllTextAsync(filePath, content);
    }
}
