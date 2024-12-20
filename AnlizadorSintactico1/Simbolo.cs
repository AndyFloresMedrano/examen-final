using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnlizadorSintactico1
{
    internal class Simbolo
    {
        public string Lexema { get; set; }
        public string Token { get; set; }

        public Simbolo(string lexema, string token)
        {
            Lexema = lexema;
            Token = token;
        }
    }
}