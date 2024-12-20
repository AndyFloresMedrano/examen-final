using AnlizadorSintactico1;
using System;
using System.Collections.Generic;

namespace AnalizadorSintactico1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ANALIZADOR SINTÁCTICO");
            Console.WriteLine("Ejemplos de entradas válidas:");
            Console.WriteLine("- home/user/documents/compi.txt");
            Console.WriteLine("- user/documents");
            Console.WriteLine("- compi/home.txt");
            Console.WriteLine();

            while (true)
            {
                Console.Write("Ingrese un camino (o escriba 'salir' para terminar): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "salir")
                {
                    Console.WriteLine("Programa terminado.");
                    break;
                }

                var simbolos = AnalizadorLexico(input);

                if (simbolos != null)
                {
                    Console.WriteLine("Símbolos:");
                    foreach (var simbolo in simbolos)
                    {
                        Console.WriteLine(simbolo);
                    }

                    if (AnalizadorSintactico(simbolos))
                    {
                        Console.WriteLine($"Camino '{input}' válido.");
                    }
                    else
                    {
                        Console.WriteLine($"Camino '{input}' inválido.");
                    }
                }
                else
                {
                    Console.WriteLine("Error léxico: Entrada inválida.");
                }
            }
        }

      
        static List<Simbolo> AnalizadorLexico(string entrada)
        {
            var simbolos = new List<Simbolo>();
            var partes = entrada.Split('/'); 

            foreach (var parte in partes)
            {
                if (parte.Contains('.')) 
                {
                    var archivo = parte.Split('.');
                    if (archivo.Length == 2 && EsNombreArchivo(archivo[0]) && EsExtensionValida(archivo[1]))
                    {
                        simbolos.Add(new Simbolo(archivo[0], "NombreArchivo"));
                        simbolos.Add(new Simbolo(archivo[1], "Extension"));
                    }
                    else
                    {
                        return null; 
                    }
                }
                else if (EsDirectorio(parte))
                {
                    simbolos.Add(new Simbolo(parte, "Directorio"));
                }
                else
                {
                    return null; 
                }
            }

            return simbolos;
        }

        static bool AnalizadorSintactico(List<Simbolo> simbolos)
        {
            int index = 0;
            
            if (!Dir(simbolos, ref index)) return false;

            while (index < simbolos.Count && simbolos[index].Token == "Directorio")
            {
                if (!Dir(simbolos, ref index)) return false;
            }

            if (index < simbolos.Count && simbolos[index].Token == "NombreArchivo")
            {
                return File(simbolos, ref index);
            }

            return index == simbolos.Count; 
        }

        static bool Dir(List<Simbolo> simbolos, ref int index)
        {
            if (index < simbolos.Count && simbolos[index].Token == "Directorio")
            {
                index++;
                return true;
            }

            return false;
        }

        static bool File(List<Simbolo> simbolos, ref int index)
        {
            if (index < simbolos.Count && simbolos[index].Token == "NombreArchivo")
            {
                index++; 

                if (index < simbolos.Count && simbolos[index].Token == "Extension")
                {
                    index++;
                    return true;
                }
            }

            return false;
        }

        static bool EsDirectorio(string token)
        {
            return token == "home" || token == "user" || token == "documents" || token == "compi";
        }

        static bool EsNombreArchivo(string token)
        {
            return token == "compi"; 
        }

        static bool EsExtensionValida(string token)
        {
            return token == "txt" || token == "docx" || token == "pdf";
        }
    }
}
