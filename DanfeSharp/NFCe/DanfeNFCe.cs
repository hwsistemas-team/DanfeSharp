using System;
using System.Collections.Generic;
using DanfeSharp.Blocos;
using DanfeSharp.Modelo;
using org.pdfclown.documents;
using org.pdfclown.documents.contents.fonts;
using org.pdfclown.files;

namespace DanfeSharp.NFCe
{
    public class DanfeNFCe : IDisposable
    {
        public DanfeViewModel ViewModel { get; private set; }
        public File File { get; private set; }
        internal Document PdfDocument { get; private set; }

        internal BlocoEmitente BlocoEmitenteLogo { get; private set; }
        internal BlocoQrCode BlocoQrCode { get; private set; }

        internal List<BlocoBase> _Blocos;
        internal Estilo EstiloPadrao { get; private set; }

        internal List<DanfeNFCePagina> Paginas { get; private set; }

        private StandardType1Font _FonteRegular;
        private StandardType1Font _FonteNegrito;
        private StandardType1Font _FonteItalico;
        private StandardType1Font.FamilyEnum _FonteFamilia;

        private Boolean _FoiGerado;

        private org.pdfclown.documents.contents.xObjects.XObject _LogoObject = null;
        private org.pdfclown.documents.contents.xObjects.XObject _QRCodeObject = null;

        public DanfeNFCe(DanfeViewModel viewModel)
        {
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            _Blocos = new List<BlocoBase>();
            File = new File();
            PdfDocument = File.Document;

            // De acordo com o item 7.7, a fonte deve ser Times New Roman ou Courier New.
            _FonteFamilia = StandardType1Font.FamilyEnum.Helvetica;
            _FonteRegular = new StandardType1Font(PdfDocument, _FonteFamilia, false, false);
            _FonteNegrito = new StandardType1Font(PdfDocument, _FonteFamilia, true, false);
            _FonteItalico = new StandardType1Font(PdfDocument, _FonteFamilia, false, true);

            EstiloPadrao = CriarEstilo();

            if (ViewModel.Orientacao == Orientacao.Retrato)
            {
                ViewModel.PaginaAltura = Constantes.FolhaNFCeAltura;
                ViewModel.PaginaLargura = Constantes.FolhaNFCeLargura;
            }
            else
            {
                ViewModel.PaginaAltura = Constantes.FolhaNFCeLargura;
                ViewModel.PaginaLargura = Constantes.FolhaNFCeAltura;
            }

            Paginas = new List<DanfeNFCePagina>();

            BlocoEmitenteLogo = AdicionarBloco<BlocoEmitente>();
            AdicionarBloco<BlocoDanfeInfo>();
            AdicionarBloco<BlocoProdutos>();
            AdicionarBloco<BlocoTotais>();
            AdicionarBloco<BlocoPagamentos>();
            AdicionarBloco<BlocoEmissao>();
            AdicionarBloco<BlocoChaveAcesso>();
            AdicionarBloco<BlocoConsumidor>();
            if (ViewModel.QrCode != null)
                BlocoQrCode = AdicionarBloco<BlocoQrCode>();
            AdicionarBloco<BlocoProtocolo>();
            AdicionarBloco<BlocoTributos>();

            AdicionarMetadata();

            _FoiGerado = false;
        }

        public void AdicionarLogoImagem(System.IO.Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var img = org.pdfclown.documents.contents.entities.Image.Get(stream);
            if (img == null) throw new InvalidOperationException("O logotipo não pode ser carregado, certifique-se que a imagem esteja no formato JPEG não progressivo.");
            _LogoObject = img.ToXObject(PdfDocument);
        }

        public void AdicionarLogoPdf(System.IO.Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            using (var pdfFile = new org.pdfclown.files.File(new org.pdfclown.bytes.Stream(stream)))
            {
                _LogoObject = pdfFile.Document.Pages[0].ToXObject(PdfDocument);
            }
        }

        public void AdicionarLogoImagem(String path)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentException(nameof(path));

            using(var fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                AdicionarLogoImagem(fs);
            }
        }

        public void AdicionarLogoPdf(String path)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentException(nameof(path));

            using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                AdicionarLogoPdf(fs);
            }
        }

        public void AdicionarQrCodeImagem(System.IO.Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var img = org.pdfclown.documents.contents.entities.Image.Get(stream);
            if (img == null) throw new InvalidOperationException("O QRCode não pode ser carregado, certifique-se que a imagem esteja no formato JPEG não progressivo.");
            _QRCodeObject = img.ToXObject(PdfDocument);
        }


        private void AdicionarMetadata()
        {
            var info = PdfDocument.Information;
            info[new org.pdfclown.objects.PdfName("ChaveAcesso")] = ViewModel.ChaveAcesso;
            info[new org.pdfclown.objects.PdfName("TipoDocumento")] = "DANFE";
            info.CreationDate = DateTime.Now;
            info.Creator = String.Format("{0} {1} - {2}", "DanfeSharp", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version, "https://github.com/SilverCard/DanfeSharp");
            info.Title = "DANFE (Documento auxiliar da NFCe)";
        }

        public void Gerar()
        {
            if (_FoiGerado) throw new InvalidOperationException("O Danfe já foi gerado.");

            BlocoEmitenteLogo.Logo = _LogoObject;

            if (BlocoQrCode != null)
                BlocoQrCode.QRCode = _QRCodeObject;

            CriarPagina();

            _FoiGerado = true;

        }

        private DanfeNFCePagina CriarPagina()
        {
            var p = new DanfeNFCePagina(this);
            Paginas.Add(p);
            p.DesenharBlocos(Paginas.Count == 1);
            p.AjustarTamanhoPagina();

            return p;
        }

        private Estilo CriarEstilo(float tFonteCampoCabecalho = 6, float tFonteCampoConteudo = 10)
        {
            return new Estilo(_FonteRegular, _FonteNegrito, _FonteItalico, tFonteCampoCabecalho, tFonteCampoConteudo);
        }

        internal T CriarBloco<T>() where T : BlocoBase
        {
            var bloco = (T)Activator.CreateInstance(typeof(T), ViewModel, EstiloPadrao);
            return bloco;
        }

        internal T CriarBloco<T>(Estilo estilo) where T : BlocoBase
        {
            var bloco = (T)Activator.CreateInstance(typeof(T), ViewModel, estilo);
            return bloco;
        }

        internal T AdicionarBloco<T>() where T: BlocoBase
        {
            var bloco = CriarBloco<T>();
            _Blocos.Add(bloco);
            return bloco;
        }

        internal T AdicionarBloco<T>(Estilo estilo) where T : BlocoBase
        {
            var bloco = CriarBloco<T>(estilo);
            _Blocos.Add(bloco);
            return bloco;
        }

        internal void AdicionarBloco(BlocoBase bloco)
        {
            _Blocos.Add(bloco);
        }


        public void Salvar(String path)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentException(nameof(path));

            File.Save(path, SerializationModeEnum.Incremental);
        }

        public void Salvar(System.IO.Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            File.Save(new org.pdfclown.bytes.Stream(stream), SerializationModeEnum.Incremental);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    File.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Danfe() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}