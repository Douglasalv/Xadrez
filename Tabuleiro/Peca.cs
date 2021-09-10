namespace Xadrez.Tabuleiro
{
    public abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            this.Posicao = null;
            this.Tabuleiro = tabuleiro;
            this.Cor = cor;
            this.qteMovimentos = 0;
        }

        public abstract bool[,] movimentosPossiveis();

        public void incrementarQteMovimentos(){
            qteMovimentos++;
        }

        private bool podeMover(Posicao pos){
            Peca p = Tabuleiro.peca(pos);
            return p == null || p.Cor != this.Cor;
        }

    }
}