using DanfeSharp.Blocos;

namespace DanfeSharp.NFCe
{
    internal class BlocoEmissao : BlocoNFeBase
    {
        public BlocoEmissao(BlocoNFeContexto contexto) : base(contexto)
        {
            var fr = Estilo.FonteNFCe3;
            var fn = Estilo.FonteNFCeNegrito3;
            var ls = ElementoVazio.T0();
            var lv3 = ElementoVazio.T3();

            MainVerticalStack.Add(new LinhaSolida(contexto, 1));
            MainVerticalStack.Add(TextBlock.Centro(contexto, "EMITIDA EM AMBIENTE DE HOMOLOGAÇÃO - SEM VALOR FISCAL", fn));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, $"Número: {ViewModel.NfNumero.ToString().PadLeft(9, '0')} Série: {ViewModel.NfSerie.ToString().PadLeft(3, '0')} Emissão: {ViewModel.DataHoraEmissao.FormatarDataHora()}", fr));
            MainVerticalStack.Add(lv3);
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}