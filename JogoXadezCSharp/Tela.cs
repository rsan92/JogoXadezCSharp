using System;
using Tabuleiro;
using JogoXadrez;
namespace JogoXadezCSharp
{
    class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro.Tabuleiro tab)
        {
            for (int linha = 0 ; linha < tab.linhas; linha++)
            {
                Console.Write($"{ 8 - linha} ");
                for (int coluna = 0; coluna < tab.colunas; coluna++)
                {
                    Peca resultado = tab.getPeca(linha, coluna);
                    if (resultado == null)
                    {
                        Console.Write("- ");
                    } else {
                        Tela.imprimirPeca(resultado);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirPeca(Peca p)
        {
            if(p.cor == Cor.Branca)
            {
                Console.Write($"{p} ");
            }
            else
            {
                ConsoleColor old = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{p} ");
                Console.ForegroundColor = old;
            }
        }

        public static JogoXadrez.PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new JogoXadrez.PosicaoXadrez(coluna, linha);
        }
    }
}
