using DanfeSharp.Blocos;

namespace DanfeSharp.Evento
{
    internal class BlocoEventoCartaCorrecao : BlocoEventoBase
    {
        #region Constructors

        public BlocoEventoCartaCorrecao(BlocoEventoContexto contexto) : base(contexto)
        {
            var correcao = new CampoMultilinha(contexto, "", ViewModel.Correcao, AlinhamentoHorizontal.Esquerda) { Height = AlturaCorrecao, IsConteudoNegrito = true };
            var linha = new FlexibleLine(contexto) { Height = AlturaCorrecao };
            linha.ComElemento(correcao).ComLargurasIguais();

            MainVerticalStack.Add(linha);
        }

        #endregion

        #region Properties

        public const float AlturaCorrecao = Constantes.A4Altura - 132;

        public override string Cabecalho => "CORREÇÃO A SER CONSIDERADA";
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;

        #endregion
    }
}