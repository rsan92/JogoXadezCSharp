using Tabuleiro;

namespace JogoXadezCSharp.JogoXadrez
{
    class PosicaoXadrez
    {
        public char coluna { get; protected set; }
        public int linha { get; protected set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        public Posicao toPosicao()
        {
            return new Posicao(8 - linha, coluna - 'a');
        }

        public override string ToString()
        {
            return $"{coluna}{linha}";
        }
    }
}
