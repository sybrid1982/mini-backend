using System;
using MiniBackend.Models;

namespace MiniBackend.DTOs
{
    public record GameDTO {
        public int Id { get; init; }
        public string YearPublished { get; init; }
        public string GameName { get; init; }
        public string BoxArtUrl { get; init; }
        public string Style { get; init; }
        public string Scale { get; init; }
    }
}