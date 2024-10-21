using Newtonsoft.Json;

namespace MonstroDeBolso.Model;

public class MonstroSimples
{
    [JsonProperty("name")]
    public string? Nome { get; set; }

    [JsonProperty("url")]
    public string? Url { get; set; }

    public override string ToString()
    {
        return $"{Url?.Split('/')[6]}. {Nome}";
    }
}