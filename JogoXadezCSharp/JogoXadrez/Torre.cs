using Tabuleiro;

namespace JogoXadrez
{
    class Torre : Tabuleiro.Peca
    {
        public Torre(Tabuleiro.Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "T";
        }
    }
}
