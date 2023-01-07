﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Furos
{
    public partial class Form1 : Form
    {
        decimal x = 0;
        decimal y = 0;
        decimal z = 0;
        //decimal passo = 0;
        //decimal npasso = 0;
        //decimal avanco = 0;
        //decimal mergulho = 0;
        decimal i = 0;
        decimal j = 0;
        decimal z_mergulho = 0;
        decimal retracao = 0;
        int quantidade_x = 0;
        int quantidade_y = 0;
        int laco = 0;
        int c = 0;
        int l = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Escreve_cabecalho()
        {
            //Cabeçalho
            text_comando.Text = "G17 G21 G90 (Plano XY - metrico - absoluto)" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "G0 X0.000 Y0.000 (zero peca)" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "S12000 (velocidade spidle)" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "M0 (troca manual de ferramenta)" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "M3 (liga spindle)" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "G4 P3.000 (pausa por 3 segundos)" + Environment.NewLine;
            z = Convert.ToDecimal(text_seg.Text);
            text_comando.Text = text_comando.Text + "G0 Z" + z + " (Aproximacao de 5mm )" + Environment.NewLine;
        }

        private void Escreve_rodape()
        {
            //Rodapé
            z = 25;
            text_comando.Text = text_comando.Text + "G0 X0.000 Y0.000 Z" + z + Environment.NewLine;
            text_comando.Text = text_comando.Text + "M5" + Environment.NewLine;
            text_comando.Text = text_comando.Text + "M30" + Environment.NewLine;
            text_comando.Text = text_comando.Text.Replace(",", ".");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            text_x.Text = text_x.Text.Replace(",", ".");
            text_y.Text = text_y.Text.Replace(",", ".");
            list_brocas.Items.Add(" X" + text_x.Text + " Y" + text_y.Text );
        }

        private void button3_Click(object sender, EventArgs e)
        {
            list_brocas.Items.Remove(list_brocas.SelectedItem);
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
                MessageBox.Show("Preencha a lista de furação por favor","Atenção");
            }
            //G82 X-7.9375 Y5.7150  Z-2.0000 F254   R3.0000  P1.000000
            z = Convert.ToDecimal(text_final_z.Text);
            z_mergulho = Convert.ToDecimal(text_mergulho.Text);
            retracao = Convert.ToDecimal(text_retracao.Text);

            if (option_82.Checked == true)
            {
                text_comando.Text = text_comando.Text + "G82 " + list_brocas.Items[0].ToString() + " Z-" + Math.Round(z, 4) +
                        " F" + Math.Round(z_mergulho, 4) + " R" + Math.Round(retracao, 4) + " P" + 1 + Environment.NewLine;
                for (int f=1; f < list_brocas.Items.Count ; f++)
                {
                    text_comando.Text = text_comando.Text + "G82 " + list_brocas.Items[f].ToString() + Environment.NewLine; 
                }
            }
            Escreve_rodape();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            list_brocas.Items.Clear();
            text_q_x.Text = text_q_x.Text.Replace(",", ".");
            text_q_y.Text = text_q_y.Text.Replace(",", ".");
            text_inter_y.Text = text_inter_y.Text.Replace(",", ".");
            text_x.Text = text_x.Text.Replace(".", ",");
            text_y.Text = text_y.Text.Replace(".", ",");

            x = Convert.ToDecimal(text_x.Text);
            y = Convert.ToDecimal(text_y.Text);

            //list_brocas.Items.Add(" X" + Math.Round(x,4) + " Y" + Math.Round(y,4));
            quantidade_x = Convert.ToInt32(text_q_x.Text);
            quantidade_y = Convert.ToInt32(text_q_y.Text);
            laco = 0;
            c = 0;
            l = 0;
            for (l = 0; l < quantidade_y; l++)
            {
                for (c = 0; c < quantidade_x; c++)
                { 
                    list_brocas.Items.Add(" X" + Math.Round(x, 4) + " Y" + Math.Round(y, 4));
                    x = x + Convert.ToDecimal(text_inter_x.Text);
                }
                y = y + Convert.ToDecimal(text_inter_y.Text);
                x = Convert.ToDecimal(text_x.Text);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }
    }
}