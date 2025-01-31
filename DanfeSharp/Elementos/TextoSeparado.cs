using System;
using DanfeSharp.Graphics;

namespace DanfeSharp
{
    internal class TextoSeparado : DrawableBase
    {
        private FlexibleLine _line;

        public TextoSeparado(string textoEsquerda, string textoDireta, float larguaPEsquerda, float larguraPDireita, Fonte fonte)
        {
            _line = new FlexibleLine() { Height = fonte.AlturaLinha }
                .ComElemento(new TextBlock(textoEsquerda, fonte))
                .ComElemento(new TextBlock(textoDireta, fonte) { AlinhamentoHorizontal = AlinhamentoHorizontal.Direita })
                .ComLarguras(larguaPEsquerda, larguraPDireita);
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            _line.X = X;
            _line.Y = Y;
            _line.Width = Width;
           _line.Draw(gfx);
        }

        public override float Height { get => _line.Height; set => throw new NotSupportedException(); }

        public override float Width { get => base.Width; set { base.Width = value; _line.Width = value; } }
    }
}