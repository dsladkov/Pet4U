using Microsoft.EntityFrameworkCore;
using Pet4U.Infrastructure;

public static class AppExtensions
{
    public async static Task ApplyMigrations(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
