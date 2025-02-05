using System;
using DanfeSharp.Modelo;
using DanfeSharp.Esquemas;

namespace DanfeSharp.Evento
{
    internal class DanfeEventoCtrl : DanfeBase<DanfeEventoContexto, BlocoEventoBase, DanfeEventoViewModel>
    {
        public DanfeEventoCtrl(DanfeEventoViewModel viewModel, DanfeConfig config)
            : base(viewModel, config, new System.Drawing.SizeF(Constantes.A4Largura, Constantes.A4Altura))
        {
        }
    }

    public class DanfeEvento : IDisposable
    {
        private bool _foiGerado = false;
        private bool _disposed = false;
        private DanfeEventoCtrl _ctrl;

        private DanfeEventoContexto Contexto => _ctrl.Contexto;
        private DanfeEventoViewModel  ViewModel => _ctrl.Contexto.ViewModel;
        private DanfeConfig Config => _ctrl.Contexto.Config;


        public DanfeEvento(DanfeEventoViewModel viewModel, DanfeConfig config)
        {
            _ctrl = new DanfeEventoCtrl(viewModel, config);

            var estiloInformacao = _ctrl.CriarEstilo(tFonteCampoConteudo: 12f);
            estiloInformacao.FonteBlocoCabecalho.Tamanho = 10;

            _ctrl.AdicionarBloco<BlocoEventoCabecalho>();
            _ctrl.AdicionarBloco<BlocoEventoIdentificacao>();
            _ctrl.AdicionarBloco<BlocoEventoDados>();

            switch (viewModel.TipoEvento)
            {
                case NFeTipoEvento.CartaCorrecao:
                    _ctrl.AdicionarBloco<BlocoEventoCartaCorrecaoCondicao>();
                    _ctrl.AdicionarBloco<BlocoEventoCartaCorrecao>(_ctrl.Contexto.ComEstilo(estiloInformacao));
                    break;
                case NFeTipoEvento.Cancelamento:
                case NFeTipoEvento.CancelamentoST:
                    _ctrl.AdicionarBloco<BlocoEventoCancelamento>(_ctrl.Contexto.ComEstilo(estiloInformacao));
                    break;
            }

            _ctrl.AdicionarMetadata("DANFE Evento", "DANFE Evento (Documento auxiliar do evento da NFe)");
        }

        public void Gerar()
        {
            if (_foiGerado) throw new InvalidOperationException("O Danfe já foi gerado.");

            var page = new DanfeEventoPagina(_ctrl);
            page.Ctrl.DesenharBlocos(true);
            page.Ctrl.DesenharCreditos();

            if (ViewModel.TipoAmbiente == 2)
                page.DesenharAvisoHomologacao();

            page.Ctrl.Gfx.Flush();

            _foiGerado = true;
        }

        public void Salvar(string path) => _ctrl.Salvar(path);
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
