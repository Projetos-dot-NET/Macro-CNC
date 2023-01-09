
using TelaParaDesenho.Objetos.FormasGeometricas;

namespace TelaParaDesenho
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var pontoInicial = new Ponto(159,159);
            var pontoFinal = new Ponto(110, 110);
            var quadrado = new Quadrado(pontoInicial, pontoFinal);

            quadrado.GirarDaOrigem(0);

            PointF infDir = new PointF((float)quadrado.InferiorDireito.X, (float)quadrado.InferiorDireito.Y);
            PointF infEsq = new PointF((float)quadrado.InferiorEsquerdo.X, (float)quadrado.InferiorEsquerdo.Y);
            PointF supDir = new PointF((float)quadrado.SuperiorDireito.X, (float)quadrado.SuperiorDireito.Y);
            PointF SupEsq = new PointF((float)quadrado.SuperiorEsquerdo.X, (float)quadrado.SuperiorEsquerdo.Y);

            Graphics g = pictureBox1.CreateGraphics();
            Pen canela_Amarelo = new Pen(Color.Yellow, 1);

            g.DrawLine(canela_Amarelo, supDir, SupEsq);
            g.DrawLine(canela_Amarelo, SupEsq, infEsq); 
            g.DrawLine(canela_Amarelo, infEsq, infDir);
            g.DrawLine(canela_Amarelo, infDir, supDir);

            quadrado.GirarDaOrigem(-5);

            infDir = new PointF((float)quadrado.InferiorDireito.X, (float)quadrado.InferiorDireito.Y);
            infEsq = new PointF((float)quadrado.InferiorEsquerdo.X, (float)quadrado.InferiorEsquerdo.Y);
            supDir = new PointF((float)quadrado.SuperiorDireito.X, (float)quadrado.SuperiorDireito.Y);
            SupEsq = new PointF((float)quadrado.SuperiorEsquerdo.X, (float)quadrado.SuperiorEsquerdo.Y);

            g.DrawLine(canela_Amarelo, supDir, SupEsq);
            g.DrawLine(canela_Amarelo, SupEsq, infEsq);
            g.DrawLine(canela_Amarelo, infEsq, infDir);
            g.DrawLine(canela_Amarelo, infDir, supDir);

            quadrado.GirarDaOrigem(-5);

            infDir = new PointF((float)quadrado.InferiorDireito.X, (float)quadrado.InferiorDireito.Y);
            infEsq = new PointF((float)quadrado.InferiorEsquerdo.X, (float)quadrado.InferiorEsquerdo.Y);
            supDir = new PointF((float)quadrado.SuperiorDireito.X, (float)quadrado.SuperiorDireito.Y);
            SupEsq = new PointF((float)quadrado.SuperiorEsquerdo.X, (float)quadrado.SuperiorEsquerdo.Y);

            g.DrawLine(canela_Amarelo, supDir, SupEsq);
            g.DrawLine(canela_Amarelo, SupEsq, infEsq);
            g.DrawLine(canela_Amarelo, infEsq, infDir);
            g.DrawLine(canela_Amarelo, infDir, supDir);
        }
    }
}