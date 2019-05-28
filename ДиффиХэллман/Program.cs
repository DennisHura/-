using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Numerics;

namespace lavrik_lab6_new
{
    class Program
    {
        static int GeneratePrime(int bitsAmount) //генерація простих чисел
        {
            var rand = new Random();
            int number = 0, count = 0;
            bool isPrime = false;
            int[] prime = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37,
                41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101,
                103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163,
                167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229,
                233, 239, 241, 251 };
            while (isPrime == false)
            {
                count++;
                number = rand.Next((int)Math.Pow(2, bitsAmount - 1), (int)Math.Pow(2, bitsAmount) - 1);
                if (number % 2 == 0)
                {
                    number += 1;
                }
                Console.WriteLine(number);
                isPrime = true;
                foreach (int i in prime)
                    if (number % i == 0)
                        isPrime = false;
            }
            Console.WriteLine("number found " + number + "");
            Console.WriteLine("checked " + count + " words");
            return number;
        }
        public static bool MillerRabinTest(int n, int k)//перевірка чисел на простоту(тест Рабіна-Міллера);
        {
            if (n < 2)
                return false;
            if (n == 2)
                return true;
            if (n % 2 == 0)
                return false;
            int r = 0, d = n - 1;
            while (d % 2 == 0)
            {
                d /= 2;
                r++;
            }
            var rand = new Random();

            for (int i = 0; i < k; i++)
            {
                int a = rand.Next(2, n - 1);
                BigInteger x = BigInteger.ModPow(a, d, n);
                if (x == 1 || x == n - 1)
                    continue;
                for (int j = 0; j < r - 1; j++)
                {
                    x = BigInteger.ModPow(x, 2, n);
                    if (x == 1)
                        return false;
                    if (x == n - 1)
                        break;
                }
                if (x != n - 1)
                    return false;
            }
            return true;
        }
        static void DiphiHellman(int n)//алгоритм Діффі-Хеллмана
        {
            Console.WriteLine("Алгоритм Диффи-Хэллмана\nВведите g, x(A) и x(B)");
            Console.Write("g=");// введення первісного кореня за модулем n
            int g = int.Parse(Console.ReadLine());
            Console.Write("x(A)=");
            int xa = int.Parse(Console.ReadLine());// введення закритого ключа абонента А
            while (xa > n)
            {
                Console.WriteLine("x(A) должно быть < n");
                Console.Write("x(A)=");
                xa = int.Parse(Console.ReadLine());
            }
            Console.Write("x(B)=");
            int xb = int.Parse(Console.ReadLine());// введення закритого ключа абонента В
            while (xb > n)
            {
                Console.WriteLine("x(B) должно быть < n");
                Console.WriteLine("x(B)=");
                xb = int.Parse(Console.ReadLine());
            }
            BigInteger ya = BigInteger.ModPow(g, xa, n); // обчислення відкритого ключа абонента А
            Console.WriteLine("открытый ключ y(A) = " + ya);
            BigInteger yb = BigInteger.ModPow(g, xb, n); // обчислення відкритого ключа абонента В
            Console.WriteLine("открытый ключ y(B) = " + yb);
            BigInteger k1 = BigInteger.ModPow(yb, xa, n); // обчислення секретного ключа абонента А 
            Console.WriteLine("общий ключ A = " + k1);
            BigInteger k2 = BigInteger.ModPow(ya, xb, n); // обчислення секретого ключа абонента В
            Console.WriteLine("общий ключ B = " + k2);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("какое количество битов должно быть в числе?");
            int bitsAmount = int.Parse(Console.ReadLine());
            Console.WriteLine("Сколько попыток должно быть в тесте Миилера Рабина ?");
            int millerTries = int.Parse(Console.ReadLine());
            DateTime one = DateTime.Now;
            int number = GeneratePrime(bitsAmount);

            if (MillerRabinTest(number, 10))
                Console.WriteLine("Тест Миллера: YES");
            else
            {
                Console.WriteLine("Тест Миллера: NO");
                Environment.Exit(0);
            }
            DateTime two = DateTime.Now;
            TimeSpan result = two - one;
            Console.WriteLine("время исполнения кода: " + result + " секунд");
            DiphiHellman(number);
        }
    }
}

