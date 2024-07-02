
namespace PRN221_GroupProject.Repository.File
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public FileUploadRepository(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public string UploadFile(IFormFile file)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                    return s;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
