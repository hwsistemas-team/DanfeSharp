
namespace DanfeSharp.Blocos
{
    internal class BlocoFatura : BlocoNFeBase
    {
        public BlocoFatura(BlocoNFeContexto contexto) : base(contexto)
        {
            var fat = ViewModel.Fatura;

            AdicionarLinhaCampos()
                .ComCampo("Número", fat.Numero)
                .ComCampo("Valor original", fat.ValorOriginal.Formatar(), AlinhamentoHorizontal.Direita)
                .ComCampo("Valor desconto", fat.ValorDesconto.Formatar(), AlinhamentoHorizontal.Direita)
                .ComCampo("Valor líquido", fat.ValorLiquido.Formatar(), AlinhamentoHorizontal.Direita)
                .ComLargurasIguais();
        }

        public override string Cabecalho => "Fatura";
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
    }
}
