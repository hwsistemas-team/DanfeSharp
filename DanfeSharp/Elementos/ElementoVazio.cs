using DanfeSharp.Graphics;

namespace DanfeSharp
{
    internal class ElementoVazio : DrawableBase
    {
        public ElementoVazio(DrawableContexto contexto) : base(contexto)
        {
        }

        public override void Draw(Gfx gfx)
        {
        }

        public static ElementoVazio T3() => new ElementoVazio(new DrawableContexto()) { Height = 3 };
        public static ElementoVazio T0() => new ElementoVazio(new DrawableContexto()) { Height = 0.8f };
    }
}
