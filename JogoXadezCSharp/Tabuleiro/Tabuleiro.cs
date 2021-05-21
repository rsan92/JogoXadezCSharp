using Tabuleiro.Exceptions;

namespace Tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }

        private Peca[,] pecas;
        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];
        }

        public Peca getPeca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

        public Peca getPeca(Posicao pos)
        {
            return pecas[pos.Linha, pos.Coluna];
        }

        public void setPeca(Peca p, Posicao pos)
        {
            if (existePeca(pos)) {
                throw new PosicaoInvalidaException("Já existe uma peça nessa posição.");
            }
            pecas[pos.Linha, pos.Coluna] = p;
            p.posicao = pos;
        }

        public Peca retirarPeca(Posicao pos)
        {
            if (pecas[pos.Linha, pos.Coluna] == null)
            {
                return null;
            }

            Peca aux = pecas[pos.Linha, pos.Coluna];
            aux.posicao = null;
            pecas[pos.Linha, pos.Coluna] = null;
            return aux;
        }

        public bool posicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= linhas || pos.Coluna < 0 || pos.Coluna >= colunas)
            {
                return false;
            }
            return true;

        }

        public bool existePeca(Posicao pos)
        {
            validarPosicao(pos);
            return getPeca(pos) != null;
        }

        public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos)){
                throw new PosicaoInvalidaException($"Posição inválida. Linha:{pos.Linha}; Coluna:{pos.Coluna}");
            }
        }

    }
}
