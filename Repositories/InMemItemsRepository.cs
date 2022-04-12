using System.Collections.Generic;
using System.Linq;
using MiniBackend.Models;
using Pagination.Dtos;

namespace MiniBackend.Repositories
{
    public class InMemMinisRepository : IMinisRepository
    {
        public InMemMinisRepository() {
            this.games = SetupGames();
            this.minis = SetupMinis(games);
        }
        private readonly List<Game> games;

        private readonly List<Mini> minis;
        
        private List<Mini> SetupMinis(List<Game> games)
        { 
            return new()
            {
                SetupMiniWithGame(new Mini { MiniId = 1245, CompletionDate = DateTime.UtcNow, MiniName = "Hulk", Sculptor = "Big Child Creatives"}, games[0]),
                SetupMiniWithGame(new Mini { MiniId = 1249, CompletionDate = DateTime.UtcNow, MiniName = "Wolverine", Sculptor = "Big Child Creatives"}, games[1]),
                SetupMiniWithGame(new Mini { MiniId = 2542, CompletionDate = DateTime.UtcNow, MiniName = "Alguacile w/Rocket Launcher", Sculptor = "Corvus Belli"}, games[2]),
                SetupMiniWithGame(new Mini { MiniId = 7659, CompletionDate = DateTime.UtcNow, MiniName = "Gorm", Sculptor = "Kingdom Death"}, games[3]),
            };
        }

        private Mini SetupMiniWithGame(Mini mini, Game game) {
            return new Mini { MiniId = mini.MiniId, CompletionDate = mini.CompletionDate, MiniName = mini.MiniName, Sculptor = mini.Sculptor, Game = game};
        }

        private readonly List<Photo> photos = new()
        {
            new Photo { PhotoId = 1, Filename = "fakePic1"}
        };

        private List<Game> SetupGames() {
            return new()
            {
                new Game { GameId = 1, YearPublished = "2020", GameName = "Marvel United", BoxArtUrl = ""},
                new Game { GameId = 2, YearPublished = "2022", GameName = "Marvel United X-Men", BoxArtUrl = ""},
                new Game { GameId = 3, YearPublished = "2021", GameName = "Infinity N4", BoxArtUrl = ""},
                new Game { GameId = 4, YearPublished = "2015", GameName = "Kingdom Death", BoxArtUrl = ""}
            };
        }

        public IEnumerable<Mini> GetMinis()
        {
            return minis;
        }

        public Mini GetMini(int id)
        {
            return minis.Where(mini => mini.MiniId == id).SingleOrDefault();
        }

        public IEnumerable<Mini> GetMinisByGame(int id)
        {
            return minis.Where(mini => mini.Game.GameId == id);
        }

        public void CreateMini(Mini mini)
        {
            minis.Add(mini);
        }

        public void UpdateMini(Mini mini)
        {
            var index = minis.FindIndex(existingMini => existingMini.MiniId == mini.MiniId);
            minis[index] = mini;
        }

        public void DeleteMini(int id)
        {
            var index = minis.FindIndex(existingMini => existingMini.MiniId == id);
            minis.RemoveAt(index);
        }

        public IEnumerable<Game> GetGames()
        {
            return games;
        }

        public Game GetGame(int id)
        {
            return games.Where(game => game.GameId == id).SingleOrDefault();
        }

        public void CreateGame(Game game)
        {
            games.Add(game);
        }

        public void UpdateGame(Game game)
        {
            var index = games.FindIndex(existingGame => existingGame.GameId == game.GameId);
            games[index] = game;
        }

        public void DeleteGame(int id)
        {
            var index = games.FindIndex(existingGame => existingGame.GameId == id);
            games.RemoveAt(index);
        }

        // photos
        public IEnumerable<Photo> GetPhotosForMini(int id)
        {
            return photos.Where(photo => photo.Mini.MiniId == id);
        }

        public void CreatePhoto(Photo photo)
        {
            photos.Add(photo);
        }

        public void UpdatePhoto(Photo photo)
        {
            var index = photos.FindIndex(p => p.PhotoId == photo.PhotoId);
            photos[index] = photo;
        }

        public void DeletePhoto(int id)
        {
            var index = photos.FindIndex(photo => photo.PhotoId == id);
            photos.RemoveAt(index);
        }

        public Photo GetPhoto(int id)
        {
            throw new NotImplementedException();
        }

        public MiniMeta GetMeta(int id)
        {
            throw new NotImplementedException();
        }

        public int FindMetaIdByValues(string style, string scale)
        {
            throw new NotImplementedException();
        }

        public void CreateMeta(MiniMeta meta)
        {
            throw new NotImplementedException();
        }

        public Task<GetMinisPaginatedDto> GetMinisByPageAsync(int limit, int page, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}