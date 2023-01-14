
using TelaParaDesenho.Objetos.FormasGeometricas;

namespace TelaParaDesenho
{
    public partial class Form1 : Form
    {
        private int grau;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var pontoInicial = new Ponto(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox4.Text));
            var pontoFinal = new Ponto(Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox5.Text));
            var quadrado = new Quadrado(pontoInicial, pontoFinal);

            quadrado.GirarDaOrigem(Convert.ToInt16(anguloBox.Text));

            PointF infDir = new PointF((float)quadrado.InferiorDireito.X, (float)quadrado.InferiorDireito.Y);
            PointF infEsq = new PointF((float)quadrado.InferiorEsquerdo.X, (float)quadrado.InferiorEsquerdo.Y);
            PointF supDir = new PointF((float)quadrado.SuperiorDireito.X, (float)quadrado.SuperiorDireito.Y);
            PointF SupEsq = new PointF((float)quadrado.SuperiorEsquerdo.X, (float)quadrado.SuperiorEsquerdo.Y);

            Graphics grafico = pictureBox1.CreateGraphics();
            Pen canela_Amarelo = new Pen(Color.Yellow, 1);

            grafico.DrawLine(canela_Amarelo, supDir, SupEsq);
            grafico.DrawLine(canela_Amarelo, SupEsq, infEsq);
            grafico.DrawLine(canela_Amarelo, infEsq, infDir);
            grafico.DrawLine(canela_Amarelo, infDir, supDir);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void anguloBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}