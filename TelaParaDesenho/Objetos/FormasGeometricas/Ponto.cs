using static System.Math;

namespace TelaParaDesenho.Objetos.FormasGeometricas
{
    public class Ponto
    {
        //atributos
        public double X { get; set; }
        public double Y { get; set; }

        //construtor
        public Ponto(double x, double y)
        {
            X = x;
            Y = y;
        }

        //métodos
        public void Mover(double x, double y)
        {
            X += x;
            Y += y;
        }

        public double MedirRaio()
        {
            return Sqrt(Pow(X, 2) + Pow(Y, 2));
        }

        public void GirarDaOrigem(double anguloDeGiro)
        {
            //polar para cartesiano
            // x = r cos(a)
            // y = r sin(a)

            //cartesiano pra polar
            // a = acos (x/r) ou
            // a = asin (y/r) ou
            // a - atan (y/x)

            var raio = MedirRaio();
            var radianoDeGiro = anguloDeGiro * (PI / 180.0F);
            var radiano = Atan(Y / X);

            Y = raio * Sin(radiano + radianoDeGiro); 
            X = raio * Cos(radiano + radianoDeGiro);
        }
    }
}
