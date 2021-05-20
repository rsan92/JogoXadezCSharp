namespace Tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }

        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.tab = tab;
            this.cor = cor;
            posicao = null;
            QtdMovimentos = 0;
        }

        public void addMovimento()
        {
            QtdMovimentos++;
        }

        public void removerMovimento()
        {
            QtdMovimentos--;
        }

        public bool movimentoPossivel(Posicao pos)
        {
            return movimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public bool existemMovimentosPossiveis()
        {
            bool[,] matriz = movimentosPossiveis();
            for (int linha=0; linha< tab.linhas; linha++)
            {
                for(int coluna=0; coluna < tab.colunas; coluna++)
                {
                    if (matriz[linha, coluna])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract bool[,] movimentosPossiveis();
    }
}
