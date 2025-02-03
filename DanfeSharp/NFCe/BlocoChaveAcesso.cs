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

            MainVerticalStack.Add(new LinhaSolida(contexto, 1));
            MainVerticalStack.Add(TextBlock.Centro(contexto, "Consulte pela chave de acesso em:", fr));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, ViewModel.UrlChave ?? "", fr));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, "CHAVE DE ACESSO", fn));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, ViewModel.ChaveAcesso, fn));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}