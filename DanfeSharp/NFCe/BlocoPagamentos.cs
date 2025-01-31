using System.Linq;
using DanfeSharp.Blocos;
using DanfeSharp.Modelo;

namespace DanfeSharp.NFCe
{
    internal class BlocoPagamentos : BlocoBase
    {
        public BlocoPagamentos(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var fr = Estilo.FonteNFCe3;
            var fn = Estilo.FonteNFCeNegrito3;
            var ls = ElementoVazio.T0();
            var w = viewModel.PaginaLargura;

            var totalPag = viewModel.Pagamentos.Sum(x => x.Valor);

            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(new TextoSeparado("FORMA PAGAMENTO", "VALOR PAGO R$", 50, 50, fr));

            foreach(var forma in viewModel.Pagamentos)
            {
                MainVerticalStack.Add(ls);
                MainVerticalStack.Add(new TextoSeparado(forma.FormaPagamento, forma.Valor.Formatar(), 50, 50, fr));
            }

            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Esquerda($"(TOTAL PAGO R$ {totalPag.Formatar()})", fr, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(new TextoSeparado("TROCO R$", viewModel.TrocoPagamento.Formatar(), 50, 50, fn));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}