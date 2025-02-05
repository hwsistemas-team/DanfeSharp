using DanfeSharp.Graphics;
using DanfeSharp.Modelo;
using org.pdfclown.documents.contents.colorSpaces;
using org.pdfclown.documents.contents.composition;

namespace DanfeSharp.Evento
{
    internal class DanfeEventoPaginaCtrl : DanfePaginaBase<DanfeEventoCtrl, DanfeEventoContexto, BlocoEventoBase, DanfeEventoViewModel>
    {
        public DanfeEventoPaginaCtrl(DanfeEventoCtrl ctrl) : base(ctrl) { }
    }

    internal class DanfeEventoPagina
    {
        internal DanfeEventoPaginaCtrl Ctrl;

        private DanfeEventoContexto Contexto => Ctrl.Danfe.Contexto;
        private DanfeEventoViewModel ViewModel => Contexto.ViewModel;
        private DanfeConfig Config => Contexto.Config;
        private PrimitiveComposer PrimitiveComposer => Ctrl.Gfx.PrimitiveComposer;
        private Gfx Gfx => Ctrl.Gfx;

        public DanfeEventoPagina(DanfeEventoCtrl ctrl)
        {
            Ctrl = new DanfeEventoPaginaCtrl(ctrl);
        }

        public void DesenharAvisoHomologacao()
        {
            var ts = new TextStack(Contexto, Ctrl.Retangulo)
            {
                AlinhamentoVertical = AlinhamentoVertical.Centro, AlinhamentoHorizontal = AlinhamentoHorizontal.Centro,
                LineHeightScale = 0.9F
            };

            ts.AddLine("SEM VALOR FISCAL", Contexto.Estilo.CriarFonteRegular(48))
              .AddLine("AMBIENTE DE HOMOLOGAÇÃO", Contexto.Estilo.CriarFonteRegular(30));

            PrimitiveComposer.BeginLocalState();
            PrimitiveComposer.SetFillColor(new DeviceRGBColor(0.35, 0.35, 0.35));
            ts.Draw(Gfx);
            PrimitiveComposer.End();
        }
    }
}