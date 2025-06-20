using System.Buffers.Text;

namespace Evostory.Story.Models
{
    public class Image
    {
        public required Guid Id { get; set; }
        public string Path { get; set; }
    }
}
