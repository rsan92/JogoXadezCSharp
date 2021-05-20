using System;
using Tabuleiro;
using JogoXadrez;
using System.Collections.Generic;

namespace JogoXadezCSharp
{
    class Tela
    {
        public static void imprimirPartida(JogoXadrez.PartidaDeXadrez partida)
        {
            imprimirTabuleiro(partida.tab);
            
            Console.WriteLine();
            imprimirPecasCapturadas(partida);

            Console.WriteLine();
            Console.WriteLine($"Turno: {partida.turno + 1 }");

            if (!partida.terminada)
            {
                Console.WriteLine();
                if (partida.Xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine($"Vencedor foi: {partida.getJogadorAtual()}");
            }

        }

        public static void imprimirPecasCapturadas(JogoXadrez.PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças Capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca), Cor.Branca);
            Console.WriteLine();
            Console.Write("Pretas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta), Cor.Preta);
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto, Cor cor)
        {
            ConsoleColor aux = Console.ForegroundColor;
            if (cor == Cor.Preta)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            Console.Write("[");

            foreach (Peca p in conjunto)
            {
                Console.Write($"{p} ");
            }

            Console.Write("]");
            Console.ForegroundColor = aux;


        }

        public static void imprimirTabuleiro(Tabuleiro.Tabuleiro tab)
        {
            for (int linha = 0 ; linha < tab.linhas; linha++)
            {
                Console.Write($"{ 8 - linha} ");
                for (int coluna = 0; coluna < tab.colunas; coluna++)
                {
                    Peca resultado = tab.getPeca(linha, coluna);
                    Tela.imprimirPeca(resultado);
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }



        public static void imprimirTabuleiro(Tabuleiro.Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor corOriginal = Console.BackgroundColor;
            ConsoleColor corDestacada = ConsoleColor.DarkGray;

            for (int linha = 0; linha < tab.linhas; linha++)
            {
                Console.Write($"{ 8 - linha} ");
                for (int coluna = 0; coluna < tab.colunas; coluna++)
                {
                    if (posicoesPossiveis[linha, coluna])
                    {
                        Console.BackgroundColor = corDestacada;
                    }
                    else
                    {
                        Console.BackgroundColor = corOriginal;
                    }
                    Peca resultado = tab.getPeca(linha, coluna);
                    Tela.imprimirPeca(resultado);
                    Console.BackgroundColor = corOriginal;

                }
                Console.WriteLine();
            }
            Console.BackgroundColor = corOriginal;
            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirPeca(Peca p)
        {
            if (p == null)
            {
                Console.Write("- ");

            }
            else { 
                if (p.cor == Cor.Branca)
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
