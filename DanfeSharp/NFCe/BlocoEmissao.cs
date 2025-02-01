using DanfeSharp.Blocos;

namespace DanfeSharp.NFCe
{
    internal class BlocoEmissao : BlocoBase
    {
        public BlocoEmissao(BlocoContexto contexto) : base(contexto)
        {
            var fr = Estilo.FonteNFCe3;
            var fn = Estilo.FonteNFCeNegrito3;
            var ls = ElementoVazio.T0();
            var lv3 = ElementoVazio.T3();
            var w = Contexto.RetanguloDesenhavel.Width;

            MainVerticalStack.Add(new LinhaSolida(contexto, 1));
            MainVerticalStack.Add(TextBlock.Centro(contexto, "EMITIDA EM AMBIENTE DE HOMOLOGAÇÃO - SEM VALOR FISCAL", fn, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, $"Número: {ViewModel.NfNumero} Série: {ViewModel.NfSerie} Emissão: {ViewModel.DataHoraEmissao.FormatarDataHora()}", fr, w));
            MainVerticalStack.Add(lv3);
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}