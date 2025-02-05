using DanfeSharp.Blocos;

namespace DanfeSharp.Evento
{
    internal class BlocoEventoCancelamento : BlocoEventoBase
    {
        #region Constructors

        public BlocoEventoCancelamento(BlocoEventoContexto contexto) : base(contexto)
        {
            var campoJustificativa = new CampoMultilinha(contexto, "", ViewModel.Justificativa, AlinhamentoHorizontal.Esquerda) { Height = AlturaCampo, IsConteudoNegrito = true };
            var linha = new FlexibleLine(contexto) { Height = AlturaCampo }
                .ComElemento(campoJustificativa)
                .ComLargurasIguais();

            MainVerticalStack.Add(linha);
        }

        #endregion

        #region Properties

        public const float AlturaCampo = Constantes.A4Altura - 99;
        public override string Cabecalho => "JUSTIFICATIVA";
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;

        #endregion
    }
}
