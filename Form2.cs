using System;
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

namespace Macro_CNC
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
        public void criarArquivo()
        {
            try
            {
                //Determino o diretorio onde será salvo o arquivo
                string nome_arquivo = diretorio + "\\" + text_mome.Text + ".tap";// "\\textBox.txt";

                //verificamos se o arquivo existe, se não existir então criamos o arquivo
                if (!File.Exists(nome_arquivo))
                    File.Create(nome_arquivo).Close();

                // crio a variavel responsável por gravar os dados no arquivo txt
                arquivo = File.AppendText(nome_arquivo);

                //Escrevo no arquivo txt os dados contidos no textbox
                arquivo.Write(text_codigo.Text);

                //Posiciono o ponteiro na próxima linha do arquivo.
                arquivo.Write("\r\n");

                MessageBox.Show("Codigo salvos com sucesso!!!","Salvar");

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


        public void interfaceUsuario()
        {

            // titulo a caixa de diágolo do browser que será aberta
            folderDialog.Description = "Selecione o Diretório a ser pesquisado:";

            //Indica o diretório raiz, a partir de onde a caixa de diálogo começará 
            //a exibição dos demais diretórios.
            folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;

            // elimina a condição de criar uma nova pasta ao abrir a caixa
            // de diálogo do browser
            folderDialog.ShowNewFolderButton = false;

            if (folderDialog.ShowDialog(this) != DialogResult.Cancel)
            {
                //Recupero o diretório da base de dados e o salvo na variavel diretorio
                diretorio = folderDialog.SelectedPath;
            }

        }
        private void btn_escreve_Click(object sender, EventArgs e)
        {

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

        private void btn_cancela_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Cab_Click(object sender, EventArgs e)
        {
            text_codigo.Text = "G17 G21 G90 (Plano XY - metrico - absoluto)" + Environment.NewLine;
            text_codigo.Text = text_codigo.Text + "G0 Z25.000 (25mm de seguranca)" + Environment.NewLine;
            text_codigo.Text = text_codigo.Text + "G0 X0.000 Y0.000 (zero peca)" + Environment.NewLine;
            text_codigo.Text = text_codigo.Text + "S12000 (velocidade spidle)" + Environment.NewLine;
            text_codigo.Text = text_codigo.Text + "M0 (troca manual de ferramenta)" + Environment.NewLine;
            text_codigo.Text = text_codigo.Text + "M3 (liga spindle)" + Environment.NewLine;
            text_codigo.Text = text_codigo.Text + "G4 P3000 (pausa por 3 segundos)" + Environment.NewLine;
            text_codigo.Text = text_codigo.Text + "G0 Z5.000 (Aproximacao de 5mm )" + Environment.NewLine;
        }

        private void btn_Rod_Click(object sender, EventArgs e)
        {
            text_codigo.Text = text_codigo.Text + "G0 Z25.000 " + Environment.NewLine;
            text_codigo.Text = text_codigo.Text + "M5" + Environment.NewLine;
            text_codigo.Text = text_codigo.Text + "G0 X0.000 Y0.000" + Environment.NewLine;
            text_codigo.Text = text_codigo.Text + "M30" + Environment.NewLine;
        }


        private void btn_ok_Click(object sender, EventArgs e)
        {
            interfaceUsuario();
            criarArquivo();
        }
    }
}
