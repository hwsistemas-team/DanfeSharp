using System;
using System.Drawing;
using DanfeSharp.Blocos;
using DanfeSharp.Graphics;
using DanfeSharp.Modelo;
using org.pdfclown.documents.contents.composition;

namespace DanfeSharp
{
    internal class DanfePaginaCtrl : DanfePaginaBase<DanfeCtrl, DanfeContext, BlocoNFeBase, DanfeViewModel>
    {
        public DanfePaginaCtrl(DanfeCtrl ctrl) : base(ctrl) { }
    }

    internal class DanfePagina
    {
        internal DanfePaginaCtrl Ctrl;
        internal RectangleF RetanguloNumeroFolhas;

        private DanfeContext Contexto => Ctrl.Danfe.Contexto;
        private DanfeViewModel ViewModel => Contexto.ViewModel;
        private DanfeConfig Config => Contexto.Config;
        private PrimitiveComposer PrimitiveComposer => Ctrl.Gfx.PrimitiveComposer;
        private Gfx Gfx => Ctrl.Gfx;

        public DanfePagina(DanfeCtrl ctrl)
        {
            Ctrl = new DanfePaginaCtrl(ctrl);
        }


        private void DesenharCanhoto()
        {
            if (Config.QuantidadeCanhotos == 0) return;

            var canhoto = Ctrl.Danfe.Canhoto;

            canhoto.SetPosition(Ctrl.RetanguloDesenhavel.Location);

            if (Config.Orientacao == Orientacao.Retrato)
            {
                canhoto.Width = Ctrl.RetanguloDesenhavel.Width;

                for (int i = 0; i < Config.QuantidadeCanhotos; i++)
                {
                    canhoto.Draw(Gfx);
                    canhoto.Y += canhoto.Height;
                }

                Ctrl.RetanguloDesenhavel = Ctrl.RetanguloDesenhavel.CutTop(canhoto.Height * Config.QuantidadeCanhotos);
            }
            else
            {
                canhoto.Width = Ctrl.RetanguloDesenhavel.Height;
                PrimitiveComposer.BeginLocalState();
                PrimitiveComposer.Rotate(90, new PointF(0, canhoto.Width + canhoto.X + canhoto.Y).ToPointMeasure());

                for (int i = 0; i < Config.QuantidadeCanhotos; i++)
                {
                    canhoto.Draw(Gfx);
                    canhoto.Y += canhoto.Height;
                }

                PrimitiveComposer.End();
                Ctrl.RetanguloDesenhavel = Ctrl.RetanguloDesenhavel.CutLeft(canhoto.Height * Config.QuantidadeCanhotos);
            }
        }

        public void DesenhaNumeroPaginas(int n, int total)
        {
            if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n));
            if (total <= 0) throw new ArgumentOutOfRangeException(nameof(n));
            if (n > total) throw new ArgumentOutOfRangeException("O número da página atual deve ser menor que o total.");

            Gfx.DrawString($"Folha {n}/{total}", RetanguloNumeroFolhas, Contexto.Estilo.FonteNumeroFolhas, AlinhamentoHorizontal.Centro);
            Gfx.Flush();
        }

        public void DesenharAvisoHomologacao()
        {
            // Ambiente de homologação
            // 7. O DANFE emitido para representar NF-e cujo uso foi autorizado em ambiente de
            // homologação sempre deverá conter a frase “SEM VALOR FISCAL” no quadro “Informações
            // Complementares” ou em marca d’água destacada.

            var ts = new TextStack(Contexto, Ctrl.RetanguloCorpo)
            {
                AlinhamentoVertical = AlinhamentoVertical.Centro,
                AlinhamentoHorizontal = AlinhamentoHorizontal.Centro,
                LineHeightScale = 0.9F
            };

            var f1 = Contexto.Estilo.CriarFonteRegular(48);
            var f2 = Contexto.Estilo.CriarFonteRegular(30);

            ts.AddLine("SEM VALOR FISCAL", f1);

            if (ViewModel.TipoAmbiente == 2)
                ts.AddLine("AMBIENTE DE HOMOLOGAÇÃO", f2);

            if (ViewModel.Cancelada)
                ts.AddLine("CANCELADA", f2);

            PrimitiveComposer.BeginLocalState();
            PrimitiveComposer.SetFillColor(new org.pdfclown.documents.contents.colorSpaces.DeviceRGBColor(0.35, 0.35, 0.35));
            ts.Draw(Gfx);
            PrimitiveComposer.End();
        }

        public void DesenharBlocos(bool isPrimeirapagina = false)
        {
            if (isPrimeirapagina && Config.QuantidadeCanhotos > 0) DesenharCanhoto();

            Ctrl.DesenharBlocos(isPrimeirapagina, bloco =>
            {
                if (bloco is BlocoIdentificacaoEmitente blocoEmitente)
                    RetanguloNumeroFolhas = blocoEmitente.RetanguloNumeroFolhas;
            });
        }
    }
}
