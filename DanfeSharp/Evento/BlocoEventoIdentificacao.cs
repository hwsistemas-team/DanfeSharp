
using DanfeSharp.Blocos;
using DanfeSharp.Elementos;

namespace DanfeSharp.Evento
{
    internal class BlocoEventoIdentificacao : BlocoEventoBase
    {
        public BlocoEventoIdentificacao(BlocoEventoContexto contexto) : base(contexto)
        {
            var chaveAcesso = Formatador.FormatarChaveAcesso(ViewModel.ChaveAcesso);
            var modelo = ViewModel.ChaveAcesso.Substring(20, 2);
            var serie = ViewModel.ChaveAcesso.Substring(22, 3);
            var numero = Formatador.FormatarNumeroNF(ViewModel.ChaveAcesso.Substring(25, 9));
            var ano = ViewModel.ChaveAcesso.Substring(2, 2);
            var mes = ViewModel.ChaveAcesso.Substring(4, 2);

            var codigoBarras = new Barcode128C(contexto, ViewModel.ChaveAcesso) {Height = AlturaCodigo};

            var coluna = new VerticalStack(contexto);
            var linha1 = new LinhaCampos(contexto) {Height = AlturaLinhaCampo}
                .ComCampo("MODELO", modelo)
                .ComCampo("SÉRIE", serie)
                .ComCampo("NÚMERO", numero)
                .ComCampo("MÊS / ANO DA EMISSÃO", $"{mes} / {ano}")
                .ComLargurasIguais();

            var linha2 = new Campo(contexto, "CHAVE DE ACESSO", chaveAcesso) {Height = AlturaLinhaCampo};

            coluna.Add(linha1, linha2);

            var fl = new FlexibleLine(contexto) { Height = codigoBarras.Height }
                .ComElemento(coluna)
                .ComElemento(codigoBarras)
                .ComLargurasIguais();

            MainVerticalStack.Add(fl);
        }


        public const float AlturaCodigo = 15;
        public const float AlturaLinhaCampo = 7.5f;

        public override string Cabecalho => "IDENTIFICAÇÃO";
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
    }
}