using System;
using Xadrez.Tabuleiro;
using Xadrez.xadrez;

namespace Xadrez
{
    public class Tela
    {
        public Tela()
        {
            
        }

        public static void imprimirTabuleiro(Tabuleiro.Tabuleiro tab){
            for(int i = 0; i < tab.Linhas; i++){
                System.Console.Write(8 - i + " ");
                for(int j = 0; j < tab.Colunas; j++){
                    if(tab.peca(i,j) == null){
                        Console.Write(" - ");
                    }
                    else {
                        System.Console.Write(" ");
                        System.Console.Write(tab.peca(i,j));
                        System.Console.Write(" ");
                    }
                    

                }
                System.Console.WriteLine();
            }

            Console.WriteLine("   a  b  c  d  e  f  g  h");
            
        }

        public static PosicaoXadrez lerPosicaoXadrez(){
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");

            return new PosicaoXadrez(coluna,linha);

        }

        public static void imprimirPeca(Peca peca){
            if(peca.Cor == Cor.Branca){
                System.Console.Write(peca);
            }
            else {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}