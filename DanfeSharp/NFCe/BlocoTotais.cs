using System.Linq;
using DanfeSharp.Blocos;
using DanfeSharp.Modelo;

namespace DanfeSharp.NFCe
{
    internal class BlocoTotais : BlocoBase
    {
        public BlocoTotais(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var fn = Estilo.FonteNFCeNegrito1;
            var fr = Estilo.FonteNFCe3;
            var ls = ElementoVazio.T0();

            var qtdTotal = viewModel.Produtos.Count;
            var valorTotal = viewModel.Produtos.Sum(x => x.ValorTotal);

            MainVerticalStack.Add(new LinhaSolida(1));
            MainVerticalStack.Add(new TextoSeparado("Qtde. total de itens", qtdTotal.ToString(), 80, 20, fr));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(new TextoSeparado("Valor total R$", valorTotal.Formatar(), 60, 40, fn));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}