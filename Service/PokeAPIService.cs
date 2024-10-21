using System.Net;
using MonstroDeBolso.Model;
using Newtonsoft.Json;
using RestSharp;

namespace MonstroDeBolso.Service;

public class PokeAPIService
{
    readonly string URL_DA_API = "https://pokeapi.co/api/v2/pokemon";

    public ListagemMonstro? ObterListagemMonstro(string? url = null)
    {
        RestClient client = new($"{url ??URL_DA_API}");
        RestRequest request = new($"", Method.Get);
        RestResponse restResponse = client.Execute(request);
        ListagemMonstro? listagemMonstro = null;

        if (restResponse.StatusCode == HttpStatusCode.OK && restResponse.Content != null)
        {
            listagemMonstro = JsonConvert.DeserializeObject<ListagemMonstro>(restResponse.Content);
        }
        else
        {
            throw new Exception("Ocorreu um falha na consulta da PokeAPI!");
        }

        return listagemMonstro;
    }

    public Monstro? ObterMonstro(string? pesquisa)
    {
        RestClient client = new($"{URL_DA_API}");
        RestRequest request = new($"{pesquisa}", Method.Get);
        RestResponse restResponse = client.Execute(request);
        Monstro? monstro = null;

        if (restResponse.StatusCode == HttpStatusCode.OK && restResponse.Content != null)
        {
            monstro = JsonConvert.DeserializeObject<Monstro>(restResponse.Content);       
        }
        else
        {
            throw new Exception("Ocorreu um falha na consulta da PokeAPI!");
        }

        return monstro;
    }
}