
namespace PRN221_GroupProject.Repository.File
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public FileUploadRepository(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public void UploadFile(IFormFile file)
        {
            try
            {
                var filePath = Path.Combine(_environment.ContentRootPath, @"wwwroot\img\Product", file.FileName);
                using var filestream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(filestream);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
