// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using System;
// using System.Linq;

// namespace MiniBackend.Models
// {
//     public static class SeedData
//     {
//         public static void Initialize(IServiceProvider serviceProvider) 
//         {
//             using (var context = new MiniContext(serviceProvider.GetRequiredService<
//                 DbContextOptions<MiniContext>>()))
//             {
//                 if(context.Minis.Any()) 
//                 {
//                     return;
//                 }

//                 context.MiniMeta.AddRange(
//                     new MiniMeta { style = "Chibi", scale = 28 },
//                     new MiniMeta { style = "Sci-fi", scale = 28 },
//                     new MiniMeta { style = "Horror", scale = 32 }
//                 );
//                 context.SaveChanges();

//                 context.Games.AddRange(
//                     new Game { year_published = "2020", game_name = "Marvel United", box_art = "", meta_id = 2},
//                     new Game { year_published = "2022", game_name = "Marvel United X-Men", box_art = "", meta_id = 2},
//                     new Game { year_published = "2021", game_name = "Infinity N4", box_art = "", meta_id = 3},
//                     new Game { year_published = "2015", game_name = "Kingdom Death", box_art = "", meta_id = 4}
//                 );
//                 context.SaveChanges();

//                 context.Minis.AddRange(
//                     new Mini { completion_date = DateTime.UtcNow, mini_name = "Hulk", sculptor = "Big Child Creatives", game_id = 5 },
//                     new Mini { completion_date = DateTime.UtcNow, mini_name = "Iron Man", sculptor = "Big Child Creatives", game_id = 5 },
//                     new Mini { completion_date = DateTime.UtcNow, mini_name = "Wolverine", sculptor = "Big Child Creatives", game_id = 6 },
//                     new Mini { completion_date = DateTime.UtcNow, mini_name = "Alguacil w/Rocket Launcher", sculptor = "Corvus Belli", game_id = 7 },
//                     new Mini { completion_date = DateTime.UtcNow, mini_name = "Gorm", sculptor = "Kingdom Death", game_id = 8 }
//                 );
//                 context.SaveChanges();
//             }
//         }
//     }
// }