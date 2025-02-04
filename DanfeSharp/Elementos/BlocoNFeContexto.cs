using DanfeSharp.Modelo;

namespace DanfeSharp
{
    internal class BlocoNFeContexto: BlocoContextoBase<DanfeViewModel>
    {
        public new BlocoNFeContexto ComEstilo(Estilo estilo)
        {
            return new BlocoNFeContexto
            {
                ViewModel = ViewModel,
                Config = Config,
                Retangulo = Retangulo,
                RetanguloDesenhavel = RetanguloDesenhavel,
                Estilo = estilo
            };
        }
    }
}