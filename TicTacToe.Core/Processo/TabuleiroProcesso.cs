using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Core
{
    public partial class ClassTabuleiro
    {
        public void JogarO(int Linha, int Coluna)
        {
            try
            {
                if (this.oLinhasTabuleiro[Linha][Coluna] == 0)
                    this.oLinhasTabuleiro[Linha][Coluna] = (int)Peao.O;
                else throw new ArgumentException("A posição do tabuleira já está ocupada.");
            }
            catch (ArgumentException e)
            {
            }
        }

        public void JogarX(int Linha, int Coluna)
        {
            try
            {
                if (this.oLinhasTabuleiro[Linha][Coluna] == 0)
                    this.oLinhasTabuleiro[Linha][Coluna] = (int)Peao.X;
                else throw new ArgumentException("A posição do tabuleira já está ocupada.");
            }
            catch (ArgumentException e)
            {
            }
        }
    }
}
