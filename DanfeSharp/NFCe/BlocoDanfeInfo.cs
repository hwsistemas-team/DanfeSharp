using DanfeSharp.Blocos;
using DanfeSharp.Modelo;

namespace DanfeSharp.NFCe
{
    internal class BlocoDanfeInfo : BlocoBase
    {
        public BlocoDanfeInfo(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var fn = Estilo.FonteNFCeNegrito2;
            var fr = Estilo.FonteNFCe3;
            var ls = ElementoVazio.T0();
            var w = viewModel.PaginaLargura;

            MainVerticalStack.Add(new LinhaSolida(1));
            MainVerticalStack.Add(TextBlock.Centro("DANFE NFC-e Documento Auxiliar de Nota Fiscal de Consumidor Eletrônica", fn, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro("Não permite aproveitamento de crédito do ICMS", fr, w));
            MainVerticalStack.Add(ls);
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}