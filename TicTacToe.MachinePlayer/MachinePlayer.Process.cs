using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Core;

namespace TicTacToe.MachinePlayer
{
    public class ClassMachinePlayer : Jogador
    {
        public override int nIndexJogador { get; set; }
        private MonteCarloProcess CerebroMaquina { get; set; }
        public override bool bJogadaFinalizada { get; set; }

        public ClassMachinePlayer(int nJogador, Dificuldade Dificuldade)
        {
            CerebroMaquina = new MonteCarloProcess(Dificuldade, nJogador);
            this.bJogadaFinalizada = false;
        }

        public override void Jogar(ClassTabuleiro oTabuleiro)
        {

            if (CerebroMaquina.bJogadaFinalizada != true)
            {
                CerebroMaquina.Jogar(oTabuleiro);
            }
            else
            {
                this.bJogadaFinalizada = true;
                this.CerebroMaquina.bJogadaFinalizada = false;
            }

        }




    }


}
