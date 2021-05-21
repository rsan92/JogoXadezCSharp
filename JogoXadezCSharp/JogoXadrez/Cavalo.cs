using Tabuleiro;

namespace JogoXadrez
{
    class Cavalo : Tabuleiro.Peca
    {
        public Cavalo(Tabuleiro.Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "C";
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

            
            pos.setValores(posicao.Linha - 1, posicao.Coluna - 2);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            pos.setValores(posicao.Linha - 2, posicao.Coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            pos.setValores(posicao.Linha - 2, posicao.Coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            pos.setValores(posicao.Linha + 1, posicao.Coluna + 2);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            pos.setValores(posicao.Linha + 2, posicao.Coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            pos.setValores(posicao.Linha + 1, posicao.Coluna - 2);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }


            return matriz;
        }
    }
}
