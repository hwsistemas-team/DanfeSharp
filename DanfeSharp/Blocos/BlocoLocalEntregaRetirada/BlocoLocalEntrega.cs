
namespace DanfeSharp.Blocos
{
    class BlocoLocalEntrega : BlocoLocalEntregaRetirada
    {
        public BlocoLocalEntrega(BlocoNFeContexto contexto)
            : base(contexto, contexto.ViewModel.LocalEntrega)
        {
        }

        public override string Cabecalho => "Informações do local de entrega";
    }
}
