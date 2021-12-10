using Identity.ViewModels.File;

namespace Identity.Tools.File;

public static class FileExtensions
{
    public static async Task<string?> SaveFileBase64Async(this FileBase64ViewModel fileBase64)
        => await Task.Run(async () =>
        {
            byte[] buffer = Convert.FromBase64String(fileBase64.Base64);
            return await new FileBytesViewModel(buffer)
            {
                Extension = fileBase64.Extension,
                Path = fileBase64.Path
            }.SaveFileBytesAsync();
        });

    public static async Task<string?> SaveFileBytesAsync(this FileBytesViewModel fileBytes)
        => await Task.Run(async () =>
        {
            try
            {
                string? path = Directory.GetCurrentDirectory() + $"/wwwroot/files/{fileBytes.Path}";
                string fileName = Guid.NewGuid() + fileBytes.Extension;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                await WriteAllBytesAsync(path + $"/{fileName}", fileBytes.Bytes);
                return fileName;
            }
            catch
            {
                return "null.png";
            }
        });

    public static string CreateFileAddress(this string fileName, string path)
        => $"https://localhost:44343/files/{path}/{fileName}";
}
