using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace exam2022FP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //EX1();
            //Console.WriteLine(SumaPrimi(2939392));
            //int[,] matrix = GenerateMatrix();
            //WriteMatrix(matrix);
            //Console.WriteLine(EX3(matrix));
            //EX4();
            int[] arr = new int[] { 2, 3, 6, 7, 3, 2, 54, 6, 4, 43, 3, 5 };
            Console.WriteLine(BS(arr, 11));
        }
        static void EX1()
        {
            /*
             * Scrieți o funcție cu numele ListaNumere care citește o listă de numere naturale de la consolă
                (pe fiecare linie din intrare este un singur număr) până când se citește zero – care se
                consideră că nu face parte din lista numerelor ce trebuie prelucrate. Funcția determină cele
                mai mari trei numere din cele citite și le afișează pe ecran separate printr-un spațiu, în
                ordine descrescătoare. Dacă în intrare nu sunt cel puțin trei numere atunci se vor afișa toate
                numerele citite (cu excepția lui zero).*/
            int nr1 = Int32.MinValue;
            int nr2 = Int32.MinValue;
            int nr3 = Int32.MinValue;
            string s = Console.ReadLine();
            while (s != "0")
            {
                int input = int.Parse(s);
                if (input >= nr1)
                {
                    nr3 = nr2;
                    nr2 = nr1;
                    nr1 = input;
                }
                else if (input >= nr2)
                {
                    nr3 = nr2;
                    nr2 = input;
                }
                else if (input >= nr3)
                {
                    nr3 = input;
                }
                s = Console.ReadLine();
            }
            Console.WriteLine();
            if(nr1 != Int32.MinValue)
            {
                Console.Write(nr1 + " ");
            }
            if (nr2 != Int32.MinValue)
            {
                Console.Write(nr2 + " ");
            }
            if (nr3 != Int32.MinValue)
            {
                Console.Write(nr3 + " ");
            }
            Console.WriteLine();
        }
        /*  Scrieți o funcție cu numele SumaDivizoriPrimi care primește ca parametru un număr natural
            n (2 <= n <= 2.000.000.000) și întoarce suma divizorilor primi ai lui n. Pentru punctaj maxim
            trebuie să creați o implementare eficientă, care să rezolve problema “rapid” pentru valori
            mari ale lui n. Exemplu: Daca n este 360 funcția calculează și întoarce valoarea 10 pentru că
            360 = 2^3*3^2*5.  */
        static int SumaPrimi(int n) // EX2
        {
            int aux = n;
            int sum = 0;
            int i = 2;
            while(n > 1)
            {
                if(n % i == 0)
                {
                    sum += i;
                }
                while(n % i == 0)
                {
                    n /= i;
                }
                if (i == 2)
                {
                    i++;
                }
                else i += 2;
                if(i > aux/2)
                {
                    sum += aux;
                    break;
                }
            }
            return sum;
        }
        /*
         * 
         * Scrieți un program C# care citește de la tastatură un număr natural n și o matrice pătratică
            cu n linii și n coloane. Programul va calcula diferența dintre: suma elementelor pozitive de
            deasupra diagonalei principale și deasupra diagonalei secundare și suma elementelor
            negative de sub diagonala principală și sub diagonala secundară.
         */
        static int EX3(int[,] m)
        {
            int nord = 0;
            int sud = 0;
            int n = m.GetLength(0);
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if(i+j > n - 1 && i > j) // sud
                    {
                        if (m[i,j] < 0)
                        {
                            sud += m[i, j];
                        }                      
                    }
                    if(i < j && i + j < n - 1) // nord
                    {
                        if (m[i,j] > 0)
                        {
                            nord += m[i, j];
                        }
                    }
                }
            }

            return nord - sud;
        }
        static void WriteMatrix(int[,] v)
        {
            Console.WriteLine("~~");
            for (int i = 0; i < v.GetLength(1); i++)
            {
                for (int j = 0; j < v.GetLength(0); j++)
                {
                    Console.Write(v[i, j] + " ");

                }
                Console.WriteLine();

            }
            Console.WriteLine();
            Console.WriteLine("~~");
        }
        static int[,] GenerateMatrix()
        {
            Random rnd = new Random();
            int n = int.Parse(Console.ReadLine());
            int[,] m = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    m[i, j] = rnd.Next(-9, 10);
                }
            }
            return m;
        }
        /*
         * Scrieți un program care citește dintr-un fișier cu numele input.in un număr natural n care se
            află pe prima linie a fișierului și apoi un șir de n numere naturale care se află pe a doua linie
            a fișierului și sunt separate prin spații. Programul va scrie într-un fișier cu numele output.out
            numerele citite ordonate crescător după suma cifrelor lor, iar dacă suma cifrelor este egală,
            descrescător după valoarea lor. Exemplu:
            6
            102 60 51 600 3 21
            Se va scrie: 102 21 3 600 60 51
         * 
         * 
         */
        static void EX4()
        {
            int n;
            int[] v;
            using(StreamReader sr = new StreamReader("input.in"))
            {
                n = int.Parse(sr.ReadLine());               
                string[] tokens = sr.ReadLine().Split(' ');
                v = new int[tokens.Length];
                for(int i = 0; i < tokens.Length; i++)
                {
                    v[i] = int.Parse(tokens[i]);
                }
            }
            int[] weight = new int[v.Length];
            for(int i = 0; i < v.Length; i++)
            {
                int k = v[i];
                int w = 0;
                while(k > 0)
                {
                    w += k % 10;
                    k /= 10;
                }
                weight[i] = w;
            }
            for(int i = 0; i < v.Length - 1; i++)
            {
                for (int j = i + 1; j < v.Length; j++)
                {
                    if (weight[i] > weight[j])
                    {
                        (weight[i], weight[j]) = (weight[j], weight[i]);
                        (v[i], v[j]) = (v[j], v[i]);
                    }
                    if (weight[i] == weight[j])
                    {
                        if (v[i] < v[j])
                        {
                            (weight[i], weight[j]) = (weight[j], weight[i]);
                            (v[i], v[j]) = (v[j], v[i]);
                        }
                    }
                }                
            }
            for(int i = 0; i < v.Length; i++)
            {
                Console.Write(v[i]+ " ");
            }
        }
        static int BS(int[] arr, int t) // EX5
        {
            MergeSort(arr, 0, arr.Length - 1);
            int low = 0;
            int high = arr.Length - 1;
            while(low < high)
            {
                int mid = (low + high) / 2;
                if (arr[mid] < t)
                {
                    low = mid + 1;
                }
                else if (arr[mid] > t)
                {
                    high = mid - 1;
                }
                else
                {
                    while (mid > 0 && arr[mid] == arr[mid - 1]) // gaseste prima pozitie pe care se afla T , stiind ca T se afla in jurul indexului mid
                    {
                        mid -= 1;
                    }
                    return mid;
                }
                
            }
            return -1;
        }
        static void MergeSort(int[] arr, int left, int right)
        {
            if(left < right)
            {
                int mid = (left + right) / 2;

                MergeSort(arr, left, mid);
                MergeSort(arr, mid + 1, right);

                Merge(left, mid, right, arr);


            }
        }
        static void Merge(int left, int mid, int right, int[] arr)
        {
            int length1 = mid - left + 1;
            int length2 = right - mid;

            int[] Left = new int[length1];
            int[] Right = new int[length2];

            for(int i = 0; i < length1; i++)
            {
                Left[i] = arr[left + i];
            }
            for (int i = 0; i < length2; i++)
            {
                Right[i] = arr[mid + i + 1];
            }

            int lefta = 0;
            int righta = 0;
            int run = left;

            while (lefta < length1 && righta < length2)
            {
                if (Left[lefta] < Right[righta])
                {
                    arr[run] = Left[lefta];
                    lefta++;
                }
                else
                {
                    arr[run] = Right[righta];
                    righta++;
                }
                run++;
            }

            while(lefta < length1)
            {
                arr[run] = Left[lefta];
                lefta++;
                run++;
            }
            while(righta < length2)
            {
                arr[run] = Right[righta];
                righta++;
                run++;
            }

        }
    }
}
