using DanfeSharp.Blocos;

namespace DanfeSharp.NFCe
{
    internal class BlocoTributos : BlocoNFeBase
    {
        public BlocoTributos(BlocoNFeContexto contexto) : base(contexto)
        {
            var fr = Estilo.FonteNFCe3;

            MainVerticalStack.Add(new LinhaSolida(contexto, 1));
            MainVerticalStack.Add(TextBlock.Centro(contexto, $"Tributos Totais Incidentes (Lei Federal 12.741/2012): {ViewModel.CalculoImposto.ValorAproximadoTributos.FormatarMoeda()}", fr));
            MainVerticalStack.Add(new LinhaSolida(contexto, 1));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}