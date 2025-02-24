namespace ERPSYS.Models
{
    public class FileUploadModel
    {
        public IFormFile ImageFile { get; set; }
    }
    public class FileRecord
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string DropboxPath { get; set; }
        public DateTime UploadedAt { get; set; }
    }


}
