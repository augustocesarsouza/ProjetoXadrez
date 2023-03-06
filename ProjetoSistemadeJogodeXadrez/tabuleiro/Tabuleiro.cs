using ProjetoSistemadeJogodeXadrez.tabuleiro;

namespace tabuleiro
{
    internal class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas; //Só tabuleiro meche nela

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];//Quantas Linha e Colunas esse Tabuleiro vai ter
        }
    }
}
