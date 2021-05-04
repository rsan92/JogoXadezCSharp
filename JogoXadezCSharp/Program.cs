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
                Tabuleiro.Tabuleiro tab = new Tabuleiro.Tabuleiro(8, 8);
                tab.setPeca(new Torre(tab, Cor.Preta), new Posicao(0,0));
                tab.setPeca(new Torre(tab, Cor.Branca), new Posicao(1, 3));
                Tela.imprimirTabuleiro(tab);

                Console.ReadKey();
            } catch (Tabuleiro.Exceptions.PosicaoInvalidaException error)
            {
                Console.WriteLine(error.Message);
            }
        
        }
    }
}
