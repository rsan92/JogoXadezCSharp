using System;
using JogoXadrez;
using Tabuleiro;
using Tabuleiro.Exceptions;
namespace JogoXadezCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                JogoXadrez.PartidaDeXadrez partida = new JogoXadrez.PartidaDeXadrez();
                while (!partida.terminada)
                {
                    try { 
                        Console.Clear();
                        Tela.imprimirPartida(partida);

                        Console.WriteLine();
                        
                        Console.WriteLine($"Vez do jogador com as peças: {partida.getJogadorAtual()}");

                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoOrigem(origem);

                        Console.Clear();                   
                        bool[,] posicoesPossiveis = partida.tab.getPeca(origem).movimentosPossiveis();

                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);
                        Console.WriteLine();

                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                    
                        partida.validarPosicaoDestino(origem, destino);
                        partida.realizaJogada(origem, destino);
                    } catch (PosicaoInvalidaException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Pressione qualquer tecla para continuar.");
                        Console.ReadKey();
                    }
                }
                Console.Clear();
                Tela.imprimirPartida(partida);



            }
            catch (Tabuleiro.Exceptions.PosicaoInvalidaException error)
            {
                Console.WriteLine(error.Message);
            }
        
        }
    }
}
