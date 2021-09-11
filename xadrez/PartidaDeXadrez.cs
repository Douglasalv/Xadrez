using Xadrez.Tabuleiro;
using Xadrez.xadrez;
using System.Collections.Generic;

namespace Xadrez.xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro.Tabuleiro tab {get; private set;}
        public int turno {get; private set;}
        public  Cor jogadorAtual {get; private set;}
        public bool Terminada {get; private set;}
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque {get; private set;}

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro.Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento (Posicao origem, Posicao destino){
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if(pecaCapturada != null){
                capturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada){
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimentos();
            if(pecaCapturada != null){
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }

            tab.colocarPeca(p, origem);
        }

        public void realizaJogada(Posicao origem, Posicao destino){
            Peca pecaCapturada = executaMovimento(origem, destino);

            if(estaEmXeque(jogadorAtual)){
                desfazMovimento(origem,destino, pecaCapturada);
                throw new TabuleiroException("Voce nao pode se colocar em cheque!");
            }

            if(estaEmXeque(adversaria(jogadorAtual))){
                xeque = true;
            }
            else{
                xeque = false;
            }

            if (testeXequeMate(adversaria(jogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }
        }

        public void validarPosicaoDeOrigem(Posicao pos){
            if(tab.peca(pos) == null){
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(jogadorAtual != tab.peca(pos).Cor){
                throw new TabuleiroException("Peca escolhida é uma peca adversaria!");
            }
            if(!tab.peca(pos).existeMovimentosPossiveis()){
                throw new TabuleiroException("Nao ha movimentos possiveis para a peca de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino){
            if(!tab.peca(origem).podeMoverPara(destino)){
                throw new TabuleiroException("Posicao de destino invalida");
            }
        }

        private void mudaJogador(){
            if(jogadorAtual == Cor.Branca){
                jogadorAtual = Cor.Preta;
            }
            else{
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor){
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if(x.Cor == cor){
                    aux.Add(x);
                }
            }

            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor){
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if(x.Cor == cor){
                    aux.Add(x);
                }
            }

            aux.ExceptWith(pecasCapturadas(cor));
            
            return aux;
        }

        private Cor adversaria(Cor cor){
            if(cor == Cor.Branca){
                return Cor.Preta;
            }

            else return Cor.Branca;
        }

        private Peca rei(Cor cor){
            
            foreach (Peca x in pecasEmJogo(cor))
            {
                if(x is Rei){
                    return x;
                }
            }

            return null;  
        }

        public bool estaEmXeque(Cor cor){
            Peca R = rei(cor);

            if(R == null){
                throw new TabuleiroException("Nao existe rei da cor " + cor + " no tabuleiro!");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
               bool[,] mat = x.movimentosPossiveis();
               if(mat[R.Posicao.Linha, R.Posicao.Coluna]){
                   return true;
               }
            }

            return false;
        }

        public bool testeXequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }

            foreach(Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for(int i = 0; i < tab.Linhas; i++)
                {
                    for(int j= 0; j < tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(x.Posicao, new Posicao(i, j));
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
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


        public void colocarNovaPeca(char coluna, int linha, Peca peca){
            tab.colocarPeca(peca, new PosicaoXadrez(coluna,linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas(){
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));

        }
    }
}