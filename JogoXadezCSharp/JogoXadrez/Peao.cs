using Tabuleiro;

namespace JogoXadrez
{
    class Peao : Tabuleiro.Peca
    {
        public Peao(Tabuleiro.Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.getPeca(pos);
            return p != null && p.cor != this.cor;
        }

        private bool livre(Posicao pos)
        {
            return tab.getPeca(pos) == null;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branca)
            {
                pos.setValores(pos.Linha - 1, pos.Coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.setValores(pos.Linha - 2, pos.Coluna);
                if (tab.posicaoValida(pos) && livre(pos) && QtdMovimentos == 0)
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.setValores(pos.Linha - 1, pos.Coluna -1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.setValores(pos.Linha - 1, pos.Coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }
            }
            else
            {
                pos.setValores(pos.Linha + 1, pos.Coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.setValores(pos.Linha + 2, pos.Coluna);
                if (tab.posicaoValida(pos) && livre(pos) && QtdMovimentos == 0)
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.setValores(pos.Linha + 1, pos.Coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.setValores(pos.Linha + 1, pos.Coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }
            }

            return matriz;
        }
    }
}
