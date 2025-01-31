using System;
using System.Drawing;
using System.Linq;
using DanfeSharp.Blocos;
using DanfeSharp.Graphics;
using org.pdfclown.documents;
using org.pdfclown.documents.contents.composition;

namespace DanfeSharp.NFCe
{
    internal class DanfeNFCePagina
    {
        public DanfeNFCe Danfe { get; private set; }
        public Page PdfPage { get; private set; }
        public PrimitiveComposer PrimitiveComposer { get; private set; }
        public Gfx Gfx { get; private set; }
        public RectangleF RetanguloCorpo { get; private set; }
        public RectangleF RetanguloDesenhavel { get; private set; }
        public RectangleF Retangulo { get; private set; }

        public DanfeNFCePagina(DanfeNFCe danfe)
        {
            Danfe = danfe ?? throw new ArgumentNullException(nameof(danfe));
            PdfPage = new Page(Danfe.PdfDocument);
            Danfe.PdfDocument.Pages.Add(PdfPage);

            Retangulo = new RectangleF(0, 0, Danfe.ViewModel.PaginaLargura,  Danfe.ViewModel.PaginaAltura);
            RetanguloDesenhavel = Retangulo.InflatedRetangle(Danfe.ViewModel.Margem);
            PdfPage.Size = new SizeF(Retangulo.Width.ToPoint(), Retangulo.Height.ToPoint());

            PrimitiveComposer = new PrimitiveComposer(PdfPage);

            // Difinir posição origem (x,y) para o inicio da página (Fica incorreta em página cima de 300mm)
            PrimitiveComposer.BeginLocalState();
            PrimitiveComposer.Translate(0, PdfPage.Size.Height);
            PrimitiveComposer.Scale(1, -1);
            PrimitiveComposer.End();

            Gfx = new Gfx(PrimitiveComposer);
        }

        public void AjustarTamanhoPagina()
        {
            float newHeight = (RetanguloDesenhavel.Y + Danfe.ViewModel.Margem).ToPoint();
            var pageContent = PdfPage.ToXObject(PdfPage.File.Document);

            var newPdfPage = new Page(PdfPage.File.Document);
            newPdfPage.Size = new SizeF(PdfPage.Size.Width, newHeight);
            Danfe.PdfDocument.Pages.Add(newPdfPage);

            var composer = new PrimitiveComposer(newPdfPage);
            composer.ShowXObject(pageContent, new PointF(0, 0));
            composer.Flush();

            Danfe.PdfDocument.Pages.Remove(PdfPage);
        }

        public void DesenharBlocos(bool isPrimeirapagina = false)
        {
            var blocos = isPrimeirapagina ? Danfe._Blocos : Danfe._Blocos.Where(x => x.VisivelSomentePrimeiraPagina == false);

            foreach (var bloco in blocos)
            {
                bloco.Width = RetanguloDesenhavel.Width;

                if (bloco.Posicao == PosicaoBloco.Topo)
                {
                    bloco.SetPosition(RetanguloDesenhavel.Location);
                    RetanguloDesenhavel = RetanguloDesenhavel.CutTop(bloco.Height);
                }
                else
                {
                    bloco.SetPosition(RetanguloDesenhavel.X, RetanguloDesenhavel.Bottom - bloco.Height);
                    RetanguloDesenhavel = RetanguloDesenhavel.CutBottom(bloco.Height);
                }

                bloco.Draw(Gfx);
            }

            RetanguloCorpo = RetanguloDesenhavel;
            Gfx.Flush();
        }
    }
}
