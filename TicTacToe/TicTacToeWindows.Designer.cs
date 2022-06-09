
using System;
using System.Drawing;
using System.Windows.Forms;
using TicTacToe.MachinePlayer;
using TicTacToe.Core;

namespace TicTacToe
{
    partial class TicTacToe : Form
    {
        
        private System.ComponentModel.IContainer components = null;
        private int nTamanho;
        private int nZeroX;
        private int nZeroY;
        private System.Windows.Forms.Panel AreaTabuleiro;
        private System.Windows.Forms.Button ButtonIniciar;


        private void OnPaint(object sender, PaintEventArgs e)
        {
            DrawGrid(e.Graphics); //Desenha as linhas
        }

        public void DrawGrid(Graphics e) // desanha as linhas do tabuleiro e da proxima peça
        {
            int nIdent = this.AreaTabuleiro.Size.Width / this.nTamanho;

            for (int i = 1; i < this.nTamanho; i++) // verticais
            {
                e.DrawLine(Pens.Black, new Point(nZeroX + (i * nIdent), nZeroY), new Point(nZeroX + (i * nIdent), nZeroY + this.AreaTabuleiro.Size.Height));
            }

            for (int i = 1; i < this.nTamanho; i++) // verticais
            {
                e.DrawLine(Pens.Black, new Point(nZeroX, nZeroY + (i * nIdent)), new Point(nZeroX + this.AreaTabuleiro.Size.Width, nZeroY + (i * nIdent)));
            }

        }

        public void DesenhaTabuleiro(ClassTabuleiro oTabuleiro)
        {
            for(int x = 0; x < oTabuleiro.nTamanho; x++)
            {
                for (int j = 0; j < oTabuleiro.nTamanho; j++)
                {
                    if(oTabuleiro.oLinhasTabuleiro[x][j] == 1)
                    {
                        Coordenada PosicaoPeca = new Coordenada(x, j);
                        PintaX(PosicaoPeca);
                    }
                    else if (oTabuleiro.oLinhasTabuleiro[x][j] == -1)
                    {
                        Coordenada PosicaoPeca = new Coordenada(x, j);
                        PintaO(PosicaoPeca);
                    }
                }
            }
        }

        public void LimparTabuleiro()
        {
            this.AreaTabuleiro.Controls.Clear();
        }

        public void Print(string Texto)
        {
            this.Title.Text = Texto;
        }

        private void PintaX(Coordenada PosicaoPeca)
        {
            PictureBox PecaX = new PictureBox();
            Image oImgX = Image.FromFile("./Imagens/X.png");
            PecaX.Size = new Size(50, 50);
            PecaX.Image = oImgX;

            int nIdent = this.AreaTabuleiro.Size.Width / this.nTamanho;

            int PositionX = nZeroX + PosicaoPeca.Coluna * nIdent + (nIdent - PecaX.Size.Width) / 2;
            int PositionY = nZeroY + PosicaoPeca.Linha * nIdent + (nIdent - PecaX.Size.Height) / 2;
            PecaX.Location = new Point(PositionX, PositionY);

            this.AreaTabuleiro.Controls.Add(PecaX);
        }

        private void PintaO(Coordenada PosicaoPeca)
        {
            PictureBox PecaO = new PictureBox();
            Image oImgO = Image.FromFile("./Imagens/O.png");
            PecaO.Size = new Size(50, 50);
            PecaO.Image = oImgO;

            int nIdent = this.AreaTabuleiro.Size.Width / this.nTamanho;

            int PositionX = nZeroX + PosicaoPeca.Coluna * nIdent + (nIdent - PecaO.Size.Width) / 2;
            int PositionY = nZeroY + PosicaoPeca.Linha * nIdent + (nIdent - PecaO.Size.Height) / 2;
            PecaO.Location = new Point(PositionX, PositionY);

            this.AreaTabuleiro.Controls.Add(PecaO);
        }


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            this.AreaTabuleiro = new System.Windows.Forms.Panel();
            this.ButtonIniciar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Title = new System.Windows.Forms.Label();
            this.EscolhaNivel = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AreaTabuleiro
            // 
            this.AreaTabuleiro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.AreaTabuleiro.Location = new System.Drawing.Point(10, 100);
            this.AreaTabuleiro.Name = "AreaTabuleiro";
            this.AreaTabuleiro.Size = new System.Drawing.Size(200, 200);
            this.AreaTabuleiro.TabIndex = 0;
            this.AreaTabuleiro.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            // 
            // ButtonIniciar
            // 
            this.ButtonIniciar.Location = new System.Drawing.Point(10, 66);
            this.ButtonIniciar.Name = "ButtonIniciar";
            this.ButtonIniciar.Size = new System.Drawing.Size(95, 25);
            this.ButtonIniciar.TabIndex = 1;
            this.ButtonIniciar.Text = "Iniciar";
            this.ButtonIniciar.UseVisualStyleBackColor = true;
            this.ButtonIniciar.Click += new System.EventHandler(this.ButtonIniciar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.Title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 58);
            this.panel1.TabIndex = 3;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Trebuchet MS", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(0, 9);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(55, 26);
            this.Title.TabIndex = 0;
            this.Title.Text = "Title";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EscolhaNivel
            // 
            this.EscolhaNivel.FormattingEnabled = true;
            this.EscolhaNivel.Items.AddRange(new object[] {
            "Facil",
            "Medio",
            "Dificil"});
            this.EscolhaNivel.SelectedIndex = 1;
            this.EscolhaNivel.Location = new System.Drawing.Point(117, 66);
            this.EscolhaNivel.Name = "EscolhaNivel";
            this.EscolhaNivel.Size = new System.Drawing.Size(95, 23);
            this.EscolhaNivel.TabIndex = 4;
            this.EscolhaNivel.SelectedIndexChanged += new System.EventHandler(this.EscolhaNivel_SelectedIndexChanged);
            // 
            // TicTacToe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(224, 311);
            this.Controls.Add(this.EscolhaNivel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ButtonIniciar);
            this.Controls.Add(this.AreaTabuleiro);
            this.Name = "TicTacToe";
            this.Text = "TicTacToe";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Panel panel1;
        private Label Title;
        private ComboBox EscolhaNivel;
    }
}

