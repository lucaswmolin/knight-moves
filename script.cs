using System;
using System.Collections;
using System.Collections.Generic;

namespace urionlinejudge
{
    class Program
    {
        const sbyte ot = 8;

        static char[] linha = { '8', '7', '6', '5', '4', '3', '2', '1' };

        static char[] coluna = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

        static sbyte[,] partida = { { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0 } };

        static sbyte[] queueX = new sbyte[4096];
        static sbyte[] queueY = new sbyte[4096];
        static sbyte[] queueN = new sbyte[4096];

        static sbyte ObterLinha(char c)
        {
            for (sbyte i = 0; i < ot; i++)
            {
                if (linha[i] == c)
                {
                    return i;
                }
            }

            throw new Exception("Esta linha não existe no tabuleiro de xadrez.");
        }

        static sbyte ObterColuna(char c)
        {
            for (sbyte i = 0; i < ot; i++)
            {
                if (coluna[i] == c)
                {
                    return i;
                }
            }

            throw new Exception("Esta coluna não existe no tabuleiro de xadrez.");
        }

        static sbyte BFS(sbyte l, sbyte c)
        {
            int pf = 1;
            int tf = 0;

            sbyte n;

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
                else
                {
                    return n;
                }

                n++;

                l -= 2;
                if (l > -1 && l < ot)
                {
                    c--;
                    if (c > -1 && c < ot)
                    {
                        queueX[pf] = l;
                        queueY[pf] = c;
                        queueN[pf++] = n;
                    }

                    c += 2;

                    if (c > -1 && c < ot)
                    {
                        queueX[pf] = l;
                        queueY[pf] = c;
                        queueN[pf++] = n;
                    }
                    c--;
                }
                l += 2;

                l += 2;
                if (l > -1 && l < ot)
                {
                    c--;
                    if (c > -1 && c < ot)
                    {
                        queueX[pf] = l;
                        queueY[pf] = c;
                        queueN[pf++] = n;
                    }

                    c += 2;

                    if (c > -1 && c < ot)
                    {
                        queueX[pf] = l;
                        queueY[pf] = c;
                        queueN[pf++] = n;
                    }
                    c--;
                }
                l -= 2;

                l--;
                if (l > -1 && l < ot)
                {
                    c -= 2;
                    if (c > -1 && c < ot)
                    {
                        queueX[pf] = l;
                        queueY[pf] = c;
                        queueN[pf++] = n;
                    }

                    c += 4;

                    if (c > -1 && c < ot)
                    {
                        queueX[pf] = l;
                        queueY[pf] = c;
                        queueN[pf++] = n;
                    }
                    c -= 2;
                }
                l++;

                l++;
                if (l > -1 && l < ot)
                {
                    c -= 2;
                    if (c > -1 && c < ot)
                    {
                        queueX[pf] = l;
                        queueY[pf] = c;
                        queueN[pf++] = n;
                    }

                    c += 4;

                    if (c > -1 && c < ot)
                    {
                        queueX[pf] = l;
                        queueY[pf] = c;
                        queueN[pf++] = n;
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

                sbyte r = 0;

                if (pi != pf)
                {
                    partida[ObterLinha(pf[1]), ObterColuna(pf[0])] = 2;

                    r = BFS(ObterLinha(pi[1]), ObterColuna(pi[0]));
                }

                Console.WriteLine("To get from " + e[0] + " to " + e[1] + " takes " + r.ToString() + " knight moves.");

                for (sbyte i = 0; i < ot; i++)
                {
                    for (sbyte j = 0; j < ot; j++)
                    {
                        partida[i, j] = 0;
                    }
                }

                es = Console.ReadLine();
            }
        }
    }
}