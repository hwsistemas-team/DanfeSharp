using DanfeSharp.Blocos;

namespace DanfeSharp.Evento
{
    internal class BlocoEventoDados : BlocoEventoBase
    {
        public BlocoEventoDados(BlocoEventoContexto contexto) : base(contexto)
        {
            var ambiente = ViewModel.TipoAmbiente == 1 ? "PRODUÇÃO" : "HOMOLOGAÇÃO";

            AdicionarLinhaCampos()
                .ComCampo("ORGÃO", ViewModel.Orgao)
                .ComCampo("AMBIENTE", ambiente)
                .ComCampo("DATA / HORA DO EVENTO", ViewModel.DataHoraEvento.FormatarDataHora(), AlinhamentoHorizontal.Centro)
                .ComLarguras(30F * Proporcao, 0, 45F * Proporcao);

            AdicionarLinhaCampos()
                .ComCampo("EVENTO",((int)ViewModel.TipoEvento).ToString())
                .ComCampo("DESCRIÇÃO DO EVENTO", ViewModel.DescricaoEvento)
                .ComCampo("SEQUÊNCIA DO EVENTO", ViewModel.SequenciaEvento.ToString())
                .ComLarguras(30F * Proporcao, 0, 45F * Proporcao);

            AdicionarLinhaCampos()
                .ComCampo("STATUS", $"{ViewModel.CodigoStatus} - {ViewModel.Motivo}")
                .ComCampo("PROTOCOLO", ViewModel.Protocolo)
                .ComLarguras(0, 45F * Proporcao);
        }

        public override string Cabecalho => ViewModel.TituloEvento;
        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
    }
}