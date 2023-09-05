using System.Text;
using ChallengeDotNet.Domain;
using ChallengeDotNet.Repositories;
using Newtonsoft.Json;

namespace ChallengeDotNet;

public class OrquestradorDeCalculo
{
    private readonly Repository _repository;

    public OrquestradorDeCalculo()
    {
        _repository = new Repository();
    }

    public async void CalcularENotificar(int id)
    {
        try
        {
            CalculoFinanceiro(id);
            CalculoCustodia(id);
        
            var entidade = _repository.GetById(id);
        
            var result = await NotificarCliente(entidade);

            if (result == 0) throw new Exception("Erro");
        }
        catch (Exception ex)
        {
            Monitoramento(ex);
        }
       
    }

    private void Monitoramento(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    
    private void CalculoFinanceiro(int id)
    {
        var entidade = _repository.GetById(id);

        entidade.Financeiro *= 10;
        
        _repository.Add(entidade);
    }

    private void CalculoCustodia(int id)
    {
        var entidade = _repository.GetById(id);

        foreach (var custodia in entidade.Custodia)
        {
            if (custodia.Ativo == "PETR4") continue;

            custodia.Valor *= 5;
        }
        
        _repository.Add(entidade);
    }

    private async Task<int> NotificarCliente(PosicaoConsolidada posicaoConsolidada)
    {
        var json = JsonConvert.SerializeObject(posicaoConsolidada);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var url = "https://service/post";
        using var client = new HttpClient();

        var response = await client.PostAsync(url, data);

        if (response.IsSuccessStatusCode) return 1;

        return 0;
    }
}