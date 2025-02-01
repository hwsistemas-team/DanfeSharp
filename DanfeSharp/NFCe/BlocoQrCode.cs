using DanfeSharp.Blocos;
using org.pdfclown.documents.contents.xObjects;

namespace DanfeSharp.NFCe
{
    internal class BlocoQrCode : BlocoBase
    {
        private Imagem _qrcodeImage;

        public BlocoQrCode(BlocoContexto contexto) : base(contexto)
        {
            _qrcodeImage = new Imagem(contexto)
            {
                Height  = 50f,
                MaxHeightHorizontalImage = 40f
            };

            var fr = Estilo.FonteNFCe3;
            var ls = ElementoVazio.T0();
            var w = Contexto.RetanguloDesenhavel.Width;

            MainVerticalStack.Add(TextBlock.Centro(contexto, "Consulte via leitor QR Code", fr, w));
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