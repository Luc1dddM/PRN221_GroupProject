namespace PRN221_GroupProject.Repository.File
{
    public interface IFileUploadRepository
    {
        public string UploadFile(IFormFile file);
    }
}
