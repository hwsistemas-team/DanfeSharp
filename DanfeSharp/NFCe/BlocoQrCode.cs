using DanfeSharp.Blocos;
using DanfeSharp.Modelo;
using org.pdfclown.documents.contents.xObjects;

namespace DanfeSharp.NFCe
{
    internal class BlocoQrCode : BlocoBase
    {
        private Imagem _qrcodeImage;

        public BlocoQrCode(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            _qrcodeImage = new Imagem(estilo)
            {
                Height  = 50f,
                MaxHeightHorizontalImage = 40f
            };

            var fr = Estilo.FonteNFCe3;
            var ls = ElementoVazio.T0();
            var w = viewModel.PaginaLargura;

            MainVerticalStack.Add(TextBlock.Centro("Consulte via leitor QR Code", fr, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(_qrcodeImage);
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;

        public XObject QRCode
        {
            get => _qrcodeImage.XImagem;
            set => _qrcodeImage.XImagem = value;
        }
    }
}