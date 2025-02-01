using DanfeSharp.Graphics;
using org.pdfclown.documents.contents.xObjects;
using System;
using System.Drawing;


namespace DanfeSharp.NFCe
{
    internal class Imagem : ElementoBase
    {
        public XObject XImagem { get; set; }
        public float MaxHeightHorizontalImage { get; set; }

        public Imagem(ElementoContexto contexto) : base(contexto) {}

        public override bool PossuiContono => false;

        public override void Draw(Gfx gfx)
        {
            if (XImagem == null)
                return;

            base.Draw(gfx);

            var rp = BoundingBox;
            RectangleF rLogo;
            var maxHeight = MaxHeightHorizontalImage > 0 ? MaxHeightHorizontalImage : Height;

            // Logo Horizontal
            if (XImagem.Size.Width > XImagem.Size.Height)
            {
                rLogo = new RectangleF(rp.X, rp.Y, rp.Width, maxHeight);
            }
            // Logo Vertical / Quadrado
            else
            {
                float lw = rp.Height * XImagem.Size.Width / XImagem.Size.Height;
                float x = lw < rp.Width ? rp.X + ((rp.Width - lw) / 2) : rp.X; // Centralizar
                rLogo = new RectangleF(x, rp.Y, lw, rp.Height);
            }

            gfx.ShowXObject(XImagem, rLogo);
        }
    }
}