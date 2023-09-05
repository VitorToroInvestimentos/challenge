using ChallengeDotNet.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChallengeDotNet;

public class ApplicationDbContext : DbContext
{
    public DbSet<PosicaoConsolidada> EntidadeDeCalculo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=server;Database=dataBase;User=user;Password=password;";
        optionsBuilder.UseSqlServer(connectionString);
    }
}