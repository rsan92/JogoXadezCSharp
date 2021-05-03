using System;
using Tabuleiro;
namespace JogoXadezCSharp
{
    class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro.Tabuleiro tab)
        {
            for (int linha = 0 ; linha < tab.linhas; linha++)
            {
                for (int coluna = 0; coluna < tab.colunas; coluna++)
                {
                    Peca resultado = tab.getPeca(linha, coluna);
                    if (resultado == null)
                    {
                        Console.Write("- ");
                    } else { 
                        Console.Write(resultado + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
