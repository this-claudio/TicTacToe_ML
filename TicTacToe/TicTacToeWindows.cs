using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToe.Core;
using TicTacToe.MachinePlayer;
using TicTacToe.GameRules;
using TicTacToe.HumanPlayer;
using System.Reflection;

namespace TicTacToe
{
    public partial class TicTacToe : Form
    {
        public Jogador JogadorX { get; set; }
        public Jogador JogadorO { get; set; }
        Score GameScore { get; set; }
        Timer AtualizarInterface { get; set; }
        public int nTurno { get; set; }
        public ClassTabuleiro Tab3x3 { get; set; }

        public TicTacToe()
        {
            this.nTamanho = 3;
            this.nZeroX = 0;
            this.nZeroY = 0;
            InitializeComponent();
            this.EscolhaNivel.SelectedItem = "Medio";
            this.Title.Text = "Jogo Pausado";
            InitializeGame();
        }

        private void StartTimer()
        {
            if(AtualizarInterface == null)
            {
                AtualizarInterface = new Timer();
                AtualizarInterface.Interval = 200;
                AtualizarInterface.Enabled = true;
                AtualizarInterface.Tick += AtualizarInterface_Tick;
            }
            AtualizarInterface.Start();
            this.ButtonIniciar.Text = "Reiniciar";
        }

        private void AtualizarInterface_Tick(object sender, EventArgs e)
        {
            GameRun();
        }

        private void GameRun()
        {
            try
            {
                
                if (nTurno == (int)Peao.X)
                {
                    Print("Vez do Jogador X");
                    if (JogadorX.bJogadaFinalizada)
                    {
                        nTurno = nTurno * -1;
                        JogadorX.bJogadaFinalizada = false;
                    }
                    else JogadorX.Jogar(Tab3x3);
                }
                else if (nTurno == (int)Peao.O)
                {
                    Print("Vez do Jogador O");
                    if (JogadorO.bJogadaFinalizada)
                    {
                        nTurno = nTurno * -1;
                        JogadorO.bJogadaFinalizada = false;
                    }
                    else JogadorO.Jogar(Tab3x3);
                }
            }
            catch (Exception e) { }
            
            DesenhaTabuleiro(Tab3x3);

            if (GameScore.OGanhou(Tab3x3) || GameScore.XGanhou(Tab3x3) || GameScore.Empate(Tab3x3))
            {
                if (GameScore.OGanhou(Tab3x3)) Print("Vitoria de O");
                if (GameScore.XGanhou(Tab3x3)) Print("Vitoria de X");
                if (GameScore.Empate(Tab3x3)) Print("Empate");

                this.ButtonIniciar.Text = "Iniciar";
                Tab3x3.Clear();
                AtualizarInterface.Stop();
            }


        }

        private void InitializeGame()
        {
            Tab3x3 = new ClassTabuleiro(this.nTamanho);
            GameScore = new Score();

            string Assembly = "ClassHumanPlayer";

            JogadorO = new ClassHumanPlayer((int)Peao.O, ref this.AreaTabuleiro);
            JogadorX = new ClassMachinePlayer((int)Peao.X, GetDificuldade());
            //JogadorO = new ClassMachinePlayer((int)Peao.O, GetDificuldade());
        }

        private Dificuldade GetDificuldade()
        {
            string Nivel = this.EscolhaNivel.SelectedItem.ToString();
            switch (Nivel)
            {
                case "Facil": return Dificuldade.Facil; break;
                case "Medio": return Dificuldade.Medio; break;
                case "Dificil": return Dificuldade.Dificil; break;
                default: return Dificuldade.Medio; break;
            }
        }

        #region EVENTOS

        private void ButtonIniciar_Click(object sender, EventArgs e)
        {
            nTurno = (int)Peao.O;
            Tab3x3.Clear();

            //Tab3x3.JogarO(1, 1);
            //Tab3x3.JogarX(0, 0);
            LimparTabuleiro();
            StartTimer();
        }


        #endregion

        private void EscolhaNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                nTurno = (int)Peao.O;
                Tab3x3.Clear();
                LimparTabuleiro();
                InitializeGame();
            }
            catch(Exception error) { }
        }
    }
}
