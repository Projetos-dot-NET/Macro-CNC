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

using System.IO;
using System.Diagnostics;
using ContornoRetangular.Classes;

namespace ContornoRetangular
{


    public partial class Form2 : Form
    {
        decimal x = 0;
        decimal y = 0;
        decimal z = 0;
        decimal ajuste_x =0;
        decimal ajuste_y =0;
        decimal passo = 0;
        decimal npasso = 0;

        /// objeto do tipo FolderBrowserDialog. Utilizado na interação do sistema
        /// com o usuário na hora da escolha do diretório da base de dados a ser
        /// pesquisada. 
        public FolderBrowserDialog folderDialog = new FolderBrowserDialog();

        //Irá armazenar o diretório recebido pelo folderDialog
        public string diretorio;

        //Arquivo de escrita 
        public TextWriter arquivo;

        
        public Form2()
        {
            InitializeComponent();
        }
        public void criarArquivo() //Rotina para salvar o arquivo para o CNC
        {
            try
            {

                //Determino o diretorio onde será salvo o arquivo
                string diretorioBkp = diretorio + "\\Backup";
                string nome_arquivo = diretorio + "\\" + text_mome.Text + ".tap";
                text_pasta.Text = diretorio; //+ "\\" + text_mome.Text + ".tap";
                

                //verificamos se o arquivo existe, se existir então deleta
                if (File.Exists(nome_arquivo))
                {
                    //DialogResult resp = MessageBox.Show("Esse arquivo já existe, deseja apagar?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    //if (resp == DialogResult.OK)
                    //{
                        //File.Delete(nome_arquivo);
                    //}
                    //else
                    //{
                        if (!Directory.Exists(diretorioBkp))
                        {

                            //Criamos um com o nome Backup
                            Directory.CreateDirectory(diretorioBkp);

                        }
                        var hora_atual = String.Format("{0}-{1}-{2}", DateTime.Now.Hour.ToString("00"), DateTime.Now.Minute.ToString("00"),DateTime.Now.Second.ToString("00"));
                        //DateTime dataHora = DateTime.Now;
                        File.Move(nome_arquivo, diretorioBkp + "\\" + text_mome.Text + "-" + hora_atual + ".tap");
                    //}
                    File.Delete(nome_arquivo);
                }    
                    

                //verificamos se o arquivo existe, se não existir então criamos o arquivo
                if (!File.Exists(nome_arquivo))
                File.Create(nome_arquivo).Close();
                
                    


                // crio a variavel responsável por gravar os dados no arquivo txt
                arquivo = File.AppendText(nome_arquivo);

                //Posiciono o ponteiro na próxima linha do arquivo.
                DialogResult result = MessageBox.Show("Deseja salvar o arquivo de CNC?", "Atenção",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                
                if (result == DialogResult.OK)
                {
                    //Escrevo no arquivo txt os dados contidos no textbox
                    arquivo.Write(text_codigo.Text);
                    //Posiciono o ponteiro na próxima linha do arquivo.
                    arquivo.Write("\r\n");
                    //MessageBox.Show("Codigo salvos com sucesso!!!", "Salvar");
                }
                else
                {
                    arquivo.Close();
                    File.Delete(nome_arquivo);
                }

                //Limpo textbox
                //text_codigo.Text = string.Empty;
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                //Fecho o arquivo
                arquivo.Close();
            }
        }

        public void interfaceUsuario(Boolean st) //Rotina para preparar a interface para selecionar a pasta a ser gravada
        {

            // titulo a caixa de diágolo do browser que será aberta
            folderDialog.Description = "Selecione o Caminho para Salvar o Arquivo:";

            //Indica o diretório raiz, a partir de onde a caixa de diálogo começará 
            //a exibição dos demais diretórios.
            folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            text_pasta.Text = folderDialog.SelectedPath;

            // elimina a condição de criar uma nova pasta ao abrir a caixa
            // de diálogo do browser
            folderDialog.ShowNewFolderButton = false;

            if (folderDialog.ShowDialog(this) != DialogResult.Cancel)
            {
                //Recupero o diretório da base de dados e o salvo na variavel diretorio
                diretorio = folderDialog.SelectedPath;
                if (st == true)
                {
                    criarArquivo();
                }
                
            }

        }
        
        private void btn_escreve_Click(object sender, EventArgs e) //Rotina para escrever o codigo CNC de usinagem
        {

            text_dist_x.Text = text_dist_x.Text.Replace(".",",");
            text_dist_y.Text = text_dist_y.Text.Replace(".",",");
            text_dia_fresa.Text = text_dia_fresa.Text.Replace(".",",");
            text_av_vertical.Text = text_av_vertical.Text.Replace(".",",");
            text_seg.Text = text_mergulho.Text.Replace(".", ",");
            text_mergulho.Text = text_mergulho.Text.Replace(".", ",");
            text_avanco.Text = text_avanco.Text.Replace(".", ",");
            text_ajuste.Text = text_ajuste.Text.Replace(".", ",");
            text_x.Text = text_x.Text.Replace(".", ",");
            text_y.Text = text_y.Text.Replace(".", ",");
            text_final_z.Text = text_final_z.Text.Replace(".", ",");
            text_aprox_z.Text = text_aprox_z.Text.Replace(".", ",");

            //fazer o delocamento em X, Y e Z em segurança para a localização do bloco a usinar
            if (opc_mais.Checked == true && opc_externo.Checked == true)
            {
                
                ajuste_x = Convert.ToDecimal(text_dist_x.Text) - (Convert.ToDecimal(text_dia_fresa.Text) / 2 + Convert.ToDecimal(text_ajuste.Text)/2);
                ajuste_y = Convert.ToDecimal(text_dist_y.Text) - (Convert.ToDecimal(text_dia_fresa.Text) / 2 + Convert.ToDecimal(text_ajuste.Text)/2);
            }

            if (opc_menos.Checked == true && opc_externo.Checked == true)
            {
                ajuste_x = Convert.ToDecimal(text_dist_x.Text) - (Convert.ToDecimal(text_dia_fresa.Text) / 2 - Convert.ToDecimal(text_ajuste.Text)/2);
                ajuste_y = Convert.ToDecimal(text_dist_y.Text) - (Convert.ToDecimal(text_dia_fresa.Text) / 2 - Convert.ToDecimal(text_ajuste.Text)/2);
            }
            if (opc_mais.Checked == true && opc_interno.Checked == true)
            {
                ajuste_x = Convert.ToDecimal(text_dist_x.Text) + (Convert.ToDecimal(text_dia_fresa.Text) / 2 + Convert.ToDecimal(text_ajuste.Text)/2);
                ajuste_y = Convert.ToDecimal(text_dist_y.Text) + (Convert.ToDecimal(text_dia_fresa.Text) / 2 + Convert.ToDecimal(text_ajuste.Text)/2);
            }

            if (opc_menos.Checked == true && opc_interno.Checked == true)
            {
                ajuste_x = Convert.ToDecimal(text_dist_x.Text) + (Convert.ToDecimal(text_dia_fresa.Text) / 2 - Convert.ToDecimal(text_ajuste.Text)/2);
                ajuste_y = Convert.ToDecimal(text_dist_y.Text) + (Convert.ToDecimal(text_dia_fresa.Text) / 2 - Convert.ToDecimal(text_ajuste.Text)/2);
            }

            x = ajuste_x;
            y = ajuste_y;
            text_codigo.Text = text_codigo.Text + "G0 X " + Math.Round(ajuste_x,4) + " Y " + Math.Round(ajuste_y,4) + Environment.NewLine ;
            npasso = Convert.ToDecimal(text_final_z.Text) / Convert.ToDecimal(text_av_vertical.Text);
            npasso = Math.Ceiling(npasso);
            passo = Convert.ToDecimal(text_final_z.Text) / Convert.ToDecimal(npasso); 

            z = Convert.ToDecimal(text_aprox_z.Text);
            
            text_codigo.Text = text_codigo.Text + "G0 Z " + Math.Round(z, 4) + Environment.NewLine;
            
            z = passo;

            for (int f = 0; f < npasso; f++)
            {
                text_codigo.Text = text_codigo.Text + "G1 Z-" + Math.Round(z, 4) + " F " + Convert.ToDecimal(text_mergulho.Text) + Environment.NewLine;
                z = z + passo;
                //Usinar no sentido concordante interno ou interno mais ajuste
                if (opc_interno.Checked == true && opc_concordante.Checked == true && opc_mais.Checked == true)
                {
                    y = y + Convert.ToDecimal(text_y.Text) - Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + " F " + Convert.ToDecimal(text_avanco.Text) + Environment.NewLine;

                    x = x + Convert.ToDecimal(text_x.Text) - Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    y = y - Convert.ToDecimal(text_y.Text) + Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    x = x - Convert.ToDecimal(text_x.Text) + Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;
                 }
                if (opc_externo.Checked == true && opc_concordante.Checked == true && opc_mais.Checked == true)
                {
                    y = y + Convert.ToDecimal(text_y.Text) + Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + " F " + Convert.ToDecimal(text_avanco.Text) + Environment.NewLine;

                    x = x + Convert.ToDecimal(text_x.Text) + Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    y = y - Convert.ToDecimal(text_y.Text) - Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    x = x - Convert.ToDecimal(text_x.Text) - Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;
                }
                //Usinar no sentido concordante interno ou interno menos ajuste
                if (opc_interno.Checked == true && opc_concordante.Checked == true && opc_menos.Checked == true)
                {
                    y = y + Convert.ToDecimal(text_y.Text) - Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + " F " + Convert.ToDecimal(text_avanco.Text) + Environment.NewLine;

                    x = x + Convert.ToDecimal(text_x.Text) - Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    y = y - Convert.ToDecimal(text_y.Text) + Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    x = x - Convert.ToDecimal(text_x.Text) + Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;
                }
                if (opc_externo.Checked == true && opc_concordante.Checked == true && opc_menos.Checked == true)
                {
                    y = y + Convert.ToDecimal(text_y.Text) + Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + " F " + Convert.ToDecimal(text_avanco.Text) + Environment.NewLine;

                    x = x + Convert.ToDecimal(text_x.Text) + Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    y = y - Convert.ToDecimal(text_y.Text) - Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    x = x - Convert.ToDecimal(text_x.Text) - Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;
                }



                //Usinar no sentido discordante interno ou externo mais ajuste
                if (opc_interno.Checked == true && opc_discordante.Checked == true && opc_mais.Checked == true)
                {
                    x = x + Convert.ToDecimal(text_x.Text) - Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + " F " + Convert.ToDecimal(text_avanco.Text) + Environment.NewLine;

                    y = y + Convert.ToDecimal(text_y.Text) - Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    x = x - Convert.ToDecimal(text_x.Text) + Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    y = y - Convert.ToDecimal(text_y.Text) + Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                }
                if (opc_externo.Checked == true && opc_discordante.Checked == true && opc_mais.Checked == true)
                {
                    x = x + Convert.ToDecimal(text_x.Text) + Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + " F " + Convert.ToDecimal(text_avanco.Text) + Environment.NewLine;

                    y = y + Convert.ToDecimal(text_y.Text) + Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    x = x - Convert.ToDecimal(text_x.Text) - Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    y = y - Convert.ToDecimal(text_y.Text) - Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;
                }
                //Usinar no sentido discordante interno ou externo menos ajuste
                if (opc_interno.Checked == true && opc_discordante.Checked == true && opc_menos.Checked == true)
                {
                    x = x + Convert.ToDecimal(text_x.Text) - Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + " F " + Convert.ToDecimal(text_avanco.Text) + Environment.NewLine;

                    y = y + Convert.ToDecimal(text_y.Text) - Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    x = x - Convert.ToDecimal(text_x.Text) + Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    y = y - Convert.ToDecimal(text_y.Text) + Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                }
                if (opc_externo.Checked == true && opc_discordante.Checked == true && opc_menos.Checked == true)
                {
                    x = x + Convert.ToDecimal(text_x.Text) + Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + " F " + Convert.ToDecimal(text_avanco.Text) + Environment.NewLine;

                    y = y + Convert.ToDecimal(text_y.Text) + Convert.ToDecimal(text_dia_fresa.Text) + Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    x = x - Convert.ToDecimal(text_x.Text) - Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;

                    y = y - Convert.ToDecimal(text_y.Text) - Convert.ToDecimal(text_dia_fresa.Text) - Convert.ToDecimal(text_ajuste.Text);
                    text_codigo.Text = text_codigo.Text + "G1 X " + Math.Round(x, 4) + " Y " + Math.Round(y, 4) + Environment.NewLine;
                }
            }
            text_codigo.Text = text_codigo.Text.Replace(",", ".");
        }

        private void btn_cancela_Click(object sender, EventArgs e) // Rotina para fechar o App
        {
            this.Close();
        }

        private void btn_Cab_Click(object sender, EventArgs e) //Rotina para escrever o Cabeçalho para Mach3
        {
            text_codigo.Text = Comandos.InserirCabecalho();
        }

        private void btn_Rod_Click(object sender, EventArgs e) //Rotina para escrever o Rodapé para Mach3
        {
            text_codigo.Text += Comandos.InserirRodape();
        }

        private void btn_ok_Click(object sender, EventArgs e) //Chamada da interface pelo botão de salvar
        {
           if(text_pasta.Text == "Selecione o caminho no botão salvar")
            {
                interfaceUsuario(true);
            }
           else
            { 
                diretorio = folderDialog.SelectedPath;
                criarArquivo();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            interfaceUsuario(false);
            text_pasta.Text = diretorio;
        }
    }
}