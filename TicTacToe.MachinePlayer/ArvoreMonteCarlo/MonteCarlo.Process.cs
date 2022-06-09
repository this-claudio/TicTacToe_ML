using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Core;

namespace TicTacToe.MachinePlayer
{
    public class MonteCarloProcess
    {
        int nDiversidade { get; set; }
        int nProfundidade { get; set; }
        public bool bJogadaFinalizada { get; set; }
        public bool bProcessando { get; set; }
        int nJogador { get; set; }
        public MonteCarloProcess(Dificuldade Dificuldade, int nJogador)
        {
            SetDificuldade(Dificuldade);
            this.nJogador = nJogador;
            this.bProcessando = false;
            this.bJogadaFinalizada = false;
        }
        protected void SetDificuldade(Dificuldade dificuldade)
        {
            switch (dificuldade)
            {
                case Dificuldade.Facil:
                    {
                        this.nDiversidade = 1000;
                        this.nProfundidade = 9;
                        break;
                    }
                case Dificuldade.Medio:
                    {
                        this.nDiversidade = 3000;
                        this.nProfundidade = 9;
                        break;
                    }
                case Dificuldade.Dificil:
                    {
                        this.nDiversidade = 30000;
                        this.nProfundidade = 9;
                        break;
                    }
            }
        }

        public void Jogar(ClassTabuleiro oTabuleiro)
        {
            if (this.bProcessando != true)
            {
                this.bProcessando = true;
                ProcessaJogada(oTabuleiro);
            }
        }

        public void ProcessaJogada(ClassTabuleiro oTabuleiro)
        {
            int nTurnoSimulacao = nJogador;
            List<MonteCarloTentativa> PopulacaoTentativas = new List<MonteCarloTentativa>();
            for (int x = 0; x < nDiversidade; x++)
            {
                PopulacaoTentativas.Add(new MonteCarloTentativa(oTabuleiro, nJogador));
            }


            for (int x = 0; x < nProfundidade; x++)
            {
                foreach (var Tentativa in PopulacaoTentativas)
                {
                    if (Tentativa.nEstadoJogo == (int)Estado.Indefinido)
                    {
                        FazerJogada(nTurnoSimulacao, Tentativa);
                        Tentativa.CheckEstadoJogo();
                    }

                }
                nTurnoSimulacao = nTurnoSimulacao * -1;
            }

            
            var ProximoPassoChancesAltasVitoria = GetJogadaChancesAltasVitoria(PopulacaoTentativas);
            if (ProximoPassoChancesAltasVitoria != null)
            {
                this.GravarJogada(ProximoPassoChancesAltasVitoria, oTabuleiro);
                
            }
            else
            {
                var ProximoPassoVitoria = GetJogadaChancesVitoria(PopulacaoTentativas);
                if(ProximoPassoVitoria != null)
                {
                    this.GravarJogada(ProximoPassoVitoria, oTabuleiro);
                }
                else
                {
                    var ProximoPassoEmpate = GetJogadaChancesEmpates(PopulacaoTentativas);
                    if (ProximoPassoEmpate != null)
                    {
                        this.GravarJogada(ProximoPassoEmpate, oTabuleiro);
                    }
                    else
                    {
                        var ProximoPassoAleatorio = GetJogadaChancesAleatoria(oTabuleiro);
                        if (ProximoPassoAleatorio != null)
                        {
                            this.GravarJogada(ProximoPassoAleatorio, oTabuleiro);
                        }
                    }

                }

            }


        }
        private void FazerJogada(int nJogadorDaVez, MonteCarloTentativa Attempt)
        {
            List<Coordenada> PosicoesLivre = GetPosicoesLivres(Attempt.oTabuleiro);

            if (PosicoesLivre.Count > 0)
            {
                Random ProximaJogadaRandom = new Random();
                int x = ProximaJogadaRandom.Next(0, (PosicoesLivre.Count));
                Attempt.oTabuleiro.oLinhasTabuleiro[PosicoesLivre[x].Linha][PosicoesLivre[x].Coluna] = nJogadorDaVez;

                Attempt.oPassos.Add(PosicoesLivre[x]);
            }
            else
            {
                Attempt.nEstadoJogo = (int)Estado.Empate;
            }
        }

        private Coordenada GetJogadaChancesAltasVitoria(List<MonteCarloTentativa> ListaTentativas)
        {
            List<MonteCarloTentativa> JogadasComVitoriaEmpate = ListaTentativas.FindAll(x => x.nEstadoJogo == (int)Estado.Vitoria);
            List<MonteCarloTentativa> JogadasComDerrota = ListaTentativas.FindAll(x => x.nEstadoJogo == (int)Estado.Derrota);

            List<string> CoordenadasVitoriosas = new List<string>();
            List<string> CoordenadasDesastrosas = new List<string>();
            foreach (var Vitoria in JogadasComVitoriaEmpate)
            {
                var CoordinateUnParse = Vitoria.oPassos[0].Linha.ToString() + ";" + Vitoria.oPassos[0].Coluna.ToString();
                CoordenadasVitoriosas.Add(CoordinateUnParse);
            }


            foreach (var Derrota in JogadasComDerrota)
            {
                var CoordinateUnParse = Derrota.oPassos[0].Linha.ToString() + ";" + Derrota.oPassos[0].Coluna.ToString();
                CoordenadasDesastrosas.Add(CoordinateUnParse);
            }


            var FrequenciaQueAJogadaDeuCerto = CoordenadasVitoriosas.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var FrequenciaQueAJogadaDeuErrado = CoordenadasDesastrosas.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

            foreach (var Item in FrequenciaQueAJogadaDeuErrado)
            {
                if (FrequenciaQueAJogadaDeuCerto.ContainsKey(Item.Key))
                    if (FrequenciaQueAJogadaDeuCerto[Item.Key] < Item.Value)
                        FrequenciaQueAJogadaDeuCerto.Remove(Item.Key);
            }

            int ValorMaisAlto = 0;
            string ProximoPassoCoordenada = "";

            foreach (var Item in FrequenciaQueAJogadaDeuCerto)
            {
                if (Item.Value > ValorMaisAlto)
                {
                    ValorMaisAlto = Item.Value;
                    ProximoPassoCoordenada = Item.Key;
                }
            }

            if (!string.IsNullOrEmpty(ProximoPassoCoordenada))
                return new Coordenada(ProximoPassoCoordenada);
            else return null;
        }

        private Coordenada GetJogadaChancesVitoria(List<MonteCarloTentativa> ListaTentativas)
        {
            List<MonteCarloTentativa> JogadasComVitoria = ListaTentativas.FindAll(x => x.nEstadoJogo == (int)Estado.Vitoria);
            if (JogadasComVitoria.Any())
            {
                List<string> CoordenadasVitoriosas = new List<string>();
                foreach (var Vitoria in JogadasComVitoria)
                {
                    var CoordinateUnParseVitoria = Vitoria.oPassos[0].Linha.ToString() + ";" + Vitoria.oPassos[0].Coluna.ToString();
                    CoordenadasVitoriosas.Add(CoordinateUnParseVitoria);
                }


                var FrequenciaVitorias = CoordenadasVitoriosas.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());


                int ValorMaisAltoVitorioso = 0;
                string ProximoPassoVitorioso = "";

                foreach (var Item in FrequenciaVitorias)
                {
                    if (Item.Value > ValorMaisAltoVitorioso)
                    {
                        ValorMaisAltoVitorioso = Item.Value;
                        ProximoPassoVitorioso = Item.Key.ToString();
                    }
                }

                if (!string.IsNullOrEmpty(ProximoPassoVitorioso))
                    return new Coordenada(ProximoPassoVitorioso);
                else return null;
            }
            else return null;
        }

        private Coordenada GetJogadaChancesEmpates(List<MonteCarloTentativa> ListaTentativas)
        {
            List<MonteCarloTentativa> JogadasComEmpate = ListaTentativas.FindAll(x => x.nEstadoJogo == (int)Estado.Empate);
            if (JogadasComEmpate.Any())
            {
                List<string> CoordenadasComEmpate = new List<string>();
                foreach (var Empate in JogadasComEmpate)
                {
                    var CoordinateUnParseVitoria = Empate.oPassos[0].Linha.ToString() + ";" + Empate.oPassos[0].Coluna.ToString();
                    CoordenadasComEmpate.Add(CoordinateUnParseVitoria);
                }


                var FrequenciaEmpate = CoordenadasComEmpate.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());


                int ValorMaisAltoEmpate = 0;
                string ProximoPassoEmpate = "";

                foreach (var Item in FrequenciaEmpate)
                {
                    if (Item.Value > ValorMaisAltoEmpate)
                    {
                        ValorMaisAltoEmpate = Item.Value;
                        ProximoPassoEmpate = Item.Key.ToString();
                    }
                }
                if (!string.IsNullOrEmpty(ProximoPassoEmpate))
                    return new Coordenada(ProximoPassoEmpate);
                else return null;
            }
            else return null;
        }
            
        private Coordenada GetJogadaChancesAleatoria(ClassTabuleiro oTabuleiro)
        {
            List<Coordenada> PosicoesLivre = GetPosicoesLivres(oTabuleiro);
            if (PosicoesLivre.Count > 0)
            {
                Random ProximaJogadaRandom = new Random();
                int x = ProximaJogadaRandom.Next(0, (PosicoesLivre.Count));

                return PosicoesLivre[x];
            }
            else return null;
        }

        private List<Coordenada> GetPosicoesLivres(ClassTabuleiro Tabuleiro)
        {
            List<Coordenada> Resultado = new List<Coordenada>();
            for (int i = 0; i < Tabuleiro.nTamanho; i++)
            {
                for (int j = 0; j < Tabuleiro.nTamanho; j++)
                {
                    if (Tabuleiro.oLinhasTabuleiro[i][j] == 0) Resultado.Add(new Coordenada(i, j));
                }
            }
            return Resultado;
        }

        private void GravarJogada(Coordenada ProximaJogada, ClassTabuleiro oTabuleiro)
        {
            if (this.nJogador == (int)Peao.X)
            {
                oTabuleiro.JogarX(ProximaJogada.Linha, ProximaJogada.Coluna);
            }
            else if (this.nJogador == (int)Peao.O)
            {
                oTabuleiro.JogarO(ProximaJogada.Linha, ProximaJogada.Coluna);

            }
            this.bProcessando = false;
            this.bJogadaFinalizada = true;
        }
    }
}



