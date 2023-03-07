using xadrez;
using tabuleiro;
using Views;
using tabuleiro.Enums;
using tabuleiro.Exceptions;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez pos = new PosicaoXadrez(7, 'c');

            Console.WriteLine(pos);

            Console.WriteLine(pos.ToPosicao());

            Console.ReadLine();
        }
    }
}