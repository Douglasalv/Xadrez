using Xadrez.Tabuleiro;

namespace Xadrez.xadrez
{
    public class Torre : Peca
    {
        public Torre(Tabuleiro.Tabuleiro tab, Cor cor) : base(tab, cor){

        }
        
        public override string ToString(){
            return "T";
        }
    }
}