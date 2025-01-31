using DanfeSharp.Blocos;
using DanfeSharp.Modelo;

namespace DanfeSharp.NFCe
{
    internal class BlocoTributos : BlocoBase
    {
        public BlocoTributos(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var fr = Estilo.FonteNFCe3;
            var w = viewModel.PaginaLargura;

            MainVerticalStack.Add(new LinhaSolida(1));
            MainVerticalStack.Add(TextBlock.Centro($"Tributos Totais Incidentes (Lei Federal 12.741/2012): {viewModel.CalculoImposto.ValorAproximadoTributos.FormatarMoeda()}", fr, w));
            MainVerticalStack.Add(new LinhaSolida(1));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}