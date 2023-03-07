using tabuleiro;
namespace xadrez
{
    internal class PosicaoXadrez
    {
        public int Linha { get; set;}
        public char Coluna { get; set; } //No xadrez a Coluna "AS LETRAS" e as Linha "Os numeros"

        public PosicaoXadrez(int linha, char coluna)
        {
            Linha = linha;
            Coluna = coluna;
            
        }

        public Posicao ToPosicao() //Para conver uma posição normal aqui no c# Usando as Coluna Que são LETRAS no Tabuleiro e Linhas que são Numeros
        {
            return new Posicao(8 - Linha, Coluna - 'a'); //Aqui ele ta usando os codigo da "tabela ASCII" no caso 'a'=97 se eu quiser Coluna 'd'=100 entao 97-100=3
        }

        public override string ToString()
        {
            return $"{Coluna}{Linha}";
        }
    }
}
