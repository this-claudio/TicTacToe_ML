using System;
using System.Windows.Forms;
using TicTacToe.Core;

namespace TicTacToe.HumanPlayer
{
    public class ClassHumanPlayer : Jogador
    {
        public override int nIndexJogador { get; set; }
        public bool bHabilitaJogada { get; set; }
        public override bool bJogadaFinalizada { get; set; }
        private Panel TabuleiroArea { get; set; }
        public ClassTabuleiro oTabuleiro { get; set; }

        public ClassHumanPlayer(int nIndexJogador, ref Panel TabuleiroArea)
        {
            this.nIndexJogador = nIndexJogador;
            this.bHabilitaJogada = false;
            this.bJogadaFinalizada = false;
            this.TabuleiroArea = TabuleiroArea;
            TabuleiroArea.MouseClick += OnClick_ColocaPeca;
        }
        /// <summary>
        /// Metodo que faz a jogada, e retorna se a jogada foi realizada com sucesso;
        /// </summary>
        /// <param name="oTabuleiro"></param>
        /// <returns></returns>
        public override void Jogar(ClassTabuleiro oTabuleiro)
        {
            this.oTabuleiro = oTabuleiro;
            this.bHabilitaJogada = true;
        }

        private void OnClick_ColocaPeca(object sender, MouseEventArgs e)
        {
            if (this.bHabilitaJogada)
            {
                MouseEventArgs Click = (MouseEventArgs)e;
                var Position = GetTabuleiroPosicao(Click);
                try
                {
                    if (nIndexJogador == (int)Peao.X)
                        oTabuleiro.JogarX(Position.Linha, Position.Coluna);
                    else oTabuleiro.JogarO(Position.Linha, Position.Coluna);

                    this.bHabilitaJogada = false;
                    this.bJogadaFinalizada = true;
                }
                catch(Exception Erro)
                {
                    MessageBox.Show(Erro.Message);
                }
            }

        }

        private Coordenada GetTabuleiroPosicao(MouseEventArgs click)
        {
            double nTamanhoQuadradinhosTabuleiro = this.TabuleiroArea.Size.Width / this.oTabuleiro.nTamanho;
            double MouseXPosition = Convert.ToDouble(click.Location.X);
            double MouseYPosition = Convert.ToDouble(click.Location.Y);

            var Coluna = Convert.ToInt32((Math.Ceiling(MouseXPosition / nTamanhoQuadradinhosTabuleiro))) - 1;
            var Linha = Convert.ToInt32((Math.Ceiling(MouseYPosition / nTamanhoQuadradinhosTabuleiro))) - 1;

            return new Coordenada(Linha, Coluna);
        }
    }
}
