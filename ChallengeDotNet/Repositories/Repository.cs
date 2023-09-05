using ChallengeDotNet.Domain;

namespace ChallengeDotNet.Repositories;

public class Repository
{
    public int Add(PosicaoConsolidada posicaoConsolidada)
    {
        using var dbContext = new ApplicationDbContext();
        dbContext.Database.EnsureCreated();

        dbContext.EntidadeDeCalculo.Add(posicaoConsolidada);
        dbContext.SaveChanges();

        return 1;
    }

    public PosicaoConsolidada GetById(int id)
    {
        using var dbContext = new ApplicationDbContext();
        dbContext.Database.EnsureCreated();

        var result = dbContext.EntidadeDeCalculo.Find(id);
        
        return result;
    }
}