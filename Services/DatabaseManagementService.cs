using Microsoft.EntityFrameworkCore;
using MiniBackend.Models;

public static class DatabaseManagementService
{
     // Getting the scope of our database context
    public static void MigrationInitialisation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            try {
                // Takes all of our migrations files and apply them against the database in case they are not implemented
                serviceScope.ServiceProvider.GetService<MiniContext>().Database.Migrate();
            } catch (Exception ex) {
                Console.WriteLine("Failed to do migrate for service");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.HelpLink);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
    }
}