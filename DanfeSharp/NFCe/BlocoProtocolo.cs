using DanfeSharp.Blocos;

namespace DanfeSharp.NFCe
{
    internal class BlocoProtocolo : BlocoNFeBase
    {
        public BlocoProtocolo(BlocoNFeContexto contexto) : base(contexto)
        {
            var fr = Estilo.FonteNFCe2;
            var ls = ElementoVazio.T0();

            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, "Protocolo de Autorização", fr));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, ViewModel.ProtocoloAutorizacao, fr));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}