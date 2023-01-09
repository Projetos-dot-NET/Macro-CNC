using static System.Math;

namespace TelaParaDesenho.Objetos.FormasGeometricas
{
    public class Quadrado
    {
        //atributos
        public Ponto SuperiorDireito { get; set; }
        public Ponto SuperiorEsquerdo { get; set; }
        public Ponto InferiorDireito { get; set; }
        public Ponto InferiorEsquerdo { get; set; }

        //construtor
        public Quadrado(Ponto inicio, Ponto fim)
        {
            InferiorEsquerdo = inicio;
            SuperiorDireito = fim;

            InferiorDireito = new Ponto(fim.X, inicio.Y);
            SuperiorEsquerdo = new Ponto(inicio.X, fim.Y); ;
        }

        //métodos
        public double MedirPerimetro()
        {
            var superior = new Linha(SuperiorEsquerdo, SuperiorDireito);
            var inferior = new Linha(SuperiorEsquerdo, SuperiorDireito);
            var esquerda = new Linha(SuperiorEsquerdo, SuperiorDireito);
            var direita = new Linha(SuperiorEsquerdo, SuperiorDireito);

            var perimetro = superior.MedirDistancia() + inferior.MedirDistancia() + direita.MedirDistancia() + esquerda.MedirDistancia();
            return perimetro;
        }

        public double MedirAreaTotal()
        {
            var inferior = new Linha(SuperiorEsquerdo, SuperiorDireito);
            var esquerda = new Linha(SuperiorEsquerdo, SuperiorDireito);

            var area = inferior.MedirDistancia() * esquerda.MedirDistancia();
            return area;
        }

        public void Mover(double x, double y)
        {
            SuperiorDireito.Mover(x, y);
            SuperiorEsquerdo.Mover(x, y);
            InferiorDireito.Mover(x, y);
            InferiorEsquerdo.Mover(x, y);
        }

        public void GirarDaOrigem(double anguloDeGiro)
        {
            SuperiorDireito.GirarDaOrigem(anguloDeGiro);
            SuperiorEsquerdo.GirarDaOrigem(anguloDeGiro);
            InferiorDireito.GirarDaOrigem(anguloDeGiro);
            InferiorEsquerdo.GirarDaOrigem(anguloDeGiro);
        }

    }
}
