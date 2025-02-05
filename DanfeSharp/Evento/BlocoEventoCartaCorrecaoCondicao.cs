using DanfeSharp.Blocos;

namespace DanfeSharp.Evento
{
    internal class BlocoEventoCartaCorrecaoCondicao : BlocoEventoBase
    {
        #region Constructors

        public BlocoEventoCartaCorrecaoCondicao(BlocoEventoContexto contexto) : base(contexto)
        {
            var condicao = new CampoMultilinha(contexto, "", ViewModel.CondicaoUso) { Height = AlturaCondicao };
            var linha = new FlexibleLine(contexto) { Height = AlturaCondicao };
            linha.ComElemento(condicao).ComLargurasIguais();

            MainVerticalStack.Add(linha);
        }

        #endregion

        #region Properties

        public const float AlturaCondicao = 25;

        public override string Cabecalho => "Condição de Uso";
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;

        #endregion
    }
}