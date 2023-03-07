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

        private bool PodeMover(Posicao pos) //Verifica se Rei pode mover para tal Posição
        {
            Peca p = Tab.Peca(pos);
            return p == null || p.Cor != Cor; // <- Se for uma Peça adiversaria / Ali se for null é por que a posicao que veio para função esta vazia entao é true pode mover
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // Acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMover(pos)) // aqui Torre quanto tiver espaço para percorrer com a torre vai quando chegar no limite para "PosicaoValida()" Outro se sabe
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha - 1;
            }

            // Abixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.PosicaoValida(pos) && PodeMover(pos)) // aqui Torre quanto tiver espaço para percorrer com a torre vai quando chegar no limite para "PosicaoValida()" Outro se sabe
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Linha = pos.Linha + 1;
            }
            // Direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos)) // aqui Torre quanto tiver espaço para percorrer com a torre vai quando chegar no limite para "PosicaoValida()" Outro se sabe
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna = pos.Coluna + 1;
            }

            // Esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos)) // aqui Torre quanto tiver espaço para percorrer com a torre vai quando chegar no limite para "PosicaoValida()" Outro se sabe
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.Coluna = pos.Coluna - 1;
            }



            return mat;
        }
    }
}
