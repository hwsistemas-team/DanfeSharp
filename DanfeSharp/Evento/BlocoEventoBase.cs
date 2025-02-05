using DanfeSharp.Blocos;
using DanfeSharp.Modelo;

namespace DanfeSharp.Evento
{
    internal abstract class BlocoEventoBase : BlocoBase<DanfeEventoViewModel>
    {
        protected BlocoEventoBase(BlocoContextoBase<DanfeEventoViewModel> contexto) : base(contexto)
        {
        }
    }
}