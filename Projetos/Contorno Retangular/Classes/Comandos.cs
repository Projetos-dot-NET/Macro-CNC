using static System.Environment;

namespace ContornoRetangular.Classes
{
    public static class Comandos
    {
        public static string InserirCabecalho(decimal z)
        {
            var cabecalho = "G17 G21 G90 (Plano XY - metrico - absoluto)" + NewLine +
                            "G0 Z" + z + " (Z para troca de ferramenta )" + NewLine +
                            "G0 X0.000 Y0.000 (zero peca)" + NewLine +
                            "S12000 (velocidade spidle)" + NewLine +
                            "M0 (troca manual de ferramenta)" + NewLine +
                            "M3 (liga spindle)" + NewLine +
                            "G4 P3000 (pausa por 3 segundos)" + NewLine +
                            "G0 Z5.000 (Aproximacao de 5mm )" + NewLine;

            return cabecalho;
        }

        public static string InserirRodape(decimal z)
        {
            var rodape = "G0 Z" + z + " (Z para troca de ferramenta )" + NewLine +
                         "M5" + NewLine +
                         "G0 X0.000 Y0.000" + NewLine +
                         "M30" + NewLine;

            return rodape;
        }

        public static string DesenharRetangulo()
        {
            return "";
        }
    }
}
