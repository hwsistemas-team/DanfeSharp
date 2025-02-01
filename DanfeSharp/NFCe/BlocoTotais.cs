using System.Linq;
using DanfeSharp.Blocos;

namespace DanfeSharp.NFCe
{
    internal class BlocoTotais : BlocoBase
    {
        public BlocoTotais(BlocoContexto contexto) : base(contexto)
        {
            var fn = Estilo.FonteNFCeNegrito1;
            var fr = Estilo.FonteNFCe3;
            var ls = ElementoVazio.T0();

            var qtdTotal = ViewModel.Produtos.Count;
            var valorTotal = ViewModel.Produtos.Sum(x => x.ValorTotal);

            MainVerticalStack.Add(new LinhaSolida(contexto, 1));
            MainVerticalStack.Add(new TextoSeparado(contexto, "Qtde. total de itens", qtdTotal.ToString(), 80, 20, fr));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(new TextoSeparado(contexto, "Valor total R$", valorTotal.Formatar(), 60, 40, fn));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}