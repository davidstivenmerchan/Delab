using Microsoft.AspNetCore.Http;

namespace Delab.Helpers;

public interface IFileStorage
{
    Task<string> UploadImage(IFormFile imageFile, string ruta, string guid);

    Task<string> UploadImage(byte[] imageFile, string ruta, string guid);

    bool DeleteImage(string ruta, string guid);
}
