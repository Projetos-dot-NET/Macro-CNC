using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Contorno_Circular
{
    public partial class Form1 : Form
    {
        decimal x = 0;
        decimal y = 0;
        decimal z = 0;
        decimal passo = 0;
        decimal npasso = 0;
        decimal avanco = 0;
        decimal mergulho = 0;
        decimal i = 0;
        decimal j = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            text_dia_f.Text = text_dia_f.Text.Replace(".", ",");
            text_av_vertical.Text = text_av_vertical.Text.Replace(".", ",");
            text_seg.Text = text_seg.Text.Replace(".", ",");
            text_mergulho.Text = text_mergulho.Text.Replace(".", ",");
            text_avanco.Text = text_avanco.Text.Replace(".", ",");
            text_x.Text = text_x.Text.Replace(".", ",");
            text_y.Text = text_y.Text.Replace(".", ",");
            text_dia.Text = text_dia.Text.Replace(".", ",");
            text_final_z.Text = text_final_z.Text.Replace(".", ",");
            text_final_z.Text = text_final_z.Text.Replace(".", ",");
            text_aprox_z.Text = text_aprox_z.Text.Replace(".", ",");

            //Cabeçalho
            z = Convert.ToDecimal(text_troca.Text);
            text_comando.Text = "G17 G21 G90 (Plano XY - metrico - absoluto)" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "G0 Z" + Math.Round(z, 4) + Environment.NewLine;
            text_comando.Text = text_comando.Text + "G0 X0.000 Y0.000 (zero peca)" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "S12000 (velocidade spidle)" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "M0 (troca manual de ferramenta)" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "M3 (liga spindle)" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "G4 P3.000 (pausa por 3 segundos)" + Environment.NewLine;
            z = Convert.ToDecimal(text_seg.Text);
            text_comando.Text = text_comando.Text + "G0 Z" + Math.Round(z,4) +" (Aproximacao de 5mm )" + Environment.NewLine;
            
            //Movimentos
            x = (Convert.ToDecimal(text_x.Text) + (Convert.ToDecimal(text_dia.Text)/2) - 
                (Convert.ToDecimal(text_dia_f.Text))/2) - Convert.ToDecimal(0.1);
            y = Convert.ToDecimal(text_y.Text);
            
            text_comando.Text = text_comando.Text +"G0 X"+ Math.Round(x, 4) + " Y" + Math.Round(y, 4) + Environment.NewLine;

            npasso = Convert.ToDecimal(text_final_z.Text) / Convert.ToDecimal(text_av_vertical.Text);
            npasso = Math.Ceiling(npasso);
            passo = Convert.ToDecimal(text_final_z.Text) / Convert.ToDecimal(npasso);
            z = Convert.ToDecimal(text_aprox_z.Text);
            mergulho = Convert.ToDecimal(text_mergulho.Text);
            avanco = Convert.ToDecimal(text_avanco.Text);
            text_comando.Text = text_comando.Text + "G0 Z" + Math.Round(z, 4) + Environment.NewLine;
            z = passo;
            j = Convert.ToInt32(0);
            for (int f = 0; f < npasso; f++)
            {
                text_comando.Text = text_comando.Text + "G1 Z-" + Math.Round(z, 4) + " F" + mergulho + Environment.NewLine;
                z = z + passo;
                x = (Convert.ToDecimal(text_x.Text) + (Convert.ToDecimal(text_dia.Text) / 2) -
                                (Convert.ToDecimal(text_dia_f.Text)) / 2) - Convert.ToDecimal(0.1);
                y = Convert.ToDecimal(text_y.Text);
                i = Convert.ToDecimal(text_dia.Text) / 2 - Convert.ToDecimal(text_dia_f.Text) / 2;

                text_comando.Text = text_comando.Text + "G2 X" + Math.Round(x, 4) + " Y" + Math.Round(y, 4) +
                    " I-" + Math.Round(i, 4) + " J" + j + " F" + avanco + Environment.NewLine;
            }
            z= Convert.ToDecimal(text_aprox_z.Text);
            text_comando.Text = text_comando.Text + "G0 Z" + Math.Round(z, 4) + Environment.NewLine;
            x = (Convert.ToDecimal(text_x.Text) + (Convert.ToDecimal(text_dia.Text) / 2) -
                (Convert.ToDecimal(text_dia_f.Text)) / 2);
            y = Convert.ToDecimal(text_y.Text);
            text_comando.Text = text_comando.Text + "G0 X" + Math.Round(x, 4) + " Y" + Math.Round(y, 4) + Environment.NewLine;
            i = Convert.ToDecimal(text_dia.Text) / 2 - Convert.ToDecimal(text_dia_f.Text) / 2;
            z = Convert.ToDecimal(text_final_z.Text);
            text_comando.Text = text_comando.Text + "G1 Z-" + Math.Round(z, 4) + " F" + mergulho + Environment.NewLine;
            text_comando.Text = text_comando.Text + "G3 X" + Math.Round(x, 4) + " Y" + Math.Round(y, 4) +
                " I-" + Math.Round(i, 4) + " J" + j + " F" + avanco + Environment.NewLine;
            text_comando.Text = text_comando.Text + "G0 Z" + Math.Round(z, 4) + Environment.NewLine;
            
            //Rodapé
            z = 25;
            text_comando.Text = text_comando.Text + "G0 X0.000 Y0.000 Z" + z + Environment.NewLine;
            text_comando.Text = text_comando.Text + "M5" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "M30" + Environment.NewLine;
            text_comando.Text = text_comando.Text.Replace(",", ".");
            //var teste = Math.Sin(0.524);// 30 * 3.14159265358979 / 180);
            //text_comando.Text = Convert.ToString(teste);
        }

    }
}

