using System.Runtime;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;

namespace TicTacToe.Core
{
    public abstract class Jogador
    {
        public abstract int nIndexJogador { get; set; }
        public abstract bool bJogadaFinalizada { get; set; }
        public abstract void Jogar(ClassTabuleiro oTabuleiro);

    }
    public partial class ClassTabuleiro : ICloneable
    {
        public List<List<int>> oLinhasTabuleiro { get; set; } 
        public int nTamanho { get; set; }
        public ClassTabuleiro(int nTamanho)
        {
            this.nTamanho = nTamanho;
            this.oLinhasTabuleiro = new List<List<int>>();

            for (int x = 0; x < nTamanho; x++)
            {
                this.oLinhasTabuleiro.Add(GetLinha(nTamanho));
            }
        }

        public ClassTabuleiro()
        { }

        private List<int> GetLinha(int nTamanho)
        {
            var LinhaTabuleiro = new List<int>();

            for (int x = 0; x < nTamanho; x++)
            {
                LinhaTabuleiro.Add(0);
            }

            return LinhaTabuleiro;
        }

        public void Clear()
        {
            this.oLinhasTabuleiro = new List<List<int>>();

            for (int x = 0; x < nTamanho; x++)
            {
                this.oLinhasTabuleiro.Add(GetLinha(this.nTamanho));
            }
        }
    

        public object Clone()
        {
            var Clone = new ClassTabuleiro
            {
                nTamanho = this.nTamanho
            };
            Clone.oLinhasTabuleiro = new List<List<int>>();
            foreach (var Linha in this.oLinhasTabuleiro)
            {
                var A = new List<int>();
                foreach (var Coluna in Linha)
                {
                    A.Add(Coluna);
                }
                Clone.oLinhasTabuleiro.Add(A);
            }

            return Clone;
        }
    }
    public enum Peao
    {
        X = 1,
        O = -1
    }
    public class Coordenada
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }
        public Coordenada(int Lin = 0, int Colun = 0)
        {
            this.Linha = Lin;
            this.Coluna = Colun;
        }

        public Coordenada(string sCoordenada)
        {
            var Posicao = sCoordenada.Split(';').Select(Int32.Parse).ToList();
            this.Linha = Posicao[0];
            this.Coluna = Posicao[1];
        }
    }
}
