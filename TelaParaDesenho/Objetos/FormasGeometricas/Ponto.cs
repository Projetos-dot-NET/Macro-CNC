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
            return Sqrt(Abs(Pow(X, 2) + Pow(Y, 2)));
        }

        public void GirarDaOrigem(double anguloDeGiro)
        {
            //polar para cartesiano
            // x = r cos(a)
            // y = r sin(a)

            //cartesiano pra polar
            // a = acos (x/r) ou
            // a = asin (x/r)

            var radianosDeGiro = anguloDeGiro * (Math.PI / 180);

            var teste1 = Asin(Y / MedirRaio());
            var teste2 = Acos(X / MedirRaio());


            Y = MedirRaio() * Sin(teste1 + radianosDeGiro); 
            X = MedirRaio() * Cos(teste2 + radianosDeGiro);
        }
    }
}
