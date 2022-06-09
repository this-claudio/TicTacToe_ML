using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Core;

namespace TicTacToe.GameRules
{
    public class Score
    {
        public bool XGanhou(ClassTabuleiro oTabuleiro)
        {
            //Analisa Vitoria em linha
            foreach (var Linha in oTabuleiro.oLinhasTabuleiro)
            {
                if (Linha.Sum() == oTabuleiro.nTamanho * (int)Peao.X)
                {
                    return true;
                }
            }
            //Analisa vitoria em coluna
            for (int i = 0; i < oTabuleiro.nTamanho; i++)
            {
                List<int> nColunaPosicaoI = new List<int>();

                foreach (var Linha in oTabuleiro.oLinhasTabuleiro)
                {
                    nColunaPosicaoI.Add(Linha[i]);
                }

                if (nColunaPosicaoI.Sum() == oTabuleiro.nTamanho * (int)Peao.X)
                {
                    return true;
                }
            }
            //Analisa Vitoria em Diagonal
            List<int> nDiagonalCimaBaixoPosicaoI = new List<int>();// diagonal - \
            List<int> nDiagonalBaixoCimaPosicaoI = new List<int>();// diagonal - /
            for (int i = 0; i < oTabuleiro.nTamanho; i++)
            {
                nDiagonalCimaBaixoPosicaoI.Add(oTabuleiro.oLinhasTabuleiro[i][i]);
            }

            for (int i = 0; i < oTabuleiro.nTamanho; i++)
            {
                int PosicaoColuna = (oTabuleiro.nTamanho - 1) - i;
                nDiagonalBaixoCimaPosicaoI.Add(oTabuleiro.oLinhasTabuleiro[i][PosicaoColuna]);
            }

            if (nDiagonalCimaBaixoPosicaoI.Sum() == oTabuleiro.nTamanho * (int)Peao.X)
            {
                return true;
            }

            if (nDiagonalBaixoCimaPosicaoI.Sum() == oTabuleiro.nTamanho * (int)Peao.X)
            {
                return true;
            }

            return false;
        }

        public bool OGanhou(ClassTabuleiro oTabuleiro)
        {
            //Analisa Vitoria em linha
            foreach (var Linha in oTabuleiro.oLinhasTabuleiro)
            {
                if (Linha.Sum() == oTabuleiro.nTamanho * (int)Peao.O)
                {
                    return true;
                }
            }
            //Analisa vitoria em coluna
            for (int i = 0; i < oTabuleiro.nTamanho; i++)
            {
                List<int> nColunaPosicaoI = new List<int>();

                foreach (var Linha in oTabuleiro.oLinhasTabuleiro)
                {
                    nColunaPosicaoI.Add(Linha[i]);
                }

                if (nColunaPosicaoI.Sum() == oTabuleiro.nTamanho * (int)Peao.O)
                {
                    return true;
                }
            }
            //Analisa Vitoria em Diagonal
            List<int> nDiagonalCimaBaixoPosicaoI = new List<int>(); // diagonal - \
            List<int> nDiagonalBaixoCimaPosicaoI = new List<int>(); // diagonal - /
            for (int i = 0; i < oTabuleiro.nTamanho; i++)
            {
                nDiagonalCimaBaixoPosicaoI.Add(oTabuleiro.oLinhasTabuleiro[i][i]);
            }

            for (int i = 0; i < oTabuleiro.nTamanho; i++)
            {
                int PosicaoColuna = (oTabuleiro.nTamanho - 1) - i;
                nDiagonalBaixoCimaPosicaoI.Add(oTabuleiro.oLinhasTabuleiro[i][PosicaoColuna]);
            }

            if (nDiagonalCimaBaixoPosicaoI.Sum() == oTabuleiro.nTamanho * (int)Peao.O)
            {
                return true;
            }

            if (nDiagonalBaixoCimaPosicaoI.Sum() == oTabuleiro.nTamanho * (int)Peao.O)
            {
                return true;
            }

            return false;
        }

        public bool Empate(ClassTabuleiro tab3x3)
        {
            bool Empate = true;
            foreach (var Linha in tab3x3.oLinhasTabuleiro)
            {
                foreach (var Coluna in Linha)
                {
                    if (Coluna == 0)
                    {
                        Empate = false;
                    }
                }
            }
            return Empate;
        }
    }
}
