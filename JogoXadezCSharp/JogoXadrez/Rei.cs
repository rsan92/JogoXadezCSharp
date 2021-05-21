using Tabuleiro;
using JogoXadezCSharp.JogoXadrez;

namespace JogoXadrez
{
    class Rei : Tabuleiro.Peca
    {

        private PartidaDeXadrez partida;
        public Rei(Tabuleiro.Tabuleiro tab, Cor cor, PartidaDeXadrez partida): base(tab, cor)
        {
            this.partida = partida;
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

        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = tab.getPeca(pos);
            return p != null && p is Torre && p.cor == this.cor && p.QtdMovimentos == 0;
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

            //Verifica Roque
            if (QtdMovimentos == 0 && !partida.Xeque )
            {
                //Pequeno
                Posicao posTorre1 = new Posicao(pos.Linha, pos.Coluna + 3);
                if (testeTorreParaRoque(posTorre1)){
                    Posicao p1 = new Posicao(pos.Linha, pos.Coluna + 1);
                    Posicao p2 = new Posicao(pos.Linha, pos.Coluna + 2);
                    if (tab.getPeca(p1) == null && tab.getPeca(p2) == null)
                    {
                        matriz[pos.Linha, pos.Coluna + 2] = true;
                    }

                }

                //Grande
                Posicao posTorre2 = new Posicao(pos.Linha, pos.Coluna - 4);
                if (testeTorreParaRoque(posTorre2)){
                    Posicao p1 = new Posicao(pos.Linha, pos.Coluna - 1);
                    Posicao p2 = new Posicao(pos.Linha, pos.Coluna - 2);
                    Posicao p3 = new Posicao(pos.Linha, pos.Coluna - 3);
                    if (tab.getPeca(p1) == null && tab.getPeca(p2) == null && tab.getPeca(p3) == null)
                    {
                        matriz[pos.Linha, pos.Coluna - 2] = true;
                    }

                }
            }

            return matriz;
        }
    }
}
