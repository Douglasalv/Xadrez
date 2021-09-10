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
                    imprimirPeca(tab.peca(i,j));
                }
                System.Console.WriteLine();
            }

            Console.WriteLine("   a  b  c  d  e  f  g  h");
        }

        public static void imprimirTabuleiro(Tabuleiro.Tabuleiro tab, bool[,] posicoesPossiveis){
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            
            for(int i = 0; i < tab.Linhas; i++){
                System.Console.Write(8 - i + " ");
                for(int j = 0; j < tab.Colunas; j++){
                    if(posicoesPossiveis[i,j]){
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else{
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(i,j));
                    Console.BackgroundColor = fundoOriginal;
                }
                System.Console.WriteLine();
            }

            Console.WriteLine("   a  b  c  d  e  f  g  h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez lerPosicaoXadrez(){
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");

            return new PosicaoXadrez(coluna,linha);

        }

        public static void imprimirPeca(Peca peca){
            if(peca == null){
                Console.Write(" - ");
            }

            else {
                System.Console.Write(" ");
                if(peca.Cor == Cor.Branca){
                    System.Console.Write(peca);
                }
                else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                System.Console.Write(" ");
            }
        }
    }
}