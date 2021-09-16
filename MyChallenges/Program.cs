using System;

namespace MyChallenges
{
    class Program
    {
        static void Main(string[] args)
        {
            Challenge1.Resolve("1+1");
            Challenge1.Resolve("1 - 1");
            Challenge1.Resolve("1* 1");
            Challenge1.Resolve("2 + 5 * 10");
            Challenge1.Resolve("(1+1)");
            Challenge1.Resolve("(3*2) + 2 ");
            Challenge1.Resolve("12* 123");
            Challenge1.Resolve("25 / 5");

            Console.ReadKey();
        }
    }
}