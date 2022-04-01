using System.ComponentModel.DataAnnotations;

namespace MiniBackend.DTOs
{
    public record UpdateMiniDto
    {
        [Required]
        public string MiniName { get; init; }
        public string Sculptor { get; init; }
        public int GameId { get; init; }
        public DateTime CompletionDate { get; init; }
    }
}