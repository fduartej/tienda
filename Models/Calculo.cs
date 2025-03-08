using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apptienda.Models
{
    public class Calculo
    {
        public string? Operacion  { get; set; }
        public double Operador1   { get; set; }
        public double Operador2   { get; set; }

        public double Calcular()
        {
            double resultado = 0;
            switch (Operacion)
            {
                case "suma":
                    resultado = Operador1 + Operador2;
                    break;
                case "resta":
                    resultado = Operador1 - Operador2;
                    break;
                case "multiplicar":
                    resultado = Operador1 * Operador2;
                    break;
                case "dividir":
                    if(Operador2 == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    resultado = Operador1 / Operador2;
                    break;
            }
            return resultado;
        }
    }
}