using Newtonsoft.Json;

namespace MonstroDeBolso.Model;

public class Mascote
{
    public int Numero { get; set; }
    public string? Nome { get; set; }
    public string? Altura { get; set; }
    public string? Peso { get; set; }
    public int Idade { get; set; }   
    public int Humor { get; set; }
    public int Alimentacao { get; set; }    
    public List<HabilidadeMonstro>? Habilidades { get; set; }

    public override string ToString()
    {
        return @$"
Número: {Numero} 
Nome: {Nome} 
Altura: {Altura} 
Peso: {Peso}";
    }

    public void AlimentarMascote()
    {
        Alimentacao++;
    }
    
    public void BrincarMascote()
    {
        Humor++;
    }

    public string ExibirMascote()
    {
        return @$"
Nome: {Nome} 
Altura: {Altura} 
Peso: {Peso}
Idade: {Peso}
Peso: {Idade} Anos em Mascote Virtual
{Nome} {(Alimentacao != 0 ? $"Está alimentado!" : "Está com fome!")}
{Nome} {(Humor != 0 ? $"Está feliz!" : "Está triste!")}";
    }
}