using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace Project
{
    class UI
    {
        public static void Main(string[] args)
        {
            bool Running = true;

            Console.WriteLine("Welcome to the SUVAT Calculator!");
            while (Running)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("Please enter the known values (enter 'x' for wanted, leave blank for unknown):");

                Console.Write("Displacement (S in m): ");
                var s = TryIntParse(Console.ReadLine());
                Console.Write("Initial Velocity (U in m/s): ");
                var u = TryIntParse(Console.ReadLine());
                Console.Write("Final Velocity (V in m/s): ");
                var v = TryIntParse(Console.ReadLine());
                Console.Write("Acceleration (A in m/s²): ");
                var a = TryIntParse(Console.ReadLine());
                Console.Write("Time (T in s): ");
                var t = TryIntParse(Console.ReadLine());

                Equations equation = new Equations(s ?? float.NaN, u ?? float.NaN, v ?? float.NaN, a ?? float.NaN, t ?? float.NaN);
                equation.WhichSetOfEquation();
                Running = false;
            }
        }

        public static float? TryIntParse(string Value)
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return float.NaN;
            }
            else if (Value.ToLower().Trim() == "x")
            {
                return float.PositiveInfinity;  // "x" = target variable to solve for
            }
            else if (float.TryParse(Value, out float result))
            {
                return result;
            }
            else
            {
                return float.NaN;
            }
        }
    }

    class Equations
    {
        class Variables
        {
            public const int S = 0;
            public const int U = 1;
            public const int V = 2;
            public const int A = 3;
            public const int T = 4;
        }

        private float result;
        private List<float> Values = new List<float>();
        private string[] variableNames = { "Displacement (S)", "Initial Velocity (U)", "Final Velocity (V)", "Acceleration (A)", "Time (T)" };

        public Equations(float s, float u, float v, float a, float t)
        {
            Values.Add(s);
            Values.Add(u);
            Values.Add(v);
            Values.Add(a);
            Values.Add(t);
        }

        public void WhichSetOfEquation()
        {
            for (int i = 0; i < 5; i++)
            {
                if (float.IsPositiveInfinity(Values[i]))
                {
                    Console.WriteLine($"Calculating for {variableNames[i]}...");
                    if (variableNames[i] == "Displacement (S)")
                    {
                        CalculateS();
                    }
                    else if (variableNames[i] == "Initial Velocity (U)")
                    {
                        CalculateU();
                    }
                    else if (variableNames[i] == "Final Velocity (V)")
                    {
                        CalculateV();
                    }
                    else if (variableNames[i] == "Acceleration (A)")
                    {
                        CalculateA();
                    }
                    else if (variableNames[i] == "Time (T)")
                    {
                        CalculateT();
                    }
                    break;
                }
            }
        }

        public void CalculateS()
        {
            if (!float.IsNaN(Values[Variables.U]) && !float.IsNaN(Values[Variables.T]) && !float.IsNaN(Values[Variables.A]))
            {
                result = Values[Variables.U] * Values[Variables.T] + 0.5f * Values[Variables.A] * Values[Variables.T] * Values[Variables.T];
            }
            else if (!float.IsNaN(Values[Variables.V]) && !float.IsNaN(Values[Variables.T]) && !float.IsNaN(Values[Variables.A]))
            {
                result = Values[Variables.V] * Values[Variables.T] - 0.5f * Values[Variables.A] * Values[Variables.T] * Values[Variables.T];
            }
            else if (!float.IsNaN(Values[Variables.U]) && !float.IsNaN(Values[Variables.V]) && !float.IsNaN(Values[Variables.T]))
            {
                result = 0.5f * (Values[Variables.U] + Values[Variables.V]) * Values[Variables.T];
            }
            else if (!float.IsNaN(Values[Variables.U]) && !float.IsNaN(Values[Variables.V]) && !float.IsNaN(Values[Variables.A]))
            {
                result = (Values[Variables.V] * Values[Variables.V] - Values[Variables.U] * Values[Variables.U]) / (2 * Values[Variables.A]);
            }
            else
            {
                Console.WriteLine("Insufficient data to calculate Displacement (S).");
            }

            Console.WriteLine($"Calculated Displacement (S): {result} m");
        }

        public void CalculateU()
        {
            if (!float.IsNaN(Values[Variables.S]) && !float.IsNaN(Values[Variables.T]) && !float.IsNaN(Values[Variables.A]))
            {
                result = (Values[Variables.S] - 0.5f * Values[Variables.A] * Values[Variables.T] * Values[Variables.T]) / Values[Variables.T];
            }
            else if (!float.IsNaN(Values[Variables.V]) && !float.IsNaN(Values[Variables.A]) && !float.IsNaN(Values[Variables.T]))
            {
                result = Values[Variables.V] - Values[Variables.A] * Values[Variables.T];
            }
            else if (!float.IsNaN(Values[Variables.S]) && !float.IsNaN(Values[Variables.V]) && !float.IsNaN(Values[Variables.T]))
            {
                result = (2 * Values[Variables.S] / Values[Variables.T]) - Values[Variables.V];
            }
            else if (!float.IsNaN(Values[Variables.V]) && !float.IsNaN(Values[Variables.A]) && !float.IsNaN(Values[Variables.S]))
            {
                result = (float)Math.Sqrt(Values[Variables.V] * Values[Variables.V] - 2 * Values[Variables.A] * Values[Variables.S]);
            }
            else
            {
                Console.WriteLine("Insufficient data to calculate Initial Velocity (U).");
            }

            Console.WriteLine($"Calculated Initial Velocity (U): {result} m/s");
        }

        public void CalculateV()
        {
            if (!float.IsNaN(Values[Variables.U]) && !float.IsNaN(Values[Variables.A]) && !float.IsNaN(Values[Variables.T]))
            {
                result = Values[Variables.U] + Values[Variables.A] * Values[Variables.T];
            }
            else if (!float.IsNaN(Values[Variables.S]) && !float.IsNaN(Values[Variables.U]) && !float.IsNaN(Values[Variables.T]))
            {
                result = (2 * Values[Variables.S] / Values[Variables.T]) - Values[Variables.U];
            }
            else if (!float.IsNaN(Values[Variables.U]) && !float.IsNaN(Values[Variables.A]) && !float.IsNaN(Values[Variables.S]))
            {
                result = (float)Math.Sqrt(Values[Variables.U] * Values[Variables.U] + 2 * Values[Variables.A] * Values[Variables.S]);
            }
            else
            {
                Console.WriteLine("Insufficient data to calculate Final Velocity (V).");
            }

            
            Console.WriteLine($"Calculated Final Velocity (V): {result} m/s");
        }

        public void CalculateA()
        {
            if (!float.IsNaN(Values[Variables.V]) && !float.IsNaN(Values[Variables.U]) && !float.IsNaN(Values[Variables.T]))
            {
                result = (Values[Variables.V] - Values[Variables.U]) / Values[Variables.T];
            }
            else if (!float.IsNaN(Values[Variables.S]) && !float.IsNaN(Values[Variables.U]) && !float.IsNaN(Values[Variables.T]))
            {
                result = (2 * (Values[Variables.S] - Values[Variables.U] * Values[Variables.T])) / (Values[Variables.T] * Values[Variables.T]);
            }
            else if (!float.IsNaN(Values[Variables.V]) && !float.IsNaN(Values[Variables.U]) && !float.IsNaN(Values[Variables.S]))
            {
                result = (Values[Variables.V] * Values[Variables.V] - Values[Variables.U] * Values[Variables.U]) / (2 * Values[Variables.S]);
            }
            else
            {
                Console.WriteLine("Insufficient data to calculate Acceleration (A).");
            }

        
            Console.WriteLine($"Calculated Acceleration (A): {result} m/s²");
        }

        public void CalculateT()
        {
            if (!float.IsNaN(Values[Variables.V]) && !float.IsNaN(Values[Variables.U]) && !float.IsNaN(Values[Variables.A]))
            {
                result = (Values[Variables.V] - Values[Variables.U]) / Values[Variables.A];
            }
            else if (!float.IsNaN(Values[Variables.S]) && !float.IsNaN(Values[Variables.U]) && !float.IsNaN(Values[Variables.A]))
            {
                float discriminant = Values[Variables.U] * Values[Variables.U] + 2 * Values[Variables.A] * Values[Variables.S];
                if (discriminant < 0)
                {
                    Console.WriteLine("No real solution for Time (T).");
                    return;
                }
                float sqrtDiscriminant = (float)Math.Sqrt(discriminant);
                float t1 = (-Values[Variables.U] + sqrtDiscriminant) / Values[Variables.A];
                float t2 = (-Values[Variables.U] - sqrtDiscriminant) / Values[Variables.A];
                result = Math.Max(t1, t2);
            }
            else if (!float.IsNaN(Values[Variables.S]) && !float.IsNaN(Values[Variables.U]) && !float.IsNaN(Values[Variables.V]))
            {
                result = (2 * Values[Variables.S]) / (Values[Variables.U] + Values[Variables.V]);
            }
            else
            {
                Console.WriteLine("Insufficient data to calculate Time (T).");
            }

        
            Console.WriteLine($"Calculated Time (T): {result} s");
        }
    }
}