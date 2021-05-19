using System;
using System.Collections.Generic;
using System.Text;

namespace BatailleSpacialeApp
{
    public static class Utilitaires
    {
        private static Random random = new Random();
        //Methode pour générer un entier aléatoire entre les bormines [min ; max[
        public static int RandomGen(int min, int max)
        {
            return random.Next(min,max);
        }

        //Methode pour ecrire {word} en {color}
        public static void WriteColor(string text, string  word, ConsoleColor color)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            string[] array = text.Split(word);
            string last = array[array.Length - 1];
            foreach (string value in array)
            {
                Console.Write(value);
                if(value != last)
                {
                    Console.ForegroundColor = color;
                    Console.Write(word);
                    Console.ForegroundColor = currentColor;
                }
            }
        }

        //Methode pour trier les tableaux de dimension [2][n] en fonction d'une des deux colonnes
        public static void Sort(int[][] tab, int taille, int col)
        {
            int reverse_col = 0;
            if (col == 0)
                reverse_col = 1;
            for (int i = 0; i < taille - 1; i++)
            {
                for (int j = 0; j < taille - i - 1; j++)
                {
                    if (tab[col][j] > tab[col][j + 1])
                    {
                        int temp = tab[col][j];
                        tab[col][j] = tab[col][j + 1];
                        tab[col][j + 1] = temp;
                        temp = tab[reverse_col][j];
                        tab[reverse_col][j] = tab[reverse_col][j + 1];
                        tab[reverse_col][j + 1] = temp;
                    }
                }
            }
        }
    }
}
