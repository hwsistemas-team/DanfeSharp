using System;
using DanfeSharp.Blocos;
using org.pdfclown.documents.contents.xObjects;

namespace DanfeSharp.NFCe
{
    internal class BlocoEmitente : BlocoBase
    {
        Imagem _emitenteLogo;

        public BlocoEmitente(BlocoContexto contexto) : base(contexto)
        {
            _emitenteLogo = new Imagem(contexto)
            {
                Height = 0,
                MaxHeightHorizontalImage = 0
            };

            var fn = Estilo.FonteNFCeNegrito2;
            var fr = Estilo.FonteNFCe3;
            var ls = ElementoVazio.T0();
            var lv3 = ElementoVazio.T3();
            var w = Contexto.RetanguloDesenhavel.Width;

            var emit = ViewModel.Emitente;

            MainVerticalStack.Add(_emitenteLogo);
            MainVerticalStack.Add(lv3);
            MainVerticalStack.Add(TextBlock.Centro(contexto, emit.NomeFantasia, fn, w));
            MainVerticalStack.Add(lv3);
            MainVerticalStack.Add(TextBlock.Esquerda(contexto, emit.RazaoSocial, fr, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(new TextoSeparado(contexto, "CNPJ: " + emit.CnpjCpf, "IE: " + emit.Ie, 50, 50, fr));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Esquerda(contexto, $"{emit.EnderecoLinha1} {emit.EnderecoLinha2} {emit.EnderecoLinha3}", fr, w));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;

        public XObject Logo
        {
            get => _emitenteLogo.XImagem;
            set
            {
                _emitenteLogo.XImagem = value;
                _emitenteLogo.Height = value != null ? 30F : 0;
                _emitenteLogo.MaxHeightHorizontalImage = value != null ? 30F : 0;
            }
        }
    }
}