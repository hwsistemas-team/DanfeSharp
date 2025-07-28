using System;
using System.Collections.Generic;
using DanfeSharp.Blocos;

namespace DanfeSharp.NFCe
{
    internal class BlocoEmissao : BlocoNFeBase
    {
        public BlocoEmissao(BlocoNFeContexto contexto) : base(contexto)
        {
            var fr = Estilo.FonteNFCe3;
            var fn = Estilo.FonteNFCeNegrito3;
            var ls = ElementoVazio.T0();
            var lv3 = ElementoVazio.T3();
            var aviso = new List<string>();

            if (ViewModel.TipoAmbiente == 2)
                aviso.Add("EMITIDA EM AMBIENTE DE HOMOLOGAÇÃO");

            if (ViewModel.Cancelada)
                aviso.Add("CANCELADA");

            if (aviso.Count > 0)
                aviso.Add("SEM VALOR FISCAL");

            MainVerticalStack.Add(new LinhaSolida(contexto, 1));

            if (aviso.Count > 0)
            {
                MainVerticalStack.Add(TextBlock.Centro(contexto, String.Join(" - ", aviso), fn));
                MainVerticalStack.Add(ls);
            }

            MainVerticalStack.Add(TextBlock.Centro(contexto, $"Número: {ViewModel.NfNumero.ToString().PadLeft(9, '0')} Série: {ViewModel.NfSerie.ToString().PadLeft(3, '0')} Emissão: {ViewModel.DataHoraEmissao.FormatarDataHora()}", fr));
            MainVerticalStack.Add(lv3);
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}