using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furos.Classes
{
    public class Localizacao
    {
        public double DistanciaX { get; set; }
        public double DistanciaY { get; set; }


        public Localizacao() { }

        public Localizacao(double distanciaX, double distanciaY)
        {
            DistanciaX = distanciaX;
            DistanciaY = distanciaY;
        }

        public Localizacao(string distanciaX, string distanciaY)
        {
            DistanciaX = double.Parse(distanciaX);
            DistanciaY = double.Parse(distanciaY);
        }

    }
}
