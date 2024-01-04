using System.ComponentModel.DataAnnotations;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        var extension = Path.GetExtension(file!.FileName);
        if (file != null)
        {
            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(GetErrorMessage());
            }
        }
        return ValidationResult.Success!;
    }

    public string GetErrorMessage()
    {
        return $"Ban can upload file anh.";
    }
}