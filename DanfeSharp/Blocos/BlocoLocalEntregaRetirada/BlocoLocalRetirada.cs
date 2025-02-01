
namespace DanfeSharp.Blocos
{
    class BlocoLocalRetirada : BlocoLocalEntregaRetirada
    {
        public BlocoLocalRetirada(BlocoContexto contexto)
            : base(contexto, contexto.ViewModel.LocalRetirada)
        {
        }

        public override string Cabecalho => "Informações do local de retirada";
    }
}
