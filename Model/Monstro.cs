using Newtonsoft.Json;

namespace MonstroDeBolso.Model;

public class Monstro
{
    [JsonProperty("id")]
    public int Numero { get; set; }
    [JsonProperty("name")]
    public string? Nome { get; set; }
    [JsonProperty("height")]
    public string? Altura { get; set; }
    [JsonProperty("weight")]
    public string? Peso { get; set; }

    [JsonProperty("abilities")]
    public List<HabilidadeMonstro>? Habilidades { get; set; }

    public override string ToString()
    {
        return @$"
Número: {Numero} 
Nome: {Nome} 
Altura: {Altura} 
Peso: {Peso}";
    }
}