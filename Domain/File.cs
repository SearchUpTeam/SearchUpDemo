using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public enum FileType
    {
        img, video, document
    }
    public abstract class File
    {
        public int Id { get; set; }
        public FileType FileType { get; set; }
        [Required]
        public string Path { get; set; }
    }
}