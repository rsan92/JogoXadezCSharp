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
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

            }

            //ne
            pos.setValores(posicao.Linha - 1, posicao.Coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

            }

            //direita
            pos.setValores(posicao.Linha, posicao.Coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

            }


            //se
            pos.setValores(posicao.Linha +1, posicao.Coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

            }

            //baixo
            pos.setValores(posicao.Linha + 1, posicao.Coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

            }

            //so
            pos.setValores(posicao.Linha + 1, posicao.Coluna +1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

            }

            //esquerda
            pos.setValores(posicao.Linha, posicao.Coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

            }

            //no
            pos.setValores(posicao.Linha - 1 , posicao.Coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

            }

            return matriz;
        }
    }
}
