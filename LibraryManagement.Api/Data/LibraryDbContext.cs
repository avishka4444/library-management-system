using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using LibraryManagement.Api.Models;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.Api.Data;

public class LibraryDbContext : DataConnection
{
    public LibraryDbContext(IConfiguration configuration) 
        : base(new DataOptions().UseConnectionString(
            SqlServerTools.GetDataProvider(SqlServerVersion.v2012, SqlServerProvider.MicrosoftDataSqlClient),
            GetConnectionString(configuration)))
    {
    }

    private static string GetConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            var availableKeys = configuration.GetSection("ConnectionStrings").GetChildren().Select(c => c.Key).ToList();
            throw new InvalidOperationException(
                $"Connection string 'DefaultConnection' is not provided. " +
                $"Please check your appsettings.json or environment variables. " +
                $"Available connection string keys: {(availableKeys.Any() ? string.Join(", ", availableKeys) : "none")}");
        }
        return connectionString;
    }

    public ITable<Book> Books => this.GetTable<Book>();
    public ITable<Author> Authors => this.GetTable<Author>();
    public ITable<Member> Members => this.GetTable<Member>();
    public ITable<Borrowing> Borrowings => this.GetTable<Borrowing>();
}

