namespace Identity.ViewModels.File;

public record FileViewModel
{
    public string? Extension { get; set; }

    public string? Path { get; set; }
}

public record FileBase64ViewModel(string Base64) : FileViewModel;

public record FileBytesViewModel(byte[] Bytes) : FileViewModel;