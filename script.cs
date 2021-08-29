using System;
using System.Collections;
using System.Collections.Generic;

namespace urionlinejudge
{
    class Program
    {
        static string[,] tabuleiro = { { "a8", "b8", "c8", "d8", "e8", "f8", "g8", "h8" },
                                       { "a7", "b7", "c7", "d7", "e7", "f7", "g7", "h7" },
                                       { "a6", "b6", "c6", "d6", "e6", "f6", "g6", "h6" },
                                       { "a5", "b5", "c5", "d5", "e5", "f5", "g5", "h5" },
                                       { "a4", "b4", "c4", "d4", "e4", "f4", "g4", "h4" },
                                       { "a3", "b3", "c3", "d3", "e3", "f3", "g3", "h3" },
                                       { "a2", "b2", "c2", "d2", "e2", "f2", "g2", "h2" },
                                       { "a1", "b1", "c1", "d1", "e1", "f1", "g1", "h1" } };

        static int[,] partida = { { 0, 0, 0, 0, 0, 0, 0, 0 },
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

        static Tuple<int, int> ObterCoordenadas(string cavalo)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tabuleiro[i,j] == cavalo)
                    {
                        return new Tuple<int, int>(i, j);
                    }
                }
            }

            throw new Exception("A casa " + cavalo + " não existe no tabuleiro de xadrez.");
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
                } else if (partida[l, c] == 1)
                {
                    continue;
                } else if (partida[l, c] == 2)
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
            while (true) {
                String es = Console.ReadLine();
                if (es != null)
                {
                    string[] e = es.Split(' ');

                    string pi = e[0];
                    string pf = e[1];

                    int r = 0;

                    if (pi != pf)
                    {
                        Tuple<int, int> cf = ObterCoordenadas(pf);

                        partida[cf.Item1, cf.Item2] = 2;

                        Tuple<int, int> ci = ObterCoordenadas(pi);

                        r = BFS(ci.Item1, ci.Item2);
                    }

                    Console.WriteLine("To get from " + pi + " to " + pf + " takes " + r.ToString() + " knight moves.");

                    Restart();
                } else
                {
                    break;
                }
            }
        }
    }
}
