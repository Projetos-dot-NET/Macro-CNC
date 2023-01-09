using static System.Math;

namespace TelaParaDesenho.Objetos.FormasGeometricas
{
    public class Linha
    {
        //atributos
        public Ponto Inicial { get; set; }
        public Ponto Final { get; set; }

        //construtor
        public Linha(Ponto inicio, Ponto fim)
        {
            this.Inicial = inicio;
            this.Final = fim;
        }

        //métodos
        public double MedirDistancia()
        {
            double catetoA = Final.X - Inicial.X;
            double catetoB = Final.X - Inicial.X;
            var hipotenusa = Sqrt(Abs(Pow(catetoA,2) + Pow(catetoB, 2)));

            return hipotenusa;
        }

        public void Mover(double x, double y)
        {
            Inicial.Mover(x,y);
            Final.Mover(x,y);
        }

        public void GirarDaOrigem(double anguloDeGiro)
        {
            Inicial.GirarDaOrigem(anguloDeGiro);
            Final.GirarDaOrigem(anguloDeGiro);
        }
    }
}
