using DanfeSharp.Blocos;

namespace DanfeSharp.NFCe
{
    internal class BlocoDanfeInfo : BlocoNFeBase
    {
        public BlocoDanfeInfo(BlocoNFeContexto contexto) : base(contexto)
        {
            var fn = Estilo.FonteNFCeNegrito2;
            var fr = Estilo.FonteNFCe3;
            var ls = ElementoVazio.T0();

            MainVerticalStack.Add(new LinhaSolida(contexto, 1));
            MainVerticalStack.Add(TextBlock.Centro(contexto, "DANFE NFC-e Documento Auxiliar de Nota Fiscal de Consumidor Eletrônica", fn));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, "Não permite aproveitamento de crédito do ICMS", fr));
            MainVerticalStack.Add(ls);
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}