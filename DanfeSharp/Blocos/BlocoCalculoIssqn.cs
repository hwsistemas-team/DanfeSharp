
namespace DanfeSharp.Blocos
{
    internal class BlocoCalculoIssqn : BlocoBase
    {
        public BlocoCalculoIssqn(BlocoContexto contexto) : base(contexto)
        {
            var m = ViewModel.CalculoIssqn;

            AdicionarLinhaCampos()
                .ComCampo("INSCRIÇÃO MUNICIPAL", m.InscricaoMunicipal, AlinhamentoHorizontal.Centro)
                .ComCampoNumerico("VALOR TOTAL DOS SERVIÇOS", m.ValorTotalServicos)
                .ComCampoNumerico("BASE DE CÁLCULO DO ISSQN", m.BaseIssqn)
                .ComCampoNumerico("VALOR TOTAL DO ISSQN", m.ValorIssqn)
                .ComLargurasIguais();
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Base;
        public override string Cabecalho => "CÁLCULO DO ISSQN";
    }
}
