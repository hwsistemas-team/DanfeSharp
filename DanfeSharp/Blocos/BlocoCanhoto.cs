using DanfeSharp.Elementos;

namespace DanfeSharp.Blocos
{
    internal class BlocoCanhoto : BlocoNFeBase
    {
        public const float TextoRecebimentoAltura = 10;
        public const float AlturaLinha2 = 9;

        public BlocoCanhoto(BlocoNFeContexto contexto) : base(contexto)
        {
            var textoRecebimento = new TextoSimples(contexto, ViewModel.TextoRecebimento) { Height = TextoRecebimentoAltura, TamanhoFonte = 8 };
            var nfe = new NumeroNfSerie(contexto, ViewModel.NfNumero.ToString(Formatador.FormatoNumeroNF), ViewModel.NfSerie.ToString()) { Height = AlturaLinha2 + TextoRecebimentoAltura, Width = 30 };

            var campos = new LinhaCampos(contexto) { Height = AlturaLinha2 }
               .ComCampo("Data de Recebimento", null)
               .ComCampo("Identificação e assinatura do recebedor", null)
               .ComLarguras(50, 0);

            var coluna1 = new VerticalStack(contexto);
            coluna1.Add(textoRecebimento, campos);

            var linha = new FlexibleLine(contexto) {Height = coluna1.Height }
            .ComElemento(coluna1)
            .ComElemento(nfe)
            .ComLarguras(0, 16);

            MainVerticalStack.Add(linha, new LinhaTracejada(contexto, 2));

        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;

    }
}
