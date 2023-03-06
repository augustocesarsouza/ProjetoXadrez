using tabuleiro;
namespace Views
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.peca(i, j) == null) //Por padrao null todos 
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write($"{tab.peca(i, j)} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
