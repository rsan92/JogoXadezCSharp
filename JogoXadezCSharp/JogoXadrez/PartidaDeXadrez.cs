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

        public Peca vulneravelEnPassant { get; private set; };

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
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.setPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            //Roque Pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(origemTorre);
                T.addMovimento();
                tab.setPeca(T, destinoTorre);
            }

            //Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(origemTorre);
                T.addMovimento();
                tab.setPeca(T, destinoTorre);
            }

            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada is null)
                {
                    Posicao posicaoPeao;
                    if (p.cor == Cor.Branca)
                    {
                        posicaoPeao = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posicaoPeao = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posicaoPeao);
                    capturadas.Add(pecaCapturada);
                }
            }


            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.removerMovimento();
            if (pecaCapturada != null)
            {
                tab.setPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.setPeca(p, origem);

            //Roque Pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(destinoTorre);
                T.removerMovimento();
                tab.setPeca(T, origemTorre);
            }

            //Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(destinoTorre);
                T.removerMovimento();
                tab.setPeca(T, origemTorre);
            }

            //En Passant
            if (p is Peao)
            {
                if(origem.Coluna != destino.Coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao p;
                    if (peao.cor == Cor.Branca)
                    {
                        p = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        p = new Posicao(4, destino.Coluna);
                    }

                    tab.setPeca(peao, p);
                }
            }

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
            
            Peca p = tab.getPeca(destino);

            if (p is Peao)
            {
                if (p.cor == Cor.Branca && destino.Linha == 0 || p.cor == Cor.Preta && destino.Linha == 7)
                {
                    p = tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.cor);
                    tab.setPeca(dama, destino);
                    pecas.Add(dama);
                }
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

            //Jogada En Passant
            if (p is Peao && (destino.Linha == origem.Linha -2 || destino.Linha == origem.Linha + 2))
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }
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
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            //Lado Preto
            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));

        }
    }
}
