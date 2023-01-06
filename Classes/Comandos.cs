
namespace Macro_CNC.Classes
{
    public static class Comandos
    {
        public static string InserirCabecalho()
        {
            var cabecalho = "G17 G21 G90 (Plano XY - metrico - absoluto)" + Environment.NewLine +
                            "G0 Z25.000 (25mm de seguranca)" + Environment.NewLine +
                            "G0 X0.000 Y0.000 (zero peca)" + Environment.NewLine +
                            "S12000 (velocidade spidle)" + Environment.NewLine +
                            "M0 (troca manual de ferramenta)" + Environment.NewLine +
                            "M3 (liga spindle)" + Environment.NewLine +
                            "G4 P3000 (pausa por 3 segundos)" + Environment.NewLine +
                            "G0 Z5.000 (Aproximacao de 5mm )" + Environment.NewLine;

            return cabecalho;
        }

        public static string InserirRodape()
        {
            var rodape = "G0 Z25.000" + Environment.NewLine +
                         "M5" + Environment.NewLine +
                         "G0 X0.000 Y0.000" + Environment.NewLine +
                         "M30" + Environment.NewLine;

            return rodape;
        }

        public static string DesenharRetangulo()
        {
            return "";
        }
    }
}
