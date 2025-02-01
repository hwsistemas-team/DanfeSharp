using System;
using System.Drawing;
using DanfeSharp.Graphics;

namespace DanfeSharp
{
    /// <summary>
    /// Campo de única linha.
    /// </summary>
    [AlturaFixa]
    internal class Text : DrawableBase
    {
        private string[] _conteudo;
        private Fonte _fonte;

        public AlinhamentoHorizontal AlinhamentoHorizontal { get; set; }

        public Text(ElementoContexto contexto, string conteudo, Fonte fonte, AlinhamentoHorizontal alinhamentoHorizontal = AlinhamentoHorizontal.Esquerda)
            : this(contexto, new string[] { conteudo }, fonte, alinhamentoHorizontal)
        {
        }

        public Text(ElementoContexto contexto, string[] conteudo, Fonte fonte, AlinhamentoHorizontal alinhamentoHorizontal = AlinhamentoHorizontal.Esquerda) : base(contexto)
        {
            _conteudo = conteudo;
            _fonte = fonte;
            AlinhamentoHorizontal = alinhamentoHorizontal;
        }


        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            var y2 = BoundingBox.Y;

            foreach(var text in _conteudo)
            {
                var h = _fonte.AlturaLinha;
                var r = new RectangleF(BoundingBox.X, y2, BoundingBox.Width, h);
                gfx.DrawString(text, r, _fonte, AlinhamentoHorizontal, AlinhamentoVertical.Base);
                y2 += h;
            }
        }

        public override float Height { get => _conteudo.Length * _fonte.AlturaLinha; set => throw new NotSupportedException(); }
    }
}
