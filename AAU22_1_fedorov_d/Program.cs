using System;
using System.Linq;
using System.Security.Policy;

class FormalNeuronDemo {
    class Neuron
    {
        double a;
        double b;
        public double[] w = {0, 0, 0, 0};
        public int c = 0;
        int d;
        
        int DialogUsers()
        {
            Console.Write("Введите темп обучения (пример: 1 или 1,2 и т.п.) и нажмите клавишу Enter: " + Environment.NewLine);
            string temporaryValue = Console.ReadLine();
            //double step;
            bool isDouble = double.TryParse(temporaryValue, out a);
            if (isDouble) a = Convert.ToDouble(temporaryValue);
            else
            {
                Console.Write("Вы ввели не число, значение подставлено автоматически (0.02)" + Environment.NewLine);
                a = 0.02;
            }
            Console.Write("Введите задержку (пример: 1 или 1,2 и т.п.) и нажмите клавишу Enter: " + Environment.NewLine);
            temporaryValue = Console.ReadLine();
            //double delay;
            isDouble = double.TryParse(temporaryValue, out b);
            if (isDouble) b = Convert.ToDouble(temporaryValue);
            else
            {
                Console.Write("Вы ввели не число, значение подставлено автоматически (-0.4)" + Environment.NewLine);
                b = -0.4;
            }
            Console.Write("Введите число повторений (прием: 1, 2, 3, 4 и т.п.) и нажмите клавишу Enter: " + Environment.NewLine);
            temporaryValue = Console.ReadLine();
            //int reiterative;
            isDouble = int.TryParse(temporaryValue, out d);
            if (isDouble) d = Convert.ToInt32(temporaryValue);
            else
            {
                Console.Write("Вы ввели не целое число, значение подставлено автоматически (10000)" + Environment.NewLine);
                d = 10000;
            }

            if (d < 0)
            {
                Console.Write("(" + d + ")" + " Исправлено на положительное число" + Environment.NewLine);
                d *= -1;
            }
            return 0;
        }

        public Neuron(int [][] X, int [] Y)
        {
            DialogUsers();
            while (Learn(X, Y)) { 
                if (c++ > d) break;
            }
        }

        public double calculate(int [] x) {
            double s = b; 
            for (int i = 0; i < w.Length; i++) s += w[i] * x[i];
            return (s > 0) ? 1 : 0;
        }
		
        bool Learn(int [][] X, int [] Y) { 
            double[] w_ = new double[w.Length]; 

            Array.Copy(w, w_, w.Length); 

            int i = 0;
            foreach (int[] x in X) {
                int y = Y[i++];
                for (int j=0; j < x.Length; j++) {
                    w[j] += a * (y - calculate(x)) * x[j];
                }
            }
            return !Enumerable.SequenceEqual(w_, w);
        }

    }
    
    static int[][] X = {
        new int [] {0, 0, 0, 0}, 
        new int [] {0, 0, 0, 1},
        new int [] {1, 1, 1, 0},
        new int [] {1, 1, 1, 0},
        new int [] {1, 1, 1, 1}
    };

    static int[] Y = {0, 1, 1, 0, 1};

    static int[][] Test = {
        new int [] {0, 0, 0, 0}, 
        new int [] {0, 0, 0, 1}, 
        new int [] {0, 1, 0, 1},
        new int [] {0, 1, 1, 0},
        new int [] {1, 1, 1, 0}, 
        new int [] {1, 1, 1, 1}
    };
    public static int Main()
    {
        Neuron neuron = new Neuron(X, Y); 
        Console.WriteLine("[{0}] {1}", 
            string.Join(", ", neuron.w), 
            neuron.c
        );

        foreach(int[] test in Test) { 
            Console.WriteLine("[{0}] {1} {2}", 
                string.Join(", ", test), 
                test[3],
                neuron.calculate(test)
            );
        }
        return 0;
    }
}