using System;
using Tabuleiro;
using JogoXadrez;
namespace JogoXadezCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro.Tabuleiro tab = new Tabuleiro.Tabuleiro(8, 8);
            tab.setPeca(new Torre(tab, Cor.Preta), new Posicao(0,0));
            tab.setPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
            Tela.imprimirTabuleiro(tab);

            Console.ReadKey();
        }
    }
}
