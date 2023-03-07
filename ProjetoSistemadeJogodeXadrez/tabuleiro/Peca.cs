﻿using tabuleiro;
using tabuleiro.Enums;

namespace tabuleiro
{   //Classe peça é uma Classe Generica
    internal abstract class Peca
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

        public abstract bool[,] MovimentosPossiveis(); //Matriz para saber para onde a peça pode ir ou não / Depende da peça / True para onde pode ir
        //Por a Classe Peca ser Muito Generica não tem como ele coloca uma logica para ver onde a peca vai agora as peças por si só vão fazer essa logica

        public void IncrementarQteMovimento()
        {
            QteMovimentos++;
        }
    }
}
