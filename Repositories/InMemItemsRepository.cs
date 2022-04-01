// using System.Collections.Generic;
// using System.Linq;
// using MiniBackend.Models;

// namespace MiniBackend.Repositories
// {
//     public class InMemMinisRepository : IMinisRepository
//     { 
//         private readonly List<Mini> minis = new()
//         {
//             new Mini { mini_id = 1245, completion_date = DateTime.UtcNow, mini_name = "Hulk", sculptor = "Big Child Creatives", game_id = 1 },
//             new Mini { mini_id = 1249, completion_date = DateTime.UtcNow, mini_name = "Wolverine", sculptor = "Big Child Creatives", game_id = 2 },
//             new Mini { mini_id = 2542, completion_date = DateTime.UtcNow, mini_name = "Alguacile w/Rocket Launcher", sculptor = "Corvus Belli", game_id = 3 },
//             new Mini { mini_id = 7659, completion_date = DateTime.UtcNow, mini_name = "Gorm", sculptor = "Kingdom Death", game_id = 4 }
//         };

//         private readonly List<Game> games = new()
//         {
//             new Game { game_id = 1, year_published = "2020", game_name = "Marvel United", box_art = "", meta_id = 2},
//             new Game { game_id = 2, year_published = "2022", game_name = "Marvel United X-Men", box_art = "", meta_id = 2},
//             new Game { game_id = 3, year_published = "2021", game_name = "Infinity N4", box_art = "", meta_id = 3},
//             new Game { game_id = 4, year_published = "2015", game_name = "Kingdom Death", box_art = "", meta_id = 4}
//         };

//         private readonly List<Photo> photos = new()
//         {
//             new Photo { photo_id = 1, filename = "fakePic1", mini_id = 1245}
//         };

//         public IEnumerable<Mini> GetMinis()
//         {
//             return minis;
//         }

//         public Mini GetMini(int id)
//         {
//             return minis.Where(mini => mini.mini_id == id).SingleOrDefault();
//         }

//         public IEnumerable<Mini> GetMinisByGame(int id)
//         {
//             return minis.Where(mini => mini.game_id == id);
//         }

//         public void CreateMini(Mini mini)
//         {
//             minis.Add(mini);
//         }

//         public void UpdateMini(Mini mini)
//         {
//             var index = minis.FindIndex(existingMini => existingMini.mini_id == mini.mini_id);
//             minis[index] = mini;
//         }

//         public void DeleteMini(int id)
//         {
//             var index = minis.FindIndex(existingMini => existingMini.mini_id == id);
//             minis.RemoveAt(index);
//         }

//         public IEnumerable<Game> GetGames()
//         {
//             return games;
//         }

//         public Game GetGame(int id)
//         {
//             return games.Where(game => game.game_id == id).SingleOrDefault();
//         }

//         public void CreateGame(Game game)
//         {
//             games.Add(game);
//         }

//         public void UpdateGame(Game game)
//         {
//             var index = games.FindIndex(existingGame => existingGame.game_id == game.game_id);
//             games[index] = game;
//         }

//         public void DeleteGame(int id)
//         {
//             var index = games.FindIndex(existingGame => existingGame.game_id == id);
//             games.RemoveAt(index);
//         }

//         // photos
//         public IEnumerable<Photo> GetPhotosForMini(int id)
//         {
//             return photos.Where(photo => photo.mini_id == id);
//         }

//         public void CreatePhoto(Photo photo)
//         {
//             photos.Add(photo);
//         }

//         public void UpdatePhoto(Photo photo)
//         {
//             var index = photos.FindIndex(p => p.photo_id == photo.photo_id);
//             photos[index] = photo;
//         }

//         public void DeletePhoto(int id)
//         {
//             var index = photos.FindIndex(photo => photo.photo_id == id);
//             photos.RemoveAt(index);
//         }
//     }
// }