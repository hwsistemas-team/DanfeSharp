using DanfeSharp.Graphics;
using System;

namespace DanfeSharp
{
    /// <summary>
    /// Elemento básico no DANFE.
    /// </summary>
    internal abstract class ElementoBase : DrawableBase
    {
        internal new ElementoContexto Contexto => (ElementoContexto)base.Contexto;
        public Estilo Estilo => Contexto.Estilo;
        public virtual bool PossuiContono => true;
        public DanfeConfig Config => Contexto.Config;

        public ElementoBase(ElementoContexto contexto) : base(contexto) { }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);
            if (PossuiContono)
                gfx.StrokeRectangle(BoundingBox, 0.25f);
        }
    }
}
