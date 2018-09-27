using RefPipelines;
using System;

namespace BindingRepro
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Going boom...");
            Hammer.CantTouchThis();
            Console.WriteLine("Shouldn't get here...");
        }
    }
}
