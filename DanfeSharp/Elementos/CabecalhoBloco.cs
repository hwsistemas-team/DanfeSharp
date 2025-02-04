using System;
using System.Drawing;
using DanfeSharp.Graphics;

namespace DanfeSharp
{
    /// <summary>
    /// Cabeçalho do bloco, normalmente um texto em caixa alta.
    /// </summary>
    internal class CabecalhoBloco : ElementoBase
    {
        public const float MargemSuperior = 3F;
        public const float MargemInferior = 0.6F;
        public String Cabecalho { get; set; }

        public CabecalhoBloco(ElementoContexto contexto, String cabecalho) : base(contexto)
        {
            Cabecalho = cabecalho ?? throw new ArgumentNullException(cabecalho);
        }

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            var r = new RectangleF(X, Y - MargemInferior, Width, Height);
            gfx.DrawString(Cabecalho.ToUpper(), r, Estilo.FonteBlocoCabecalho,
                AlinhamentoHorizontal.Esquerda, AlinhamentoVertical.Base );
        }

        public override float Height { get => MargemSuperior + MargemInferior + Estilo.FonteBlocoCabecalho.AlturaLinha; set => throw new NotSupportedException(); }
        public override bool PossuiContono => false;
    }
}
