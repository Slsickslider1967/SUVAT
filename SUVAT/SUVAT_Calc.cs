using System.Runtime.CompilerServices;

namespace Project
{
    class UI
    {
        public static void Main()
        {
            bool Running = true;

            Console.WriteLine("Welcome to the SUVAT Calculator!");
            while (Running)
            {
                Console.WriteLine("------------------------------");
                Running = false;
            }
        }
        
    }

    class Equations
    {
        private float S;
        private float U;
        private float V;
        private float A;
        private float T;
        private float result;

        public Equations(float s, float u, float v, float a, float t)
        {
            S = s;
            U = u;
            V = v;
            A = a;
            T = t;
        }

        public void WhichEquation()
        {
            
        }
    }
}