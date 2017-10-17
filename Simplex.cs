using System;

namespace CO_T1_2
{
    class Simplex
    {
        public string[,] matrix { get; set; }
        public int m { get; set; }
        public int n { get; set; }
        public int l { get; set; }
        public int k { get; set; }

        public Simplex(string[,] _matrix, int m, int n)
        {
            this.matrix = _matrix;
            this.m = m;
            this.n = n;
        }

        public bool SimplexAlgorithm()
        {
            while (!IsOptimal())
            {
                GetPivotColumn();

                if (IsOptimalInfinite())
                {
                    Console.Write("Optim is infinite!");
                    return false;
                }
                GetPivotLine();

                Rotation();
                ChangeVariables();

                for (var i = 1; i < m; i++)
                {
                    if (i != k)
                    {
                        matrix[i, l] = (-(Double.Parse(matrix[i, l]) / Double.Parse(matrix[k, l]))).ToString();
                    }
                }
                for (var j = 1; j < n; j++)
                {
                    if (j != l)
                    {
                        matrix[k, j] = (Double.Parse(matrix[k, j]) / Double.Parse(matrix[k, l])).ToString();
                    }
                }
                matrix[k, l] = (1 / Double.Parse(matrix[k, l])).ToString();

                PrintMatrix();
            }
            return true;
        }

        private void Rotation()
        {
            for (var i = 1; i < m; i++)
            {
                if (i != k)
                {
                    for (var j = 1; j < n; j++)
                    {
                        if (j != l)
                        {
                            matrix[i, j] =
                                ((Double.Parse(matrix[i, j])*Double.Parse(matrix[k, l]) -
                                  Double.Parse(matrix[i, l])*Double.Parse(matrix[k, j]))
                                 /Double.Parse(matrix[k, l])).ToString();
                        }
                    }
                }
            }
        }

        private void ChangeVariables()
        {
            string aux = matrix[k, 0];
            matrix[k, 0] = matrix[0, l];
            matrix[0, l] = aux;
        }

        bool IsOptimal()
        {
            for (int i = 1; i < n - 1; i++)
            {  if (Double.Parse(matrix[m - 1, i]) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        void GetPivotColumn()
        {
            int minIndex = int.MaxValue;
            for (int j = 1; j < n - 1; j++)
            {
                if (Double.Parse(matrix[m - 1, j]) < 0)
                {
                    if (Int32.Parse(matrix[0, j].Substring(1)) < minIndex)
                    {
                        l = j;
                        minIndex = Int32.Parse(matrix[0, j].Substring(1));
                    }
                }
            }
        }

        bool IsOptimalInfinite()
        {
            for (int i = 1; i < m - 1; i++)
            {
                double x = Double.Parse(matrix[i, l]);
                if (Double.Parse(matrix[i, l]) > 0)
                    return false;
            }
            return true;
        }

        void GetPivotLine()
        {
            var minValue = Double.MaxValue;
            var minIndex = int.MaxValue;

            for (var i = 1; i < m - 1; i++)
            {
                if (Double.Parse(matrix[i, l]) > 0)
                {
                    var temp = Double.Parse(matrix[i, n - 1]) / Double.Parse(matrix[i, l]);
                    if (temp < minValue)
                    {
                        this.k = i;
                        minIndex = Int32.Parse(matrix[i, 0].Substring(1));
                        minValue = temp; 
                    }
                    else if(temp==minValue)
                    {
                        if (Int32.Parse(matrix[i, 0].Substring(1)) < minIndex)
                        {
                            this.k = i;
                            minIndex = Int32.Parse(matrix[i, 0].Substring(1));
                        }
                        minValue = temp; 
                    }
                }
            }
        }


        public void PrintSolution()
        {
            for (int j = 1; j < n - 1; j++)
            {
                Console.WriteLine(matrix[0, j] + " = 0");
            }

            for (int i = 1; i <m-1; i++)
            {
                if (m == n)
                {
                    Console.WriteLine(matrix[i, 0] + " = " + matrix[i, m - 1]);
                }
                else
                {
                    Console.WriteLine(matrix[i, 0] + " = " + matrix[i, m]);
                }
            }


            Console.WriteLine("The value of objective function is: " + matrix[m - 1, n - 1]);
        }

        public void PrintMatrix()
        {
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    Console.Write(string.Format("{0} ",matrix[i, j]));
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("-------------------------------------------------------");
        }
    }
}
