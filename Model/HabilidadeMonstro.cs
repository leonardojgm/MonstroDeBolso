using Newtonsoft.Json;

namespace MonstroDeBolso.Model;

public class HabilidadeMonstro
{
    [JsonProperty("ability")]
    public Habilidade? Habilidade { get; set; }

    public override string ToString()
    {
        return $"{Habilidade}";
    }
}