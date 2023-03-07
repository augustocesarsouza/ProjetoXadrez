using tabuleiro;
using tabuleiro.Enums;

namespace tabuleiro
{   //Classe peça é uma Classe Generica
    internal class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            Posicao = null;
            Tab = tab;
            Cor = cor;
            QteMovimentos = 0; //Quando ela acaba de ser criada ela tem 0 Movimento
        }
    }
}
