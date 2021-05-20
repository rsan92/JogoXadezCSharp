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

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.getPeca(pos);
            return p == null || p.cor != this.cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //acima
            pos.setValores(posicao.Linha - 1, posicao.Coluna);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tab.getPeca(pos) != null && tab.getPeca(pos).cor != this.cor)
                {
                    break;
                }
                pos.Linha--;
            }

            //baixo
            pos.setValores(posicao.Linha + 1, posicao.Coluna);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tab.getPeca(pos) != null && tab.getPeca(pos).cor != this.cor)
                {
                    break;
                }
                pos.Linha++;
            }

            //direita
            pos.setValores(posicao.Linha, posicao.Coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tab.getPeca(pos) != null && tab.getPeca(pos).cor != this.cor)
                {
                    break;
                }
                pos.Coluna++;
            }


            //esquerda
            pos.setValores(posicao.Linha, posicao.Coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
                if (tab.getPeca(pos) != null && tab.getPeca(pos).cor != this.cor)
                {
                    break;
                }
                pos.Coluna--;
            }



            return matriz;
        }

    }
}
