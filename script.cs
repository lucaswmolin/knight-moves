using System;
using System.Collections;
using System.Collections.Generic;

namespace urionlinejudge
{
    class Program
    {
        static char[] linha = { '8', '7', '6', '5', '4', '3', '2', '1' };

        static char[] coluna = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

        static byte[,] partida = { { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 } };

        static int[] queueX = new int[4096];
        static int[] queueY = new int[4096];
        static int[] queueN = new int[4096];

        static void Restart()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    partida[i, j] = 0;
                }
            }
        }

        static int ObterLinha(char c)
        {
            for (int i = 0; i < 8; i++)
            {
                if (linha[i] == c)
                {
                    return i;
                }
            }

            throw new Exception("Esta linha não existe no tabuleiro de xadrez.");
        }

        static int ObterColuna(char c)
        {
            for (int i = 0; i < 8; i++)
            {
                if (coluna[i] == c)
                {
                    return i;
                }
            }

            throw new Exception("Esta coluna não existe no tabuleiro de xadrez.");
        }

        static int BFS(int l, int c)
        {
            int pf = 1;
            int tf = 0;

            int n;

            queueX[0] = l;
            queueY[0] = c;
            queueN[0] = 0;

            while (pf > tf)
            {
                l = queueX[tf];
                c = queueY[tf];
                n = queueN[tf++];

                if (partida[l, c] == 0)
                {
                    partida[l, c] = 1;
                }
                else if (partida[l, c] == 1)
                {
                    continue;
                }
                else if (partida[l, c] == 2)
                {
                    return n;
                }

                if (l - 2 > -1 && l - 2 < 8)
                {
                    if (c - 1 > -1 && c - 1 < 8)
                    {
                        queueX[pf] = l - 2;
                        queueY[pf] = c - 1;
                        queueN[pf++] = n + 1;
                    }

                    if (c + 1 > -1 && c + 1 < 8)
                    {
                        queueX[pf] = l - 2;
                        queueY[pf] = c + 1;
                        queueN[pf++] = n + 1;
                    }
                }

                if (l + 2 > -1 && l + 2 < 8)
                {
                    if (c - 1 > -1 && c - 1 < 8)
                    {
                        queueX[pf] = l + 2;
                        queueY[pf] = c - 1;
                        queueN[pf++] = n + 1;
                    }

                    if (c + 1 > -1 && c + 1 < 8)
                    {
                        queueX[pf] = l + 2;
                        queueY[pf] = c + 1;
                        queueN[pf++] = n + 1;
                    }
                }

                if (l - 1 > -1 && l - 1 < 8)
                {
                    if (c - 2 > -1 && c - 2 < 8)
                    {
                        queueX[pf] = l - 1;
                        queueY[pf] = c - 2;
                        queueN[pf++] = n + 1;
                    }

                    if (c + 2 > -1 && c + 2 < 8)
                    {
                        queueX[pf] = l - 1;
                        queueY[pf] = c + 2;
                        queueN[pf++] = n + 1;
                    }
                }

                if (l + 1 > -1 && l + 1 < 8)
                {
                    if (c - 2 > -1 && c - 2 < 8)
                    {
                        queueX[pf] = l + 1;
                        queueY[pf] = c - 2;
                        queueN[pf++] = n + 1;
                    }

                    if (c + 2 > -1 && c + 2 < 8)
                    {
                        queueX[pf] = l + 1;
                        queueY[pf] = c + 2;
                        queueN[pf++] = n + 1;
                    }
                }
            }

            return 0;
        }

        static void Main(string[] args)
        {
            String es = Console.ReadLine();
            while (es != null)
            {
                string[] e = es.Split(' ');

                char[] pi = e[0].ToCharArray();

                char[] pf = e[1].ToCharArray();

                int r = 0;

                if (pi != pf)
                {
                    partida[ObterLinha(pf[1]), ObterColuna(pf[0])] = 2;

                    r = BFS(ObterLinha(pi[1]), ObterColuna(pi[0]));
                }

                Console.WriteLine("To get from " + e[0] + " to " + e[1] + " takes " + r.ToString() + " knight moves.");

                Restart();

                es = Console.ReadLine();
            }
        }
    }
}
