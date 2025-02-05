using System.Drawing;
using DanfeSharp.Blocos;
using DanfeSharp.Graphics;
using DanfeSharp.Modelo;
using org.pdfclown.documents;
using org.pdfclown.documents.contents.composition;

namespace DanfeSharp.NFCe
{
    internal class DanfeNFCePaginaCtrl : DanfePaginaBase<DanfeNFCeCtrl, DanfeContext, BlocoNFeBase, DanfeViewModel>
    {
        public DanfeNFCePaginaCtrl(DanfeNFCeCtrl ctrl) : base(ctrl) { }
    }

    internal class DanfeNFCePagina
    {
        internal DanfeNFCePaginaCtrl Ctrl;

        private DanfeContext Contexto => Ctrl.Danfe.Contexto;
        private DanfeViewModel ViewModel => Contexto.ViewModel;
        private DanfeConfig Config => Contexto.Config;
        private PrimitiveComposer PrimitiveComposer => Ctrl.Gfx.PrimitiveComposer;
        private Gfx Gfx => Ctrl.Gfx;

        public DanfeNFCePagina(DanfeNFCeCtrl ctrl)
        {
            Ctrl = new DanfeNFCePaginaCtrl(ctrl);

            AjustarOrigemCoordenadasXY();
        }

        // Difinir posição origem (x,y) para o inicio da página (Fica incorreta em página cima de 300mm)
        private void AjustarOrigemCoordenadasXY()
        {
            PrimitiveComposer.BeginLocalState();
            PrimitiveComposer.Translate(0, Ctrl.PdfPage.Size.Height);
            PrimitiveComposer.Scale(1, -1);
            PrimitiveComposer.End();
        }

        public void AjustarTamanhoPagina()
        {
            float newHeight = (Ctrl.RetanguloDesenhavel.Y + Ctrl.Danfe.Contexto.Config.Margem).ToPoint();
            var pageContent = Ctrl.PdfPage.ToXObject(Ctrl.PdfPage.File.Document);

            var newPdfPage = new Page(Ctrl.PdfPage.File.Document);
            newPdfPage.Size = new SizeF(Ctrl.PdfPage.Size.Width, newHeight);
            Ctrl.Danfe.PdfDocument.Pages.Add(newPdfPage);

            var composer = new PrimitiveComposer(newPdfPage);
            composer.ShowXObject(pageContent, new PointF(0, 0));
            composer.Flush();

            Ctrl.Danfe.PdfDocument.Pages.Remove(Ctrl.PdfPage);
        }
    }
}
