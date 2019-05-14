using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnWheels.Services.Services.FileService
{
    public interface IFileService
    {
        void SaveFile(IFormFile file, string fileName);
        void UpdateFile(string oldFileName, IFormFile formFile, string fileName);
        void DeleteFile(string fileName);

        string GetUniqueFileName(string fileName);
    }
}
