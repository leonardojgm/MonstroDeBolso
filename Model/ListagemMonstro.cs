using Newtonsoft.Json;

namespace MonstroDeBolso.Model;

public class ListagemMonstro
{    
    [JsonProperty("count")]
    public int Quantidade { get; set; }
    [JsonProperty("next")]
    public string? Proximo { get; set; }

    [JsonProperty("previous")]
    public string? Anterior { get; set; }

    [JsonProperty("results")]
    public List<MonstroSimples>? Monstros { get; set; }

    public override string ToString()
    {
        return $"Quantidade de monstros: {Quantidade}";
    }
}