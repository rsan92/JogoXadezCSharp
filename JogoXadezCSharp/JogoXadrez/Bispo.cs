using Tabuleiro;

namespace JogoXadrez
{
    class Bispo : Tabuleiro.Peca
    {
        public Bispo(Tabuleiro.Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "B";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.getPeca(pos);
            return p == null || p.cor != this.cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //NO
            pos.setValores(posicao.Linha - 1, posicao.Coluna - 1);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if(tab.getPeca(pos) != null && tab.getPeca(pos).cor != cor)
                {
                    break;
                }
                pos.setValores(pos.Linha - 1, pos.Coluna - 1);
            }

            //NE
            pos.setValores(posicao.Linha - 1, posicao.Coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tab.getPeca(pos) != null && tab.getPeca(pos).cor != cor)
                {
                    break;
                }
                pos.setValores(pos.Linha - 1, pos.Coluna + 1);
            }

            //SE
            pos.setValores(posicao.Linha + 1, posicao.Coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tab.getPeca(pos) != null && tab.getPeca(pos).cor != cor)
                {
                    break;
                }
                pos.setValores(pos.Linha + 1, pos.Coluna + 1);
            }

            //SO
            pos.setValores(posicao.Linha + 1, posicao.Coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tab.getPeca(pos) != null && tab.getPeca(pos).cor != cor)
                {
                    break;
                }
                pos.setValores(pos.Linha + 1, pos.Coluna - 1);
            }

            return matriz;
        }
    }
}
