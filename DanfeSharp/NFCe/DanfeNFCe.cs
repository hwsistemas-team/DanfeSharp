using System;
using System.Drawing;
using DanfeSharp.Blocos;
using DanfeSharp.Modelo;

namespace DanfeSharp.NFCe
{
    internal class DanfeNFCeCtrl : DanfeBase<DanfeContext, BlocoNFeBase, DanfeViewModel>
    {
        public DanfeNFCeCtrl(DanfeViewModel viewModel, DanfeConfig config)
            : base(viewModel, config, new SizeF(Constantes.FolhaNFCeLargura, Constantes.FolhaNFCeAltura))
        {
        }
    }

    public class DanfeNFCe : IDisposable
    {
        private bool _foiGerado = false;
        private bool _disposed = false;
        private BlocoEmitente _blocoEmitenteLogo;
        private BlocoQrCode _blocoQrCode;
        private DanfeNFCeCtrl _ctrl;

        private DanfeContext Contexto => _ctrl.Contexto;
        private DanfeViewModel  ViewModel => _ctrl.Contexto.ViewModel;
        private DanfeConfig Config => _ctrl.Contexto.Config;


        public DanfeNFCe(DanfeViewModel viewModel, DanfeConfig config)
        {
            _ctrl = new DanfeNFCeCtrl(viewModel,config);

            _blocoEmitenteLogo = _ctrl.AdicionarBloco<BlocoEmitente>();
            _ctrl.AdicionarBloco<BlocoDanfeInfo>();

            if (Config.NFCeExibirItens)
                _ctrl.AdicionarBloco<BlocoProdutos>();

            _ctrl.AdicionarBloco<BlocoTotais>();
            _ctrl.AdicionarBloco<BlocoPagamentos>();
            _ctrl.AdicionarBloco<BlocoEmissao>();
            _ctrl.AdicionarBloco<BlocoChaveAcesso>();
            _ctrl.AdicionarBloco<BlocoConsumidor>();

            if (ViewModel.QrCode != null)
                _blocoQrCode = _ctrl.AdicionarBloco<BlocoQrCode>();

            _ctrl.AdicionarBloco<BlocoProtocolo>();
            _ctrl.AdicionarBloco<BlocoTributos>();

            _ctrl.AdicionarMetadata("DANFE", "DANFE (Documento auxiliar da NFCe)");

            _foiGerado = false;
        }

        public void AdicionarLogoImagem(System.IO.Stream stream) => _blocoEmitenteLogo.Logo = _ctrl.AdicionarImagem(stream);

        public void AdicionarLogoPdf(System.IO.Stream stream) => _blocoEmitenteLogo.Logo = _ctrl.AdicionarPdf(stream);

        public void AdicionarLogoImagem(String path) => _blocoEmitenteLogo.Logo = _ctrl.AdicionarImagem(path);

        public void AdicionarLogoPdf(String path) => _blocoEmitenteLogo.Logo = _ctrl.AdicionarPdf(path);

        public void AdicionarQrCodeImagem(System.IO.Stream stream) { if (_blocoQrCode != null) _blocoQrCode.QRCode = _ctrl.AdicionarImagem(stream); }


        public void Gerar()
        {
            if (_foiGerado) throw new InvalidOperationException("O Danfe já foi gerado.");

            CriarPagina();

            _foiGerado = true;

        }

        private DanfeNFCePagina CriarPagina()
        {
            var p = new DanfeNFCePagina(_ctrl);
            p.Ctrl.DesenharBlocos(true);
            p.AjustarTamanhoPagina();

            return p;
        }

        public void Salvar(String path) => _ctrl.Salvar(path);

        public void Salvar(System.IO.Stream stream) => _ctrl.Salvar(stream);


        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool isDisposing)
        {
            if (_disposed)
                return;

            if (isDisposing)
                _ctrl.Dispose();

            _disposed = true;
        }
    }
}