using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelaParaDesenho.Objetos.FormasGeometricas
{
    public class QuadradoRuim
    {
        //atributos
        public Linha Superior { get; set; }
        public Linha Inferior { get; set; }
        public Linha Esquerda { get; set; }
        public Linha Direita { get; set; }

        //Construtor
        public QuadradoRuim(Ponto inicio, Ponto fim)
        {
            var arestaSuperior = new Ponto(inicio.X, fim.Y);
            var arestaInferior = new Ponto(fim.X, inicio.Y);

            Superior = new Linha(arestaSuperior, fim);
            Inferior = new Linha(inicio, arestaInferior);
            Esquerda = new Linha(inicio, arestaSuperior);
            Direita = new Linha(arestaInferior, fim);
        }
    }
}
