using DanfeSharp.Modelo;

namespace DanfeSharp.Evento
{
    internal class BlocoEventoContexto: BlocoContextoBase<DanfeEventoViewModel>
    {
        public new BlocoEventoContexto ComEstilo(Estilo estilo)
        {
            return new BlocoEventoContexto
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