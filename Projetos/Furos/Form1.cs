using Furos.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using Image = System.Drawing.Image;

namespace Furos
{
    public partial class Form1 : Form
    {
        //decimal[,] Matriz = new decimal[50, 2];

        //decimal x = 0;
        //decimal y = 0;
        decimal z = 0;
        decimal q = 0;
        decimal z_mergulho = 0;
        decimal retracao = 0;

        public Form1()
        {
            InitializeComponent();
        }
        public string diretorio;

        //Arquivo de escrita 
        public TextWriter arq;
        public Boolean item_selecionado =false;
        /***********************************************************************************/
        Image ZoomPicture(Image img, Size size)
        {
            Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width * size.Width),
                Convert.ToInt32(img.Height * size.Height));
            Graphics gpu = Graphics.FromImage(bm);
            gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bm;
        }

        private PictureBox org;
        private int f;

        public delegate void KeyPressEventHandler(object  sender, KeyPressEventArgs e);

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value != 0)
            {
                picture_Tela.Image = null;
                picture_Tela.Image = ZoomPicture(org.Image, new Size(trackBar1.Value, trackBar1.Value));
            }
        }
        /*************************************************************************************************/
        private void Form1_Load(object sender, EventArgs e)
        {
            picture_Tela.Width = 600;
            picture_Tela.Height = 600;


            trackBar1.Minimum = 1;
            trackBar1.Maximum = 6;
            trackBar1.SmallChange = 1;
            trackBar1.LargeChange = 1;
            trackBar1.UseWaitCursor = false;

            this.DoubleBuffered = true;
            org = new PictureBox();
            org.Image = picture_Tela.Image;
        }



        public float PegaCoord_X(int linha_do_List)
        {

            float valor;
            string Linha = list_brocas.Items[linha_do_List].ToString();
            Linha = Linha.ToUpper();
            Linha = Linha.Trim();

            valor = (float)Convert.ToDecimal(Linha.Split('X')[1].Split('Y')[0]);
            return valor;
        }

        public float PegaCoord_Y(int linha_do_List)
        {

            float valor;
            string Linha = list_brocas.Items[linha_do_List].ToString();
            Linha = Linha.ToUpper();
            Linha = Linha.Trim();

            valor = (float)Convert.ToDecimal(Linha.Split('Y')[1]);
            return valor;
        }




        public float PegaCoord_aux_X(int linha_do_List)
        {

            float valor;
            string Linha = list_aux.Items[linha_do_List].ToString();
            Linha = Linha.ToUpper();
            Linha = Linha.Trim();

            valor = (float)Convert.ToDecimal(Linha.Split('X')[1].Split('Y')[0]);
            return valor;
        }

        public float PegaCoord_aux_Y(int linha_do_List)
        {

            float valor;
            string Linha = list_aux.Items[linha_do_List].ToString();
            Linha = Linha.ToUpper();
            Linha = Linha.Trim();

            valor = (float)Convert.ToDecimal(Linha.Split('Y')[1]);
            return valor;
        }



        public void desenhaUCS()
        {
            decimal ucsX = Convert.ToDecimal(text_x.Text);
            decimal ucsY = Convert.ToDecimal(text_y.Text);

            if (ucsX == 0 && ucsY == 0)
            {
                Graphics g = picture_Tela.CreateGraphics();
                Pen canela_Amarelo = new Pen(Color.Yellow, 1);

                PointF pnt1 = new PointF(280.0F, 300.0F);
                PointF pnt2 = new PointF(320.0F, 300.0F);
                g.DrawLine(canela_Amarelo, pnt1, pnt2);

                PointF pnt3 = new PointF(300.0F, 280.0F);
                PointF pnt4 = new PointF(300.0F, 320.0F);
                g.DrawLine(canela_Amarelo, pnt3, pnt4);
            }
            if (ucsX > 0 && ucsY > 0)
            {
                Graphics g = picture_Tela.CreateGraphics();
                Pen canela_Amarelo = new Pen(Color.Yellow, 1);

                PointF pnt1 = new PointF(20.0F, 590.0F);
                PointF pnt2 = new PointF(20.0F, 530.0F);
                g.DrawLine(canela_Amarelo, pnt1, pnt2);

                PointF pnt3 = new PointF(10.0F, 580.0F);
                PointF pnt4 = new PointF(70.0F, 580.0F);
                g.DrawLine(canela_Amarelo, pnt3, pnt4);
            }
            if (ucsX >= 0 && ucsY < 0)
            {
                Graphics g = picture_Tela.CreateGraphics();
                Pen canela_Amarelo = new Pen(Color.Yellow, 1);

                PointF pnt1 = new PointF(20.0F, 10.0F);
                PointF pnt2 = new PointF(20.0F, 70.0F);
                g.DrawLine(canela_Amarelo, pnt1, pnt2);

                PointF pnt3 = new PointF(10.0F, 20.0F);
                PointF pnt4 = new PointF(70.0F, 20.0F);
                g.DrawLine(canela_Amarelo, pnt3, pnt4);
            }
            if (ucsX < 0 && ucsY < 0) //quarto quadrante 
            {
                Graphics g = picture_Tela.CreateGraphics();
                Pen canela_Amarelo = new Pen(Color.Yellow, 1);

                PointF pnt1 = new PointF(580.0F, 10.0F);
                PointF pnt2 = new PointF(580.0F, 70.0F);
                g.DrawLine(canela_Amarelo, pnt1, pnt2);

                PointF pnt3 = new PointF(530.0F, 20.0F);
                PointF pnt4 = new PointF(590.0F, 20.0F);
                g.DrawLine(canela_Amarelo, pnt3, pnt4);
            }
            if (ucsX < 0 && ucsY >= 0) //terceiro quadrante 
            {
                Graphics g = picture_Tela.CreateGraphics();
                Pen canela_Amarelo = new Pen(Color.Yellow, 1);

                PointF pnt1 = new PointF(580.0F, 590.0F);
                PointF pnt2 = new PointF(580.0F, 530.0F);
                g.DrawLine(canela_Amarelo, pnt1, pnt2);

                PointF pnt3 = new PointF(590.0F, 580.0F);
                PointF pnt4 = new PointF(530.0F, 580.0F);
                g.DrawLine(canela_Amarelo, pnt3, pnt4);
            }
        }

        public void Desenha_Percurso()
        {
            decimal X1;
            decimal Y1;
            int f;
            _ = 360 / Convert.ToDouble(text_quant.Text);
            decimal dia_broca = Convert.ToDecimal(text_dia_f.Text);
            double quant = list_brocas.Items.Count; //Convert.ToDouble(text_quant.Text);
            float raio_broca = (float)dia_broca / 2;
            int linhaList = 0;

            decimal ucsX = Convert.ToDecimal(text_x.Text);
            decimal ucsY = Convert.ToDecimal(text_y.Text);
            float pZeroX = 0;
            float pZeroY = 0;
            float pontoX = 0;
            float pontoY = 0;
            var ponto1 = new PointF();
            var ponto2 = new PointF();

            Graphics g = picture_Tela.CreateGraphics();
            Pen caneta_Amarelo = new Pen(Color.Yellow, 1);
            Pen caneta_Vermelho = new Pen(Color.Red, 1);
            //Brush Black = new SolidBrush(Color.Black);

            float[] dashValues = { 5, 5 };

            Pen caneta_Branco = new Pen(Color.White, 1);

            //g.FillRectangle(Black, 0, 0, 600, 600);
            picture_Tela.Refresh();
            f = 0;
            //caneta_Vermelho.DashPattern = dashValues;
            caneta_Branco.DashPattern = dashValues;
            desenhaUCS();
            list_aux.Items.Clear();
            while (f < quant)
            {
                X1 = (decimal)PegaCoord_X(linhaList);
                Y1 = (decimal)PegaCoord_Y(linhaList);
                if (X1 == 0 && Y1 == 0) break;
                if (f == 0)
                {
                    if (ucsX == 0 && Math.Abs(ucsY) > 0) { MessageBox.Show("Valor de X fora da mesa", "Atenção"); break; }
                    if (Math.Abs(ucsX) > 0 && ucsY == 0) { MessageBox.Show("Valor de Y fora da mesa", "Atenção"); break; }
                }

                if (ucsX == 0 && ucsY == 0) //(0,0)ponto central
                {
                    if (f == 0)
                    {
                        pZeroX = 300.0F;
                        pZeroY = 300.0F;
                        ponto1.X = pZeroX;
                        ponto1.Y = pZeroY;
                    }
                    pontoX = 300 + ((float)X1 - (float)raio_broca);
                    pontoY = 300 - ((float)Y1 + (float)raio_broca);
                }
                if (ucsX > 0 && ucsY < 0) //(50,-50)primeiro guadrante
                {
                    if (f == 0)
                    {
                        pZeroX = 20.0F;
                        pZeroY = 20.0F;
                        ponto1.X = pZeroX;
                        ponto1.Y = pZeroY;
                    }
                    pontoX = 20 + ((float)X1 - (float)raio_broca);
                    pontoY = 20 - ((float)Y1 - (float)raio_broca);
                }

                if (ucsX > 0 && ucsY > 0) //(50,50)segundo guadrante
                {
                    if (f == 0)
                    {
                        pZeroX = 20.0F;
                        pZeroY = 580.0F;
                        ponto1.X = pZeroX;
                        ponto1.Y = pZeroY;
                    }
                    pontoX = 20 + ((float)X1 - (float)raio_broca);
                    pontoY = 580 - ((float)Y1 + (float)raio_broca);
                }
                if (ucsX < 0 && ucsY > 0) //(-50,+50)terceiro guadrante
                {
                    if (f == 0)
                    {
                        pZeroX = 580.0F;
                        pZeroY = 580.0F;
                        ponto1.X = pZeroX;
                        ponto1.Y = pZeroY;
                    }
                    pontoX = 580 + ((float)X1 - (float)raio_broca);
                    pontoY = 580 - ((float)Y1 - (float)raio_broca);
                }
                if (ucsX < 0 && ucsY < 0) //(-50,-50)quarto guadrante
                {
                    if (f == 0)
                    {
                        pZeroX = 580.0F;
                        pZeroY = 20.0F;
                        ponto1.X = pZeroX;
                        ponto1.Y = pZeroY;
                    }
                    pontoX = 580 + ((float)X1 - (float)raio_broca);
                    pontoY = 20 - ((float)Y1 + (float)raio_broca);
                }
                g.DrawEllipse(caneta_Amarelo, pontoX, pontoY, (float)dia_broca, (float)dia_broca);
                
                //Copia a lista
                list_aux.Items.Add("X " + pontoX + "Y " + pontoY); 

                pontoX += (float)raio_broca;
                pontoY += (float)raio_broca;

                ponto2.X = pontoX;
                ponto2.Y = pontoY;

                if (f == 0)
                {
                    g.DrawLine(caneta_Vermelho, ponto1, ponto2);
                }
                else
                {
                    g.DrawLine(caneta_Branco, ponto1, ponto2);
                }
                ponto1 = ponto2;
                f += 1;
                linhaList++;
            }

            ponto2.X = pZeroX;
            ponto2.Y = pZeroY;

            g.DrawLine(caneta_Branco, ponto1, ponto2);

        }



        private void Escreve_cabecalho()
        {
            //Cabeçalho
            text_comando.Text = "G17 G21 G90 (Plano XY - metrico - absoluto)" + Environment.NewLine;
            z = Convert.ToDecimal(text_Z_troca.Text);
            text_comando.Text = text_comando.Text + "G00 Z" + Math.Round(z, 4) + " (Z para troca de ferramenta )" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "G00 X0.000 Y0.000 (zero peca)" + Environment.NewLine;

            //text_comando.Text = text_comando.Text + "S " + rpm + " (velocidade spidle)" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "M00 (broca de " + text_dia_f.Text + "mm, troca manual)" + Environment.NewLine;
            string rpm = text_rpm.Text;
            text_comando.Text = text_comando.Text + "M03 S" + rpm + " (liga spindle)" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "G04 P3.000 (pausa por 3 segundos)" + Environment.NewLine;
            z = Convert.ToDecimal(text_seg.Text);
            text_comando.Text = text_comando.Text + "G00 Z" + Math.Round(z, 4) + " (Aproximacao)" + Environment.NewLine;
        }

        private void Escreve_rodape()
        {
            //Rodapé
            z = 25;
            text_comando.Text = text_comando.Text + "G00 X0.000 Y0.000 Z" + z + Environment.NewLine;
            text_comando.Text = text_comando.Text + "M05" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "M30" + Environment.NewLine;
            text_comando.Text = text_comando.Text.Replace(",", ".");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var localizacao = new Localizacao(text_x.Text, text_y.Text);

            list_brocas.Items.Add(" X" + localizacao.DistanciaX.ToString()+ " Y" + localizacao.DistanciaY.ToString());

            Desenha_Percurso();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (list_brocas.Items.Count != 0)
            {
                text_dia_f.Text = text_dia_f.Text.Replace(".", ",");
                text_av_vertical.Text = text_av_vertical.Text.Replace(".", ",");
                text_seg.Text = text_seg.Text.Replace(".", ",");
                text_mergulho.Text = text_mergulho.Text.Replace(".", ",");
                text_retracao.Text = text_retracao.Text.Replace(".", ",");
                text_x.Text = text_x.Text.Replace(".", ",");
                text_y.Text = text_y.Text.Replace(".", ",");
                text_final_z.Text = text_final_z.Text.Replace(".", ",");
                text_final_z.Text = text_final_z.Text.Replace(".", ",");
                text_aprox_z.Text = text_aprox_z.Text.Replace(".", ",");

                Escreve_cabecalho();
            }
            else
            {
                MessageBox.Show("Preencha a lista de furação por favor", "Atenção");
                return;
            }

            z = Convert.ToDecimal(text_final_z.Text);
            z_mergulho = Convert.ToDecimal(text_mergulho.Text);
            retracao = Convert.ToDecimal(text_retracao.Text);

            if (option_82.Checked == true)
            {
                text_comando.Text = text_comando.Text + "G82 " + list_brocas.Items[0].ToString() + " Z-" + Math.Round(z, 4) +
                        " F" + Math.Round(z_mergulho, 4) + " R" + Math.Round(retracao, 4) + " P" + 1 + Environment.NewLine;
                for (int f = 1; f < list_brocas.Items.Count; f++)
                {
                    text_comando.Text = text_comando.Text + "G82 " + list_brocas.Items[f].ToString() + Environment.NewLine;
                }
            }
            else
            {
                q = Convert.ToDecimal(text_av_vertical.Text);
                text_comando.Text = text_comando.Text + "G83 " + list_brocas.Items[0].ToString() + " Z-" + Math.Round(z, 4) +
                        " F" + Math.Round(z_mergulho, 4) + " R" + Math.Round(retracao, 4) + " Q" + Math.Round(q, 4) + Environment.NewLine;
                for (int f = 1; f < list_brocas.Items.Count; f++)
                {
                    text_comando.Text = text_comando.Text + "G83 " + list_brocas.Items[f].ToString() + Environment.NewLine;
                }
            }
            Escreve_rodape();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            text_q_x.Text = text_q_x.Text.Replace(",", ".");
            text_q_y.Text = text_q_y.Text.Replace(",", ".");
            text_inter_x.Text = text_inter_x.Text.Replace(",", ".");
            text_inter_y.Text = text_inter_y.Text.Replace(",", ".");
            text_x.Text = text_x.Text.Replace(".", ",");
            text_y.Text = text_y.Text.Replace(".", ",");

            var localizacao = new Localizacao(text_x.Text, text_y.Text);
            var matrizRetangular = new MatrizRetangular(text_q_x.Text, text_q_y.Text, text_inter_x.Text, text_inter_y.Text);
            
            double x = localizacao.DistanciaX; 
            double y = localizacao.DistanciaY;

            list_brocas.Items.Clear();
            list_aux.Items.Clear();

            for (int l = 0; l < matrizRetangular.NumeroFurosVertical; l++)
            {
                for (int c = 0; c < matrizRetangular.NumeroFurosHorizontal; c++)
                {
                    list_brocas.Items.Add(" X" + Math.Round(x, 4) + " Y" + Math.Round(y, 4));
                    //g.DrawEllipse(caneta_Amarelo, (float)local_x + (float)x - (float)raio_furo, (float)local_y + (float)y - (float)raio_furo, 2 * (float)raio_furo, 2 * (float)raio_furo);
                    x += matrizRetangular.IntervaloHorizontal; //Convert.ToDecimal(text_inter_x.Text);
                }
                y += matrizRetangular.IntervaloVertical;//y + Convert.ToDecimal(text_inter_y.Text);
                x = localizacao.DistanciaX;//Convert.ToDecimal(text_x.Text);
            }
            desenhaUCS();
            Desenha_Percurso();
        }

        private void list_brocas_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Graphics g = picture_Tela.CreateGraphics();
            Brush Black = new SolidBrush(Color.Black);

            if (list_brocas.Items.Count > 0)
            {
                DialogResult Result = MessageBox.Show("Deseja apagar Todos os Item da lista", "Atenção",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Result == DialogResult.Yes)
                {
                    list_brocas.Items.Clear();
                    list_aux.Items.Clear();
                    //g.FillRectangle(Black, 0, 0, 600, 600);
                    picture_Tela.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Não existe nenhum Item na lista", "Atenção");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // para baixo da lista
            ////////////////////////////

            var index = list_brocas.SelectedIndex;
            if (index + 1 > list_brocas.Items.Count - 1)
                return; // não tem mais posição abaixo dele

            if (index != -1)
            {
                // armazena o item
                var item = list_brocas.Items[index];

                // remove o item atual
                list_brocas.Items.Remove(item);

                // adiciona o item na posição atual + 1
                list_brocas.Items.Insert(index + 1, item);

                // deixa o item selecionado caso queira clicar no botão novamente
                list_brocas.SelectedIndex = index + 1;
                Desenha_Percurso();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // para cima da lista
            //////////////////////

            var index = list_brocas.SelectedIndex;
            if (index - 1 < 0)
                return; // não tem mais posição acima dele

            // armazena o item
            var item = list_brocas.Items[index];

            // remove o item atual
            list_brocas.Items.Remove(item);

            // adiciona o item na posição atual + 1
            list_brocas.Items.Insert(index - 1, item);

            // deixa o item selecionado caso queira clicar no botão novamente
            list_brocas.SelectedIndex = index - 1;
            Desenha_Percurso();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            double AngIni;
            double AngFim;
            double X1;
            double Y1;
            double X; 
            double Y;

            var localizacao = new Localizacao(text_x.Text, text_y.Text);
            X = localizacao.DistanciaX;
            Y = localizacao.DistanciaY;
            var MatrizCircular = new MatrizCircular(text_raio.Text, text_quant.Text, text_graus.Text);

            double raio = MatrizCircular.Raio ;
            double n_furos = Convert.ToDouble(360 / MatrizCircular.NumeroFuros);

            double f;

            double giro = MatrizCircular.GrauRotacao;

            list_brocas.Items.Clear();
            list_aux.Items.Clear();

            //if (check_girar.Checked == true) giro = 360 / 4;

            AngIni = giro;
            AngFim = 360 + giro;

            f = AngIni;

            while (f < AngFim)
            {
                decimal rr = (decimal)raio;
                decimal xx = Convert.ToDecimal(Math.Cos(f * Math.PI / 180)) * rr;
                X1 = X + (double)xx;
                decimal yy = Convert.ToDecimal(Math.Sin((180 - f) * Math.PI / 180)) * rr;
                Y1 = Y + (double)yy;
                list_brocas.Items.Add(" X" + Math.Round(X1, 4) + " Y" + Math.Round(Y1, 4));
                f += n_furos;
            }
            desenhaUCS();
            Desenha_Percurso();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            for (int f = list_brocas.SelectedIndices.Count - 1; f >= 0; f--)
            {
                list_brocas.Items.RemoveAt(list_brocas.SelectedIndices[f]);
                desenhaUCS();
                Desenha_Percurso();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //double numero = double.Parse(text_x.Text);
            //MessageBox.Show("Valor X=" + numero,"Atenção");
            try
            {
                SaveFileDialog arquivo = new SaveFileDialog();

                if (arquivo.ShowDialog() == DialogResult.OK)
                {
                    //MessageBox.Show(arquivo.FileName);

                    diretorio = arquivo.FileName;
                    string nome_arquivo = diretorio;
                    arq = File.AppendText(nome_arquivo);
                    //Escrevo no arquivo txt os dados contidos no textbox
                    arq.Write(text_comando.Text);
                    //Posiciono o ponteiro na próxima linha do arquivo.
                    arq.Write("\r\n");
                    //MessageBox.Show("Codigo salvos com sucesso!!!", "Salvar");
                }
                else
                {
                    arq.Close();
                    //MessageBox.Show("Cancelou");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                //Fecho o arquivo
                arq.Close();
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picture_Tela_MouseMove(object sender, MouseEventArgs e)
        {
            if (item_selecionado == false)
            {
                Point coodenada = new Point(e.X, e.Y);
                Graphics g = picture_Tela.CreateGraphics();
                Pen caneta_Vermelho = new Pen(Color.Red, 1);
                Pen caneta_Amarela = new Pen(Color.Yellow, 1);

                decimal r = Convert.ToDecimal(text_dia_f.Text) / 2;
                double localx = coodenada.X;
                double localy = 600 - coodenada.Y - 25;
                decimal dia_broca = Convert.ToDecimal(text_dia_f.Text);

                for (int f = 0; f < list_brocas.Items.Count; f++)
                    list_brocas.SetSelected(f, false);

                for (int f = 0; f < list_aux.Items.Count; f++)
                {
                    float cx = PegaCoord_aux_X(f) + (float)r;
                    float cy = 600 - PegaCoord_aux_Y(f) - 25 - (float)r;
                    if (localx - (float)r <= cx & localx + (float)r >= cx & localy - (float)r <= cy & localy + (float)r >= cy)
                    {
                        list_brocas.SetSelected(f, true);
                        g.DrawEllipse(caneta_Vermelho, cx - (float)r, 600 - cy - 25 - (float)r, (float)dia_broca, (float)dia_broca);
                    }
                    else
                    {
                        g.DrawEllipse(caneta_Amarela, cx - (float)r, 600 - cy - 25 - (float)r, (float)dia_broca, (float)dia_broca);
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //double cx = PegaCoord_X(0);
            //double cy = PegaCoord_Y(0);

            list_brocas.SetSelected(0, true);
            string hed = list_brocas.Items[f].ToString();
            MessageBox.Show(hed.Replace(" ","").Replace(",",""));
        }

        private void picture_Tela_Click(object sender, EventArgs e)
        {
            //button9_Click(null,null);
            if (item_selecionado == true)
                item_selecionado = false;
            else
                item_selecionado = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            object selected = list_brocas.SelectedItem;
        }

        private void list_brocas_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void list_brocas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                button9_Click(null, null);
                item_selecionado = false;
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void text_quant_TextChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            double GrausFuros = Convert.ToDouble(text_graus.Text);
            double numeroFuros = Convert.ToDouble(text_quant.Text);
            double menorGrau = 360 / numeroFuros / 4;
            if (opt_A.Checked == true)
            {
                if(menorGrau<360)GrausFuros += menorGrau;
                if (GrausFuros <= 360)
                    text_graus.Text = Convert.ToString(Math.Round(GrausFuros, 4));
                else
                    text_graus.Text = "0";
            }
            else
            {
                if(menorGrau>0)GrausFuros -= menorGrau;
                if (Convert.ToInt16(menorGrau) == 0) GrausFuros = 0;
                if (GrausFuros >= 0)
                    text_graus.Text = Convert.ToString(Math.Round(GrausFuros,14));
                else
                    text_graus.Text = "360";
            }
            //button5_Click(null, null);
            
        }
    }
    
}


