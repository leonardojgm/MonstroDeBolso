using AutoMapper;
using MonstroDeBolso.Model;
using MonstroDeBolso.Service;

namespace MonstroDeBolso.View;

public class MonstroDeBolsoView
{
    string? usuario = string.Empty;
    readonly List<Mascote> mascotesAdotados = [];

    public void ExibirInicio()
    {
        ExibirNomeAplicacao();

        usuario = ExibirPergunta("Qual o seu nome?");

        ExibirMensagem($"Bem vindo {usuario}!");
    }
    
    public void ExibirMenu()
    {
        string? opcao = string.Empty;    

        while (opcao != "0")
        {
            ExibirTitulo("MENU PRINCIPAL", false);
            ExibirMensagem("1 - Listagem de Monstros");
            ExibirMensagem("2 - Informações de um Monstro");
            ExibirMensagem("3 - Adote um Monstro");
            ExibirMensagem("4 - Exibir Mascotes Adotados");
            ExibirMensagem("5 - Administrar Mascotes Adotados");
            ExibirMensagem("0 - Sair");

            opcao = ExibirPergunta($"{usuario}, digite qual a opção desejada? ");

            switch (opcao)
            {
                case "1":
                    ExibirListagemMonstro();
                    break;
                case "2":
                    ExibirInformacoesMonstro();
                    break;
                case "3":
                    ExibirAdotarMonstro();
                    break;
                case "4":
                    ExibirMascotesAdotados();
                    break;
                case "5":
                    ExibirSelecionarAdministrarMascotesAdotados();
                    break;
                case "0":
                    Sair();
                    return;
                default:
                    ExibirMensagem("Opção inválida!");
                    break;
            }

            opcao = string.Empty;  
        }
    }

    public void ExibirSelecionarAdministrarMascotesAdotados()
    {
        ExibirTitulo("SELEÇÃO DE MASCOTE", true);

        if (!mascotesAdotados.Any())
        {
            ExibirMensagem("Nenhum Monstro Adotado!");
            PularLinha();
            ExibirEncerrarTela();

            return;
        }

        Mascote? mascote = null;

        while (mascote == null)
        {
            string? pesquisa = ExibirPergunta($"{usuario}, digite o nome ou número do Mascote:");
        
            mascote = mascotesAdotados.FirstOrDefault(m => m.Numero.ToString() == pesquisa || m.Nome == pesquisa);

            if (mascote != null)
            {
                ExibirMascote(mascote.Numero);
            
                string? confirmaAdocao = string.Empty;

                while (confirmaAdocao != "s" && confirmaAdocao != "n" && mascote != null)
                {
                    confirmaAdocao = ExibirPergunta($"{usuario}, deseja administrar o Mascote {mascote.Nome}? (s/n)")?.ToLower();

                    if (confirmaAdocao == "s")
                    {
                        ExibirAdministrarMonstrosAdotados(mascote.Numero);
                    }
                    else if (confirmaAdocao == "n")
                    {
                        mascote = null;
                    }
                }
            }
            else
            {
                ExibirMensagem("Não foi possível obter as informações do Mascote!");
            }
        }

        PularLinha();
        ExibirEncerrarTela();
    }

    public void ExibirAdministrarMonstrosAdotados(int numeroMascote)
    {
        Mascote mascote = mascotesAdotados.First(m => m.Numero == numeroMascote);
        string? opcao = string.Empty;    

        while (opcao != "0")
        {
            ExibirTitulo("MENU ADMINISTRAR MONSTROS", true);
            ExibirMensagem($"1 - Saber como {mascote.Nome} está");
            ExibirMensagem($"2 - Alimentar o {mascote.Nome}");
            ExibirMensagem($"3 - Brincar com {mascote.Nome}");
            ExibirMensagem("0 - Voltar");

            opcao = ExibirPergunta($"{usuario}, digite qual a opção desejada? ");

            switch (opcao)
            {
                case "1":
                    ExibirMascote(numeroMascote, true);
                    break;
                case "2":
                    AlimentarMascote(numeroMascote);
                    break;
                case "3":
                    BrincarMascote(numeroMascote);
                    break;
                case "0":
                    return;
                default:
                    ExibirMensagem("Opção inválida!");
                    break;
            }

            opcao = string.Empty;  
        }

        PularLinha();
        ExibirEncerrarTela();
    }
    
    private void ExibirMascote(int numeroMascote, bool encerraTela = false)
    {
        ExibirMensagem(mascotesAdotados.First(m => m.Numero == numeroMascote).ExibirMascote());        
        PularLinha();

        if (encerraTela)
        {
            ExibirEncerrarTela();
        }
    }

    private void AlimentarMascote(int numeroMascote)
    {
        PularLinha();

        mascotesAdotados.First(m => m.Numero == numeroMascote).AlimentarMascote();

        ExibirMensagem("(=^w^=)");
        ExibirMensagem("Mascote alimentado!");
        PularLinha();
        ExibirEncerrarTela();
    }

    private void BrincarMascote(int numeroMascote)
    {
        PularLinha();
        
        mascotesAdotados.First(m => m.Numero == numeroMascote).BrincarMascote();

        ExibirMensagem("(=^w^=)");
        ExibirMensagem("Mascote entretido!");
        PularLinha();
        ExibirEncerrarTela();
    }

    private void ExibirListagemMonstro(string? url = null)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            ExibirTitulo("LISTAGEM DE MONSTROS", true);
        }

        PokeAPIService service = new();
        ListagemMonstro? listagemMonstro = service.ObterListagemMonstro(url);

        if (listagemMonstro != null)
        {
            if (string.IsNullOrWhiteSpace(url)) ExibirMensagem($"{listagemMonstro}");
            
            if (listagemMonstro.Monstros != null)
            {
                foreach (MonstroSimples monstro in listagemMonstro.Monstros)
                {
                    ExibirMensagem($"{monstro}");
                } 
                
                if (!string.IsNullOrWhiteSpace(listagemMonstro.Proximo))
                {
                    string? exibeProximo = string.Empty;

                    while (exibeProximo != "s" && exibeProximo != "n")
                    {
                        exibeProximo = ExibirPergunta($"{usuario}, deseja exibir os proximos monstros? (s/n)")?.ToLower();

                        switch (exibeProximo)
                        {
                            case "s":
                                PularLinha();
                                ExibirListagemMonstro(listagemMonstro.Proximo);
                                break;
                            case "n":
                                break;
                            default:
                                ExibirMensagem("Opção inválida!");
                                break;
                        }
                    }
                }
            }
            else
            {
                ExibirMensagem("A listagem de Monstros estava vazia!");
            }
        }
        else
        {
            ExibirMensagem("Não foi possível obter a listagem de Monstros!");
        }

        if (string.IsNullOrWhiteSpace(url))
        {
            ExibirEncerrarTela();
        }
    }

    private void ExibirInformacoesMonstro()
    {
        ExibirTitulo("INFORMAÇÕES DE UM MONSTRO", true);

        Monstro? monstro = null;

        while (monstro == null)
        {      
            try
            {
                string? pesquisa = ExibirPergunta($"{usuario}, digite o nome ou número do Monstro:");
                PokeAPIService service = new();

                monstro = service.ObterMonstro(pesquisa);

                if (monstro != null)
                {
                    ExibirMonstro(monstro);
                }
                else
                {
                    ExibirMensagem("Não foi possível obter as informações do Monstro!");
                }
            }
            catch
            {
                ExibirMensagem("Não foi possível obter as informações do Monstro!");
            }
        }

        PularLinha();
        ExibirEncerrarTela();
    }

    private void ExibirAdotarMonstro()
    {
        ExibirTitulo("ADOTAR UM MONSTRO", true);

        Monstro? monstro = null;

        while (monstro == null)
        {
            try
            {
                string? pesquisa = ExibirPergunta($"{usuario}, digite o nome ou número do Monstro que deseja adotar:");
                PokeAPIService service = new();

                monstro = service.ObterMonstro(pesquisa);

                if (monstro != null)
                {
                    string? desejaVerInformacoes = string.Empty;

                    while (desejaVerInformacoes != "s" && desejaVerInformacoes != "n" && monstro != null)
                    {
                        desejaVerInformacoes = ExibirPergunta($"{usuario}, deseja exibir informações sobre o Monstro? (s/n)")?.ToLower();

                        if (desejaVerInformacoes == "s")
                        {
                            ExibirMonstro(monstro);
                        }
                    }
                    
                    string? confirmaAdocao = string.Empty;

                    while (confirmaAdocao != "s" && confirmaAdocao != "n" && monstro != null)
                    {
                        confirmaAdocao = ExibirPergunta($"{usuario}, deseja confirmar a adoção do Monstro {monstro.Nome}? (s/n)")?.ToLower();

                        if (confirmaAdocao == "s")
                        {
                            Mapper.CreateMap<Monstro, Mascote>();

                            Mascote mascote = Mapper.Map<Monstro, Mascote>(monstro);

                            mascotesAdotados.Add(mascote);

                            ExibirMensagem($"{usuario} adotou o Mascote {mascote.Nome}!");
                            ExibirOvoChocando();
                        }
                    }
                }
                else
                {
                    ExibirMensagem("Não foi possível obter as informações do Monstro!");
                }
            }
            catch
            {
                ExibirMensagem("Não foi possível obter as informações do Monstro!");
            }
        }

        PularLinha();
        ExibirEncerrarTela();
    }

    private void ExibirMascotesAdotados()
    {
        ExibirTitulo("MASCOTES ADOTADOS", true);

        if (mascotesAdotados.Any())
        {
            foreach (Mascote? mascote in mascotesAdotados)
            {
                ExibirMensagem($"{mascote}");
            } 
        }
        else
        {
            ExibirMensagem("Nenhum Monstro foi adotado!");
        }

        PularLinha();
        ExibirEncerrarTela();
    }

    private static void ExibirMonstro(Monstro monstro)
    {
        ExibirMensagem($"{monstro}");

        if (monstro.Habilidades != null && monstro.Habilidades.Any())
        {
            ExibirMensagem($"Habilidades:");

            foreach (HabilidadeMonstro habilidade in monstro.Habilidades)
            {
                ExibirMensagem($"- {habilidade}");
            }
        }
    }
    
    private static void Sair()
    {
        ExibirMensagem("Obrigado por usar o Monstro de Bolso!");
    }

    private static void ExibirNomeAplicacao()
    {
        LimparTela();
        ExibirMensagem(@$"
    █████████████████████████████████████████████████████████████████████████████████████████
    █▄─▀█▀─▄█─▄▄─█▄─▀█▄─▄█─▄▄▄▄█─▄─▄─█▄─▄▄▀█─▄▄─███▄─▄▄▀█▄─▄▄─███▄─▄─▀█─▄▄─█▄─▄███─▄▄▄▄█─▄▄─█
    ██─█▄█─██─██─██─█▄▀─██▄▄▄▄─███─████─▄─▄█─██─████─██─██─▄█▀████─▄─▀█─██─██─██▀█▄▄▄▄─█─██─█
    ▀▄▄▄▀▄▄▄▀▄▄▄▄▀▄▄▄▀▀▄▄▀▄▄▄▄▄▀▀▄▄▄▀▀▄▄▀▄▄▀▄▄▄▄▀▀▀▄▄▄▄▀▀▄▄▄▄▄▀▀▀▄▄▄▄▀▀▄▄▄▄▀▄▄▄▄▄▀▄▄▄▄▄▀▄▄▄▄▀
    ");
    }

    private static void ExibirOvoChocando()
    {
        ExibirMensagem($"O ovo está chocando!");
        ExibirMensagem(@$"
            ████████                                  
            ██        ██                                
        ██▒▒▒▒        ██                              
        ██▒▒▒▒▒▒      ▒▒▒▒██                            
        ██▒▒▒▒▒▒      ▒▒▒▒██                            
    ██  ▒▒▒▒        ▒▒▒▒▒▒██                          
    ██                ▒▒▒▒██                          
    ██▒▒      ▒▒▒▒▒▒          ██                        
    ██      ▒▒▒▒▒▒▒▒▒▒        ██                        
    ██      ▒▒▒▒▒▒▒▒▒▒    ▒▒▒▒██                        
    ██▒▒▒▒  ▒▒▒▒▒▒▒▒▒▒  ▒▒▒▒▒▒██                        
    ██▒▒▒▒  ▒▒▒▒▒▒    ▒▒▒▒██                          
    ██▒▒▒▒            ▒▒▒▒██                          
        ██▒▒              ██                            
        ████        ████                              
            ████████            
    ");
    }

    private static void ExibirTitulo(string mensagem, bool limpaTela = true)
    {
        if (limpaTela) 
        {
            LimparTela();
        }

        ExibirMensagem($"************************************");
        ExibirMensagem(mensagem);
        ExibirMensagem($"************************************");
        PularLinha();
    }

    private static void ExibirEncerrarTela()
    {
        ExibirPergunta($"Pressione uma tecla para continuar...");
        LimparTela();
    }

    private static void ExibirMensagem(string mensagem)
    {
        Console.WriteLine($"{mensagem}");
    }

    private static string? ExibirPergunta(string mensagem)
    {
        Console.Write($"{mensagem} ");   

        return Console.ReadLine();
    }

    private static void PularLinha()
    {
        Console.WriteLine();
    }

    static void LimparTela()
    {
        Console.Clear();
    }
}