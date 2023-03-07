using tabuleiro;
using tabuleiro.Enums;

namespace xadrez
{
    internal class Torre : Peca //Relação é um é uma peca e mais algumas coisa
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
