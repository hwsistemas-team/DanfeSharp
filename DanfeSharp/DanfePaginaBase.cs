using System;
using System.Drawing;
using System.Linq;
using DanfeSharp.Blocos;
using DanfeSharp.Graphics;
using org.pdfclown.documents;
using org.pdfclown.documents.contents.composition;

namespace DanfeSharp
{
    internal class DanfePaginaBase<TDanfe, TDanfeContext, TBloco, TViewModel>
        where TDanfe : DanfeBase<TDanfeContext, TBloco, TViewModel>
        where TBloco : BlocoBase<TViewModel>
        where TDanfeContext : BlocoContextoBase<TViewModel>, new()
        where TViewModel : class
    {
        public TDanfe Danfe { get; private set; }
        public Page PdfPage { get; private set; }
        public PrimitiveComposer PrimitiveComposer { get; private set; }
        public Gfx Gfx { get; private set; }
        public RectangleF RetanguloCorpo { get; private set; }
        public RectangleF RetanguloDesenhavel { get; set; }
        public RectangleF RetanguloCreditos { get; private set; }
        public RectangleF Retangulo { get; private set; }

        public DanfePaginaBase(TDanfe danfe)
        {
            Danfe = danfe ?? throw new ArgumentNullException(nameof(danfe));
            PdfPage = new Page(Danfe.PdfDocument);
            Danfe.PdfDocument.Pages.Add(PdfPage);

            Retangulo = Danfe.Contexto.Retangulo.Copy();
            RetanguloDesenhavel = Danfe.Contexto.RetanguloDesenhavel.Copy();
            RetanguloCreditos = new RectangleF(RetanguloDesenhavel.X, RetanguloDesenhavel.Bottom + Danfe.Contexto.Estilo.PaddingSuperior, RetanguloDesenhavel.Width, Retangulo.Height - RetanguloDesenhavel.Height - Danfe.Contexto.Estilo.PaddingSuperior);
            PdfPage.Size = new SizeF(Retangulo.Width.ToPoint(), Retangulo.Height.ToPoint());

            PrimitiveComposer = new PrimitiveComposer(PdfPage);

            Gfx = new Gfx(PrimitiveComposer);
        }

        public void DesenharCreditos()
        {
            Gfx.DrawString("Impresso com DanfeSharp", RetanguloCreditos, Danfe.Contexto.Estilo.CriarFonteItalico(6), AlinhamentoHorizontal.Direita);
        }

        public void DesenharBlocos(bool isPrimeirapagina = false, Action<TBloco> onDraw = null)
        {
            var blocos = isPrimeirapagina ? Danfe.Blocos : Danfe.Blocos.Where(x => x.VisivelSomentePrimeiraPagina == false);

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

                onDraw?.Invoke(bloco);
            }

            RetanguloCorpo = RetanguloDesenhavel;
            Gfx.Flush();
        }
    }
}