using Tabuleiro;
using JogoXadezCSharp.JogoXadrez;
namespace JogoXadrez
{
    class Peao : Tabuleiro.Peca
    {

        private PartidaDeXadrez partida;
        public Peao(Tabuleiro.Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
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
                pos.setValores(posicao.Linha - 1, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.setValores(posicao.Linha - 2, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos) && QtdMovimentos == 0)
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.setValores(posicao.Linha - 1, posicao.Coluna -1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.setValores(posicao.Linha - 1, posicao.Coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                //En Passant
                if(posicao.Linha == 3)
                {
                    Posicao aEsquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if(tab.posicaoValida(aEsquerda) && existeInimigo(aEsquerda) && tab.getPeca(aEsquerda) == partida.vulneravelEnPassant)
                    {
                        matriz[aEsquerda.Linha -1, aEsquerda.Coluna] = true;
                    }

                    Posicao aDireita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tab.posicaoValida(aDireita) && existeInimigo(aDireita) && tab.getPeca(aDireita) == partida.vulneravelEnPassant)
                    {
                        matriz[aDireita.Linha -1, aDireita.Coluna] = true;
                    }
                }
            }
            else
            {
                pos.setValores(posicao.Linha + 1, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.setValores(posicao.Linha + 2, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos) && QtdMovimentos == 0)
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.setValores(posicao.Linha + 1, posicao.Coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                pos.setValores(posicao.Linha + 1, posicao.Coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                //En Passant
                if (posicao.Linha == 4)
                {
                    Posicao aEsquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (tab.posicaoValida(aEsquerda) && existeInimigo(aEsquerda) && tab.getPeca(aEsquerda) == partida.vulneravelEnPassant)
                    {
                        matriz[aEsquerda.Linha +1, aEsquerda.Coluna] = true;
                    }

                    Posicao aDireita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tab.posicaoValida(aDireita) && existeInimigo(aDireita) && tab.getPeca(aDireita) == partida.vulneravelEnPassant)
                    {
                        matriz[aDireita.Linha +1, aDireita.Coluna] = true;
                    }
                }


            }

            return matriz;
        }
    }
}
