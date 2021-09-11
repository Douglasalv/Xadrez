using System;
using Xadrez.Tabuleiro;
using Xadrez.xadrez;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try{
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada){
                    try{
                        Console.Clear();
                        Tela.imprimirPartida(partida);

                        System.Console.WriteLine();
                        System.Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(origem);


                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();
                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

                        System.Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeDestino(origem,destino);

                        partida.realizaJogada(origem, destino);
                    }
                    catch(TabuleiroException e){
                        System.Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                
            }
            catch(TabuleiroException e)
            {
                System.Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
