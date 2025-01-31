using DanfeSharp.Blocos;
using DanfeSharp.Modelo;

namespace DanfeSharp.NFCe
{
    internal class BlocoEmissao : BlocoBase
    {
        public BlocoEmissao(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var fr = Estilo.FonteNFCe3;
            var fn = Estilo.FonteNFCeNegrito3;
            var ls = ElementoVazio.T0();
            var lv3 = ElementoVazio.T3();
            var w = viewModel.PaginaLargura;

            MainVerticalStack.Add(new LinhaSolida(1));
            MainVerticalStack.Add(TextBlock.Centro("EMITIDA EM AMBIENTE DE HOMOLOGAÇÃO - SEM VALOR FISCAL", fn, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro($"Número: {viewModel.NfNumero} Série: {viewModel.NfSerie} Emissão: {viewModel.DataHoraEmissao.FormatarDataHora()}", fr, w));
            MainVerticalStack.Add(lv3);
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}