using Microsoft.EntityFrameworkCore;
using MiniBackend.DTOs;
using MiniBackend.Models;
using Pagination.Models;

namespace MiniBackend
{
    public static class Extension {
        public static MiniDTO AsDto(this Mini mini)
        {
            try {
                var filenames = mini.Photos.Select(photo => photo.Filename);
                return new MiniDTO {
                    Id = mini.MiniId,
                    CompletionDate = mini.CompletionDate,
                    MiniName = mini.MiniName,
                    Sculptor = mini.Sculptor,
                    GameId = mini.Game.GameId,
                    FileNames = filenames.ToArray()
                };
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public static GameDTO AsDto(this Game game)
        {
            return new GameDTO
            {
                Id = game.GameId,
                YearPublished = game.YearPublished,
                GameName = game.GameName,
                BoxArtUrl = game.BoxArtUrl,
                Style = game.MiniMeta.Style,
                Scale = game.MiniMeta.Scale
            };
        }

        public static PhotoDTO AsDto(this Photo photo)
        {
            return new PhotoDTO
            {
                Id = photo.PhotoId,
                FileName = photo.Filename,
                MiniId = photo.Mini.MiniId
            };
        }
    }
}

namespace Pagination {
    public static class Extensions{
        public static async Task<PagedModel<TModel>> PaginateAsync<TModel>(
            this IQueryable<TModel> query,
            int page,
            int limit,
            CancellationToken cancellationToken)
            where TModel : class
        {

            var paged = new PagedModel<TModel>();

            page = (page < 0) ? 1 : page;

            paged.CurrentPage = page;
            paged.PageSize = limit;

            var totalItemsCountTask = query.CountAsync(cancellationToken);

            var startRow = (page - 1) * limit;
            paged.Items = await query
                    .Skip(startRow)
                    .Take(limit)
                    .ToListAsync(cancellationToken);

            paged.TotalItems = await totalItemsCountTask;
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

            return paged;
        }
    }
}