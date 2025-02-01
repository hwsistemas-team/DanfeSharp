using DanfeSharp.Blocos;

namespace DanfeSharp.NFCe
{
    internal class BlocoProtocolo : BlocoBase
    {
        public BlocoProtocolo(BlocoContexto contexto) : base(contexto)
        {
            var fr = Estilo.FonteNFCe2;
            var ls = ElementoVazio.T0();
            var w = Contexto.RetanguloDesenhavel.Width;

            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, "Protocolo de Autorização", fr, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, ViewModel.ProtocoloAutorizacao, fr, w));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}