using Tabuleiro;

namespace JogoXadrez
{
    class Rei : Tabuleiro.Peca
    {
        public Rei(Tabuleiro.Tabuleiro tab, Cor cor): base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "R";
        }
    }
}
