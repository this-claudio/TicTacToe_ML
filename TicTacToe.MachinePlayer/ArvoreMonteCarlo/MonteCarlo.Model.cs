using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Core;
using TicTacToe.GameRules;

namespace TicTacToe.MachinePlayer
{
    public class MonteCarloTentativa
    {
        public ClassTabuleiro oTabuleiro { get; set; }
        public List<Coordenada> oPassos { get; set; }
        public int nEstadoJogo { get; set; }
        public int nJogador { get; set; }
        public int ID { get; set; }
        public MonteCarloTentativa(ClassTabuleiro Tabuleiro, int nJogador)
        {
            this.nEstadoJogo = (int)Estado.Indefinido;
            this.oPassos = new List<Coordenada>();
            this.nJogador = nJogador;
            this.ID = (new Random()).Next(0, 100);
            this.oTabuleiro = (ClassTabuleiro)Tabuleiro.Clone();

        }

        public void CheckEstadoJogo()
        {
            Score GameScore = new Score();
            if (nJogador == (int)Peao.X)
            {
                
                if (GameScore.XGanhou(this.oTabuleiro))
                {
                    this.nEstadoJogo = (int)Estado.Vitoria;
                }
                else if (GameScore.OGanhou(this.oTabuleiro))
                {
                    this.nEstadoJogo = (int)Estado.Derrota;
                }
            }
            else
            {
                if (GameScore.OGanhou(this.oTabuleiro))
                {
                    this.nEstadoJogo = (int)Estado.Vitoria;
                }
                else if (GameScore.XGanhou(this.oTabuleiro))
                {
                    this.nEstadoJogo = (int)Estado.Derrota;
                }
            }

            if (this.nEstadoJogo != (int)Estado.Vitoria && this.nEstadoJogo != (int)Estado.Derrota)
            {
                this.nEstadoJogo = (int)Estado.Empate;
                foreach(var Linha in this.oTabuleiro.oLinhasTabuleiro)
                {
                    foreach(var Coluna in Linha)
                    {
                        if (Coluna == 0)
                        {
                            this.nEstadoJogo = (int)Estado.Indefinido;
                        }
                    }
                }
            }
        }
    }

    enum Estado
    {
        Vitoria = 3,
        Derrota = 4,
        Indefinido = 9,
        Empate = 5
    }

}
