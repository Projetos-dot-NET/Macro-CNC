using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furos.Classes
{
    public class MatrizRetangular
    {
        public int NumeroFurosHorizontal { get; set; }
        public int NumeroFurosVertical { get; set; }
        public double IntervaloHorizontal { get; set; }
        public double IntervaloVertical { get; set; }

        public MatrizRetangular() { }

        public MatrizRetangular(int nFH, int nFV, double iH, double iV)
        {
            NumeroFurosHorizontal = nFH;
            NumeroFurosVertical = nFV;
            IntervaloHorizontal= iH;
            IntervaloVertical= iV;
        }

        public MatrizRetangular(string nFH, string nFV, string iH, string iV )
        {
            NumeroFurosHorizontal = int.Parse(nFH);
            NumeroFurosVertical = int.Parse(nFV);
            IntervaloHorizontal = double.Parse(iH);
            IntervaloVertical = double.Parse(iV);
        }
    }
}
