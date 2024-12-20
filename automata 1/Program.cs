using automata_1;
using System;

namespace automata1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== AUTÓMATA FINITO ===");
            Console.WriteLine("Valida cadenas según el patrón: m(h|a)*");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Ingrese una cadena (o escriba 'salir' para terminar): ");
                string source = Console.ReadLine();

                if (source.ToLower() == "salir")
                {
                    Console.WriteLine("Programa terminado.");
                    break;
                }

                bool esValido = ValidarCadena(source);
                if (esValido)
                {
                    Console.WriteLine($"Cadena '{source}' aceptada.");
                }
                else
                {
                    Console.WriteLine($"Cadena '{source}' no aceptada.");
                }
            }
        }

        private static bool ValidarCadena(string source)
        {
            Estado1 estadoActual = Estado1.Q0;

            foreach (char c in source)
            {
                switch (estadoActual)
                {
                    case Estado1.Q0:
                        if (c == 'm')
                            estadoActual = Estado1.Q1;
                        else
                            return false;
                        break;

                    case Estado1.Q1:
                        if (c == 'h')
                            estadoActual = Estado1.Q3;
                        else if (c == 'a')
                            estadoActual = Estado1.Q2;
                        else
                            return false;
                        break;

                    case Estado1.Q2:
                        if (c == 'a')
                            estadoActual = Estado1.Q2;
                        else if (c == 'm')
                            estadoActual = Estado1.Q1;
                        else if (c == 'h')
                            estadoActual = Estado1.Q3;
                        else
                            return false;
                        break;

                    case Estado1.Q3:
                        if (c == 'a')
                            estadoActual = Estado1.Q2;
                        else
                            return false;
                        break;

                    default:
                        return false;
                }
            }

            return estadoActual == Estado1.Q2 || estadoActual == Estado1.Q3;
        }
    }
}

