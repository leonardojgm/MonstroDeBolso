using MonstroDeBolso.View;

namespace MonstroDeBolso.Controller;

public class MonstroDeBolsoController
{
    public static void ExecutarInicio()
    {
        MonstroDeBolsoView view = new();

        view.ExibirInicio();        
        view.ExibirMenu();
    }
}