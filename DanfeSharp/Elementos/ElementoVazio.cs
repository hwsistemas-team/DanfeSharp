using DanfeSharp.Graphics;

namespace DanfeSharp
{
    internal class ElementoVazio : DrawableBase
    {
        public override void Draw(Gfx gfx)
        {
        }

        public static ElementoVazio T3() => new ElementoVazio { Height = 3 };
        public static ElementoVazio T0() => new ElementoVazio { Height = 0.8f };
    }
}
