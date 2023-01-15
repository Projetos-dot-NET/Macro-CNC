using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furos.Classes
{
    public class MatrizCircular
    {
        public double Raio { get; set; }
        public double NumeroFuros { get; set; }
        public double GrauRotacao { get; set; }

        public MatrizCircular() { }
        /*
        public MatrizCircular(double r, int nF, double gR)
        {
            Raio = r;
            NumeroFuros = nF;
            GrauRotacao = gR;
        }
        */
        public MatrizCircular(string r, string nF, string gR)
        {
            Raio = double.Parse(r);
            NumeroFuros = double.Parse(nF);
            GrauRotacao = double.Parse(gR);
        }
    }
}
