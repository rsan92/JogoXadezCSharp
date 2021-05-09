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
                    Tela.imprimirTabuleiro(partida.tab);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                    partida.executaMovimento(origem, destino);
                    Console.Clear();
                }


            } catch (Tabuleiro.Exceptions.PosicaoInvalidaException error)
            {
                Console.WriteLine(error.Message);
            }
        
        }
    }
}
