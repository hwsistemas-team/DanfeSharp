using DanfeSharp.Modelo;

namespace DanfeSharp
{
    internal class BlocoContexto : ElementoContexto
    {
        public DanfeViewModel ViewModel { get; set; }

        public BlocoContexto ComEstilo(Estilo estilo)
        {
            return new BlocoContexto
            {
                ViewModel = ViewModel,
                Config = Config,
                Retangulo = Retangulo,
                RetanguloDesenhavel = RetanguloDesenhavel,
                Estilo = estilo,
            };
        }
    }
}