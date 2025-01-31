using DanfeSharp.Blocos;
using DanfeSharp.Modelo;

namespace DanfeSharp.NFCe
{
    internal class BlocoProtocolo : BlocoBase
    {
        public BlocoProtocolo(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var fr = Estilo.FonteNFCe2;
            var ls = ElementoVazio.T0();
            var w = viewModel.PaginaLargura;

            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro("Protocolo de Autorização", fr, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(viewModel.ProtocoloAutorizacao, fr, w));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}