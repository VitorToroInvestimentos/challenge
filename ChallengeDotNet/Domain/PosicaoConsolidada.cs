namespace ChallengeDotNet.Domain;

public class PosicaoConsolidada
{
    public int Id { get; set; }
    public decimal Financeiro { get; set; }
    public IEnumerable<Custodia> Custodia { get; set; }
}