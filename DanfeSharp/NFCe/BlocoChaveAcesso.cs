using DanfeSharp.Blocos;

namespace DanfeSharp.NFCe
{
    internal class BlocoChaveAcesso : BlocoBase
    {
        public BlocoChaveAcesso(BlocoContexto contexto) : base(contexto)
        {
            var fr = Estilo.FonteNFCe3;
            var fn = Estilo.FonteNFCeNegrito3;
            var ls = ElementoVazio.T0();
            var w = Contexto.RetanguloDesenhavel.Width;

            MainVerticalStack.Add(new LinhaSolida(contexto, 1));
            MainVerticalStack.Add(TextBlock.Centro(contexto, "Consulte pela chave de acesso em:", fr, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, ViewModel.UrlChave ?? "", fr, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, "CHAVE DE ACESSO", fn, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, ViewModel.ChaveAcesso, fn, w));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}