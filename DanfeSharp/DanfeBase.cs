using System;
using System.Collections.Generic;
using System.Drawing;
using DanfeSharp.Blocos;
using org.pdfclown.documents;
using org.pdfclown.documents.contents.fonts;
using org.pdfclown.documents.contents.xObjects;
using org.pdfclown.files;
using org.pdfclown.objects;

namespace DanfeSharp
{
    internal class DanfeBase<TDanfeContext, TBloco, TViewModel> : IDisposable
        where TBloco : BlocoBase<TViewModel>
        where TDanfeContext : BlocoContextoBase<TViewModel>, new()
        where TViewModel : class
    {
        private bool _disposed = false;
        private readonly StandardType1Font _fonteRegular;
        private readonly StandardType1Font _fonteNegrito;
        private readonly StandardType1Font _fonteItalico;
        private readonly StandardType1Font.FamilyEnum _fonteFamilia;
        internal List<TBloco> Blocos { get; private set; }
        internal TDanfeContext Contexto { get; private set; }
        internal Document PdfDocument { get; private set; }
        internal File File { get; private set; }

        public DanfeBase(TViewModel viewModel, DanfeConfig config, SizeF size)
        {
            Blocos = new List<TBloco>();
            File = new File();
            PdfDocument = File.Document;

            // De acordo com o item 7.7, a fonte deve ser Times New Roman ou Courier New.
            _fonteFamilia = StandardType1Font.FamilyEnum.Times;
            _fonteRegular = new StandardType1Font(PdfDocument, _fonteFamilia, false, false);
            _fonteNegrito = new StandardType1Font(PdfDocument, _fonteFamilia, true, false);
            _fonteItalico = new StandardType1Font(PdfDocument, _fonteFamilia, false, true);

            Contexto = new TDanfeContext
            {
                Estilo = CriarEstilo(),
                ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel)),
                Config = config ?? throw new ArgumentNullException(nameof(config))
            };

            if (Contexto.Config.Orientacao == Orientacao.Retrato)
                Contexto.Retangulo = new RectangleF(0, 0, size.Width, size.Height);
            else
                Contexto.Retangulo = new RectangleF(0, 0, size.Height, size.Width);

            Contexto.RetanguloDesenhavel = Contexto.Retangulo.InflatedRetangle(Contexto.Config.Margem);
        }

        internal T CriarBloco<T>() where T : TBloco
        {
            return (T)Activator.CreateInstance(typeof(T), Contexto);
        }

        internal T CriarBloco<T>(BlocoContextoBase<TViewModel>  contexto) where T : TBloco
        {
            return (T)Activator.CreateInstance(typeof(T), contexto);
        }

        internal T AdicionarBloco<T>() where T: TBloco
        {
            var bloco = CriarBloco<T>();
            Blocos.Add(bloco);
            return bloco;
        }

        internal T AdicionarBloco<T>(BlocoContextoBase<TViewModel>  contexto) where T : TBloco
        {
            var bloco = CriarBloco<T>(contexto);
            Blocos.Add(bloco);
            return bloco;
        }

        internal void AdicionarBloco(TBloco bloco)
        {
            Blocos.Add(bloco);
        }

        internal Estilo CriarEstilo(float tFonteCampoCabecalho = 6, float tFonteCampoConteudo = 10)
        {
            return new Estilo(_fonteRegular, _fonteNegrito, _fonteItalico, tFonteCampoCabecalho, tFonteCampoConteudo);
        }

        internal void AdicionarMetadata(string tipo, string titulo)
        {
            var info = PdfDocument.Information;
            info[new PdfName("TipoDocumento")] = tipo;
            info.CreationDate = DateTime.Now;
            info.Title = titulo;
            info.Creator = String.Format("{0} {1} - {2}", "DanfeSharp", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version, "https://github.com/SilverCard/DanfeSharp");
        }

        internal void Salvar(String path)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentException(nameof(path));

            File.Save(path, SerializationModeEnum.Incremental);
        }

        internal void Salvar(System.IO.Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            File.Save(new org.pdfclown.bytes.Stream(stream), SerializationModeEnum.Incremental);
        }

        internal XObject AdicionarImagem(System.IO.Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var img = org.pdfclown.documents.contents.entities.Image.Get(stream);
            if (img == null) throw new InvalidOperationException("O logotipo não pode ser carregado, certifique-se que a imagem esteja no formato JPEG não progressivo.");

            return img.ToXObject(PdfDocument);
        }

        internal XObject AdicionarPdf(System.IO.Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            using (var pdfFile = new org.pdfclown.files.File(new org.pdfclown.bytes.Stream(stream)))
            {
                return pdfFile.Document.Pages[0].ToXObject(PdfDocument);
            }
        }

        internal XObject AdicionarImagem(String path)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentException(nameof(path));

            using(var fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                return AdicionarImagem(fs);
            }
        }

        internal XObject AdicionarPdf(String path)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentException(nameof(path));

            using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                return AdicionarPdf(fs);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                File.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}