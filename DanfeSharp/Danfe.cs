using System;
using System.Collections.Generic;
using DanfeSharp.Blocos;
using DanfeSharp.Modelo;

namespace DanfeSharp
{
    internal class DanfeCtrl : DanfeBase<DanfeContext, BlocoNFeBase, DanfeViewModel>
    {
        public BlocoCanhoto Canhoto;
        public BlocoIdentificacaoEmitente IdentificacaoEmitente;

        public DanfeCtrl(DanfeViewModel viewModel, DanfeConfig config)
            : base(viewModel, config, new System.Drawing.SizeF(Constantes.A4Largura, Constantes.A4Altura))
        {
        }
    }

    public class Danfe : IDisposable
    {
        private bool _foiGerado = false;
        private bool _disposed = false;
        private List<DanfePagina> Paginas;
        private DanfeCtrl _ctrl;

        private DanfeContext Contexto => _ctrl.Contexto;
        private DanfeViewModel  ViewModel => _ctrl.Contexto.ViewModel;
        private DanfeConfig Config => _ctrl.Contexto.Config;


        public Danfe(DanfeViewModel viewModel, DanfeConfig config)
        {
            _ctrl = new DanfeCtrl(viewModel, config);

            Paginas = new List<DanfePagina>();
            _ctrl.Canhoto = _ctrl.CriarBloco<BlocoCanhoto>();
            _ctrl.IdentificacaoEmitente = _ctrl.AdicionarBloco<BlocoIdentificacaoEmitente>();
            _ctrl.AdicionarBloco<BlocoDestinatarioRemetente>();

            if (ViewModel.LocalRetirada != null && Config.ExibirBlocoLocalRetirada)
                _ctrl.AdicionarBloco<BlocoLocalRetirada>();

            if (ViewModel.LocalEntrega != null && Config.ExibirBlocoLocalEntrega)
                _ctrl.AdicionarBloco<BlocoLocalEntrega>();

            if (!String.IsNullOrEmpty(ViewModel.Fatura?.Numero) && Config.ExibirBlocoFatura)
                _ctrl.AdicionarBloco<BlocoFatura>();

            if (ViewModel.Duplicatas.Count > 0)
                _ctrl.AdicionarBloco<BlocoDuplicatas>();

            _ctrl.AdicionarBloco<BlocoCalculoImposto>(Config.Orientacao == Orientacao.Paisagem ? Contexto : Contexto.ComEstilo(_ctrl.CriarEstilo(4.75F)));
            _ctrl.AdicionarBloco<BlocoTransportador>();
            _ctrl.AdicionarBloco<BlocoDadosAdicionais>(Contexto.ComEstilo(_ctrl.CriarEstilo(tFonteCampoConteudo: 8)));

            if (ViewModel.CalculoIssqn.Mostrar)
                _ctrl.AdicionarBloco<BlocoCalculoIssqn>();

            _ctrl.AdicionarMetadata("DANFE", "DANFE (Documento auxiliar da NFe)");

            _foiGerado = false;
        }

        public void AdicionarLogoImagem(System.IO.Stream stream) => _ctrl.IdentificacaoEmitente.Logo = _ctrl.AdicionarImagem(stream);

        public void AdicionarLogoPdf(System.IO.Stream stream) => _ctrl.IdentificacaoEmitente.Logo = _ctrl.AdicionarPdf(stream);

        public void AdicionarLogoImagem(String path) => _ctrl.IdentificacaoEmitente.Logo = _ctrl.AdicionarImagem(path);

        public void AdicionarLogoPdf(String path) => _ctrl.IdentificacaoEmitente.Logo = _ctrl.AdicionarPdf(path);

        public void Gerar()
        {
            if (_foiGerado) throw new InvalidOperationException("O Danfe já foi gerado.");

            var tabela = new TabelaProdutosServicos(Contexto);

            while (true)
            {
                DanfePagina p = CriarPagina();

                tabela.SetPosition(p.Ctrl.RetanguloCorpo.Location);
                tabela.SetSize(p.Ctrl.RetanguloCorpo.Size);
                tabela.Draw(p.Ctrl.Gfx);

                p.Ctrl.Gfx.Stroke();
                p.Ctrl.Gfx.Flush();

                if (tabela.CompletamenteDesenhada) break;

            }

            PreencherNumeroFolhas();
            _foiGerado = true;

        }

        private DanfePagina CriarPagina()
        {
            DanfePagina p = new DanfePagina(_ctrl);
            Paginas.Add(p);
            p.DesenharBlocos(Paginas.Count == 1);
            p.Ctrl.DesenharCreditos();

            if (ViewModel.TipoAmbiente == 2 || ViewModel.Cancelada)
                p.DesenharAvisoHomologacao();

            return p;
        }

        internal void PreencherNumeroFolhas()
        {
            int nFolhas = Paginas.Count;
            for (int i = 0; i < Paginas.Count; i++)
            {
                Paginas[i].DesenhaNumeroPaginas(i + 1, nFolhas);
            }
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
