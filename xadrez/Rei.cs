using Xadrez.Tabuleiro;

namespace Xadrez.xadrez
{
    public class Rei : Peca
    {
        public Rei(Tabuleiro.Tabuleiro tab, Cor cor) : base(tab, cor){

        }
        
        public override string ToString(){
            return "R";
        }
    }

}