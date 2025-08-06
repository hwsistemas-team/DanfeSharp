using System;
using System.IO;
using DanfeSharp.Evento;
using DanfeSharp.Modelo;
using DanfeSharp.NFCe;
using QRCoder;
using SkiaSharp;

namespace DanfeSharp.Sample
{
    public class Program
    {
        public static void Main()
        {
           //SampleNFe();
           SampleNFCe();
           //SampleCCe();
           //SampleCanc();
        }

        private static void SampleNFe()
        {
            var baseDir = Path.Combine(Directory.GetCurrentDirectory(), "_data");
            var logoFilename = Path.Combine(baseDir, "logo.png");
            var xmlFilename = Path.Combine(baseDir, "nfe.xml");;
            var pdfFilename = Path.Combine(baseDir, "output", "nfe.pdf");

            //Cria o modelo a partir do arquivo Xml da NF-e.
            var modelo = DanfeViewModelCreator.CriarDeArquivoXml(xmlFilename);
            modelo.Cancelada = false;

            var config = new DanfeConfig
            {
                Margem = 5,
                QuantidadeCanhotos = 1,
                ExibirBlocoLocalEntrega = true,
                ExibirBlocoLocalRetirada = true,
                ExibirIcmsInterestadual = true,
                ExibirPisConfins = true,
                ExibirBlocoFatura = true,
                PreferirEmitenteNomeFantasia = false,
                Orientacao = Orientacao.Retrato,
                FormatoProdutoQuantidade = "#,0.00#",
                FormatoProdutoValorUnitario = "#,0.000#"
            };

            //Inicia o Danfe com o modelo criado

            using (var danfe = new Danfe(modelo, config))
            {
                danfe.AdicionarLogoImagem(logoFilename);
                danfe.Gerar();
                danfe.Salvar(pdfFilename);
            }
        }

        private static void SampleNFCe()
        {
            var baseDir = Path.Combine(Directory.GetCurrentDirectory(), "_data");
            var logoFilename = Path.Combine(baseDir, "logo.png");
            var xmlFilename = Path.Combine(baseDir, "nfce.xml");
            var pdfFilename = Path.Combine(baseDir, "output", "nfce.pdf");

            //Cria o modelo a partir do arquivo Xml da NFC-e.
            var modelo = DanfeViewModelCreator.CriarDeArquivoXml(xmlFilename);
            modelo.Cancelada = true;

            var config = new DanfeConfig
            {
                Margem = 5,
                NFCeItensEm2Linhas = true,
                NFCeExibirItens = true,
                Orientacao = Orientacao.Retrato,
                FormatoProdutoQuantidade = "#,0.00#",
                FormatoProdutoValorUnitario = "#,0.000#"
            };

            //Inicia o Danfe com o modelo criado
            using (var danfe = new DanfeNFCe(modelo, config))
            {
                danfe.AdicionarLogoImagem(logoFilename);

                if (modelo.QrCode != null)
                {
                    using (var qrCodeStream = GerarQRCode(modelo.QrCode))
                    {
                        danfe.AdicionarQrCodeImagem(qrCodeStream);
                    }
                }

                danfe.Gerar();
                danfe.Salvar(pdfFilename);
            }
        }

        private static void SampleCCe()
        {
            var baseDir = Path.Combine(Directory.GetCurrentDirectory(), "_data");
            var logoFilename = Path.Combine(baseDir, "logo.png");
            var xmlFilename = Path.Combine(baseDir, "cce-02.xml");
            var pdfFilename = Path.Combine(baseDir, "output", "cce.pdf");

            //Cria o modelo a partir do arquivo Xml da NF-e.
            var modelo = DanfeEventoViewModelCreator.CriarDeArquivoXml(xmlFilename);

            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Retrato,
                FormatoProdutoQuantidade = "#,0.00#",
                FormatoProdutoValorUnitario = "#,0.000#"
            };

            //Inicia o Danfe com o modelo criado
            using (var danfe = new DanfeEvento(modelo, config))
            {
                //danfe.AdicionarLogoImagem(logoFilename);

                danfe.Gerar();
                danfe.Salvar(pdfFilename);
            }
        }

        private static void SampleCanc()
        {
            var baseDir = Path.Combine(Directory.GetCurrentDirectory(), "_data");
            var logoFilename = Path.Combine(baseDir, "logo.png");
            var xmlFilename = Path.Combine(baseDir, "canc-02.xml");
            var pdfFilename = Path.Combine(baseDir, "output", "canc.pdf");

            //Cria o modelo a partir do arquivo Xml da NF-e.
            var modelo = DanfeEventoViewModelCreator.CriarDeArquivoXml(xmlFilename);

            var config = new DanfeConfig
            {
                Orientacao = Orientacao.Retrato,
                FormatoProdutoQuantidade = "#,0.00#",
                FormatoProdutoValorUnitario = "#,0.000#"
            };

            //Inicia o Danfe com o modelo criado
            using (var danfe = new DanfeEvento(modelo, config))
            {
                //danfe.AdicionarLogoImagem(logoFilename);

                danfe.Gerar();
                danfe.Salvar(pdfFilename);
            }
        }

        private static Stream GerarQRCode(string conteudo)
        {
            using (var qrGenerator = new QRCodeGenerator())
            using (var qrCodeData = qrGenerator.CreateQrCode(conteudo, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new BitmapByteQRCode(qrCodeData))
            {
                var qrCodeImageBytes = qrCode.GetGraphic(10);
                using (var qrCodeStream = new MemoryStream(qrCodeImageBytes))
                {
                    return PngToJpeg(qrCodeStream);
                }
            }
        }

        private static MemoryStream PngToJpeg(Stream pngStream)
        {
            using (var inputStream = new SKManagedStream(pngStream))
            using (var pngImage = SKBitmap.Decode(inputStream))
            {
                if (pngImage == null)
                    throw new InvalidOperationException("O stream fornecido não é um PNG válido.");

                var jpegStream = new MemoryStream();
                var jpegData = SKImage.FromBitmap(pngImage).Encode(SKEncodedImageFormat.Jpeg, 85);
                jpegData.SaveTo(jpegStream);
                jpegStream.Position = 0;

                return jpegStream;
            }
        }
    }
}