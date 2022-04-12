using System.Collections.Generic;
using MiniBackend.DTOs;

namespace Pagination.Dtos {
    public record GetMinisPaginatedDto
    {
        public int CurrentPage { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages { get; init; }
        public List<MiniDTO> Minis { get; init; }
    }
}