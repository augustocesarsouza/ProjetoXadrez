using tabuleiro;
using tabuleiro.Enums;
using tabuleiro.Exceptions;
using System.Collections.Generic;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas = new HashSet<Peca>();
        private HashSet<Peca> Capturadas = new HashSet<Peca>(); //Parecido com List()
        public bool Xeque { get; private set; } //Verifica se a partirda esta em Xeque

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQteMovimento();
            Peca pecaCapturada = Tab.RetirarPeca(destino);//Se tiver uma peça no destino tira ela Meio que captura ela 
            Tab.ColocarPeca(p, destino); // Coloca no lugar Destino
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino); //Rei aqui
            p.DecrementarQteMovimento();
            if (pecaCapturada != null) //Torre aqui Exemplo de Peça
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);//Executa

            if (EstaEmXeque(JogadorAtual)) //Aqui verifica se meu movimento Do rei vai estar em check Por que eu nao posso mover meu Rei para Check
            {//Voce nao pode se alto colocar em Xeque com REI
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("You cannot put yourself in check!");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual))) //Se for Branco meu Adversario é o Preto...
            {//Meu Adversario pode fica em Xeque com minha Jogada
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequemate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }else
            {
                Turno++;
                MudaJogador();
            }
        }

        public void ValidarPosicaoDeOrigem(Posicao pos) //Valida a Origem
        {
            if (Tab.Peca(pos) == null)
            {
                throw new TabuleiroException("there is no piece in the chosen origin position!");
            }
            if (JogadorAtual != Tab.Peca(pos).Cor) //Aqui só pode escolher a Peça se for a mesma do Jogador em questao Cor jogador tem que ser a mesma da PEÇA
            {
                throw new TabuleiroException("The source part chosen is not yours!");
            }
            if (!Tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("there are no possible moves for the chosen parent piece!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Invalid target position!");
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor) //Para separar como dentro da HashSet<Peca> Capturadas Vai ser aleatoria as peças capturadas aqui vamos filtrar elas
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor)); // Basicamente ele ta tirando de dentro de aux as peças que foram capturadas que nao estao mais em jogo
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }else
            {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
            {
                throw new TabuleiroException($"There is no {cor} color king on the board");
            }

            foreach (Peca x in PecasEmJogo(Adversaria(cor))) //Peça adversaria
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna]) //Meu Rei está em Xeque Se der TRUE
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequemate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j]) //Se for movimento Possivel de ser feito pela Peça
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor); //Verifica se ainda está em Xeque
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) //Entrou aqui nao esta em xeque mais
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(linha, coluna).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Branca));
            ColocarNovaPeca('h', 7, new Torre(Tab, Cor.Branca));

            ColocarNovaPeca('a', 8, new Rei(Tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Torre(Tab, Cor.Preta));
        }
    }
}
