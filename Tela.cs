using System;
using Xadrez.Tabuleiro;

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
                        Console.Write("-");
                    }
                    else {
                        System.Console.Write(tab.peca(i,j));
                        System.Console.WriteLine(" ");
                    }
                    

                }
                System.Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
            
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