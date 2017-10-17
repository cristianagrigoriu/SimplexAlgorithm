using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CO_T1_2
{
    class Program
    {
        public static string[,] matrix { get; set; }
        public static int m { get; set; }
        public static int n { get; set; }
        static void Main(string[] args)
        {

            ReadFromFile(@"D:\facultate\anul 1 master\CO\CO_AlgorithmSimplex\CO_T1_2\exemplu.txt");

            var simplex = new Simplex(matrix, m, n);
            simplex.PrintMatrix();
            if(simplex.SimplexAlgorithm())
            simplex.PrintSolution();
        }

        public static string[,] ReadFromFile(string filename)
        {
            var sr = new StreamReader(filename);
            string matrixFromFile = sr.ReadToEnd();

            string[] lines = matrixFromFile.Split('\n');

            int rowCount = lines.Length;
            int columnCount = lines[0].Split(' ').Length;

            m = rowCount;
            n = columnCount;

            matrix = new string[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                string[] lineSplit = lines[i].Split(' ');
                for (int j = 0; j < columnCount; j++)
                {
                    matrix[i, j] = lineSplit[j].Replace("\r","");
                }
            }
            sr.Close();
            return matrix;
        }
    }
}
