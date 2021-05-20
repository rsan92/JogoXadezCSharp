using Tabuleiro;
using JogoXadrez;
using System.Collections.Generic;

namespace JogoXadezCSharp.JogoXadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro.Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        private Tabuleiro.Cor jogadorAtual;
        public bool terminada { get; private set; }

        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool Xeque { get; private set; }
        public PartidaDeXadrez()
        {
            tab = new Tabuleiro.Tabuleiro(8, 8);
            turno = 0;
            jogadorAtual = Cor.Branca;
            terminada = false;
            Xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();

        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.addMovimento();
            Peca peca_capturada = tab.retirarPeca(destino);
            tab.setPeca(p, destino);
            if (peca_capturada != null)
            {
                capturadas.Add(peca_capturada);
            }
            return peca_capturada;
        }

        public Cor corAdversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;

            }
        }

        private Peca getRei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = getRei(cor);
            if (R == null)
            {
                throw new Tabuleiro.Exceptions.PosicaoInvalidaException($"Não existe rei da Cor {cor}");
            }
            foreach (Peca peca in pecasEmJogo(corAdversaria(cor)))
            {
                bool[,] movimentos_peca = peca.movimentosPossiveis();
                if(movimentos_peca[R.posicao.Linha, R.posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool VerificarXequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }

            foreach(Peca x in pecasEmJogo(cor))
            {
                bool[,] movimentos = x.movimentosPossiveis();
                for (int linha = 0; linha < tab.linhas; linha++)
                {
                    for (int coluna = 0; coluna < tab.colunas; coluna++)
                    {
                        if (movimentos[linha, coluna]) {
                            Posicao destino = new Posicao(linha, coluna);
                            Posicao origem = x.posicao;
                            Peca PecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            DesfazMovimento(origem, destino, PecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }

                    }
                }
            }


            return true;
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in capturadas)
            {
                if (p.cor == cor)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in pecas)
            {
                if (p.cor == cor)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new Tabuleiro.Exceptions.PosicaoInvalidaException("Você não pode se colocar em XEQUE.");
            }
            if (estaEmXeque(corAdversaria(jogadorAtual)))
            {
                Xeque = true;
                if (VerificarXequeMate(corAdversaria(jogadorAtual)))
                {
                    terminada = true;
                }
            }
            else
            {
                Xeque = false;
            }
            
            if (!terminada)
            {
                turno++;
                mudarJogador();
            }

        }

        public void DesfazMovimento(Posicao origem,Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.removerMovimento();
            if (pecaCapturada != null)
            {
                tab.setPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.setPeca(p, origem);

        }

        public void validarPosicaoOrigem(Posicao pos)
        {
            if (tab.getPeca(pos) == null)
            {
                throw new Tabuleiro.Exceptions.PosicaoInvalidaException("Não existe peça na posição de origem!");
            }

            if (jogadorAtual != tab.getPeca(pos).cor)
            {
                throw new Tabuleiro.Exceptions.PosicaoInvalidaException("Cor da peça diferente do jogador atual!");
            }

            if (!tab.getPeca(pos).existemMovimentosPossiveis())
            {
                throw new Tabuleiro.Exceptions.PosicaoInvalidaException("Não existem movimentos possiveis para a peça escolhida!");
            }
        }

        public void validarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.getPeca(origem).movimentoPossivel(destino))
            {
                throw new Tabuleiro.Exceptions.PosicaoInvalidaException("Não é possível realizar movimento para o destino selecionado!");

            }
        }

        public string getJogadorAtual()
        {
            if (jogadorAtual == Cor.Branca)
            {
                return "Brancas";
            }
            else
            {
                return "Pretas";
            }
        }

        private void mudarJogador()
        {
            if(this.jogadorAtual == Cor.Branca)
            {
                this.jogadorAtual = Cor.Preta;
            }
            else
            {
                this.jogadorAtual = Cor.Branca;
            }
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.setPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
            
        }
        private void colocarPecas()
        {
            //Lado Branco
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            //Lado Preto
            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));

        }
    }
}
