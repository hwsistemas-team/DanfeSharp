using System.Linq;
using DanfeSharp.Blocos;

namespace DanfeSharp.NFCe
{
    internal class BlocoPagamentos : BlocoNFeBase
    {
        public BlocoPagamentos(BlocoNFeContexto contexto) : base(contexto)
        {
            var fr = Estilo.FonteNFCe3;
            var fn = Estilo.FonteNFCeNegrito3;
            var ls = ElementoVazio.T0();

            var totalPag = ViewModel.Pagamentos.Sum(x => x.Valor);

            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(new TextoSeparado(contexto, "FORMA PAGAMENTO", "VALOR PAGO", 50, 50, fr));

            foreach(var forma in ViewModel.Pagamentos)
            {
                MainVerticalStack.Add(ls);
                MainVerticalStack.Add(new TextoSeparado(contexto, forma.FormaPagamento, "R$ " + forma.Valor.Formatar(), 50, 50, fr));
            }

            // MainVerticalStack.Add(ls);
            // MainVerticalStack.Add(TextBlock.Esquerda(contexto, $"(TOTAL PAGO R$ {totalPag.Formatar()})", fr));

            if (ViewModel.TrocoPagamento > 0)
            {
                MainVerticalStack.Add(ls);
                MainVerticalStack.Add(new TextoSeparado(contexto, "TROCO", "R$ " + ViewModel.TrocoPagamento.Formatar(), 50, 50, fn));
            }
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}