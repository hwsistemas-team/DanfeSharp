using DanfeSharp.Graphics;
using System;
using pcf = org.pdfclown.documents.contents.fonts;

namespace DanfeSharp
{
    /// <summary>
    /// Coleção de fontes e medidas a serem compartilhadas entre os elementos básicos.
    /// </summary>
    internal class Estilo
    {
        public float PaddingSuperior { get; set; }
        public float PaddingInferior { get; set; }
        public float PaddingHorizontal { get; set; }
        public float FonteTamanhoMinimo { get; set; }

        public pcf.Font FonteInternaRegular { get; set; }
        public pcf.Font FonteInternaNegrito { get; set; }
        public pcf.Font FonteInternaItalico { get; set; }

        public Fonte FonteCampoCabecalho { get; private set; }
        public Fonte FonteCampoConteudo { get; private set; }
        public Fonte FonteCampoConteudoNegrito { get; private set; }
        public Fonte FonteBlocoCabecalho { get; private set; }
        public Fonte FonteNumeroFolhas { get; private set; }


        public Fonte FonteNFCe1 { get; private set; }
        public Fonte FonteNFCe2 { get; private set; }
        public Fonte FonteNFCe3 { get; private set; }
        public Fonte FonteNFCeNegrito1 { get; private set; }
        public Fonte FonteNFCeNegrito2 { get; private set; }
        public Fonte FonteNFCeNegrito3 { get; private set; }

        public Estilo(pcf.Font fontRegular, pcf.Font fontBold, pcf.Font fontItalic, float tamanhoFonteCampoCabecalho = 6, float tamanhoFonteConteudo = 10)
        {
            PaddingHorizontal = 0.75F;
            PaddingSuperior = 0.65F;
            PaddingInferior = 0.3F;

            FonteInternaRegular = fontRegular;
            FonteInternaNegrito = fontBold;
            FonteInternaItalico = fontItalic;

            FonteCampoCabecalho = CriarFonteRegular(tamanhoFonteCampoCabecalho);
            FonteCampoConteudo = CriarFonteRegular(tamanhoFonteConteudo);
            FonteCampoConteudoNegrito = CriarFonteNegrito(tamanhoFonteConteudo);
            FonteBlocoCabecalho = CriarFonteNegrito(7);
            FonteNumeroFolhas = CriarFonteNegrito(10F);
            FonteTamanhoMinimo = 5.75F;

            FonteNFCe1 = CriarFonteRegular(10);
            FonteNFCe2 = CriarFonteRegular(8);
            FonteNFCe3 = CriarFonteRegular(6);
            FonteNFCeNegrito1 = CriarFonteNegrito(10);
            FonteNFCeNegrito2 = CriarFonteNegrito(8);
            FonteNFCeNegrito3 = CriarFonteNegrito(6);
        }

        public Fonte CriarFonteRegular(float emSize) => new Fonte(FonteInternaRegular, emSize);
        public Fonte CriarFonteNegrito(float emSize) => new Fonte(FonteInternaNegrito, emSize);
        public Fonte CriarFonteItalico(float emSize) => new Fonte(FonteInternaItalico, emSize);

    }
}
