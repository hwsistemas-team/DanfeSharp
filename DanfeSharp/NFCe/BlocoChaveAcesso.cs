using DanfeSharp.Blocos;
using DanfeSharp.Modelo;

namespace DanfeSharp.NFCe
{
    internal class BlocoChaveAcesso : BlocoBase
    {
        public BlocoChaveAcesso(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var fr = Estilo.FonteNFCe3;
            var fn = Estilo.FonteNFCeNegrito3;
            var ls = ElementoVazio.T0();
            var w = viewModel.PaginaLargura;

            MainVerticalStack.Add(new LinhaSolida(1));
            MainVerticalStack.Add(TextBlock.Centro("Consulte pela chave de acesso em:", fr, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(viewModel.UrlChave ?? "", fr, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro("CHAVE DE ACESSO", fn, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(viewModel.ChaveAcesso, fn, w));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}