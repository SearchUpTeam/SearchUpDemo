using System.ComponentModel.DataAnnotations.Schema;

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
        public string Path { get; set; }
    }
}