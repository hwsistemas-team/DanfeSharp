using DanfeSharp.Blocos;

namespace DanfeSharp.Evento
{
    internal class BlocoEventoCabecalho : BlocoEventoBase
    {
        #region Constructors

        public BlocoEventoCabecalho(BlocoEventoContexto contexto) : base(contexto)
        {
            var ts = new TextStack(contexto, BoundingBox) {Height = 30, AlinhamentoVertical = AlinhamentoVertical.Centro, AlinhamentoHorizontal = AlinhamentoHorizontal.Centro, LineHeightScale = 2F}
                .AddLine(ViewModel.TituloEvento, Estilo.CriarFonteNegrito(15))
                .AddLine("Não possui valor fiscal. Simples representação do evento indicado abaixo.", Estilo.FonteCampoConteudo)
                .AddLine("CONSULTE A AUTENTICIDADE NO SITE DA SEFAZ AUTORIZADORA.", Estilo.FonteCampoConteudo);
            MainVerticalStack.Add(ts);
        }

        #endregion

        #region Properties

        public override bool PossuiContono => true;
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;

        #endregion
    }
}