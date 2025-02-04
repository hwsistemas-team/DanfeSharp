
namespace DanfeSharp.Blocos
{
    class BlocoLocalRetirada : BlocoLocalEntregaRetirada
    {
        public BlocoLocalRetirada(BlocoNFeContexto contexto)
            : base(contexto, contexto.ViewModel.LocalRetirada)
        {
        }

        public override string Cabecalho => "Informações do local de retirada";
    }
}
