using DanfeSharp.Blocos;
using DanfeSharp.Modelo;

namespace DanfeSharp.NFCe
{
    internal class BlocoConsumidor : BlocoBase
    {
        public BlocoConsumidor(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var fn = Estilo.FonteNFCeNegrito2;
            var fr = Estilo.FonteNFCe3;
            var ls = ElementoVazio.T0();
            var lv3 = ElementoVazio.T3();

            var dest = ViewModel.Destinatario;
            var w = viewModel.PaginaLargura;

            if (dest == null)
            {
                MainVerticalStack.Add(new LinhaSolida(1));
                MainVerticalStack.Add(TextBlock.Centro("CONSUMIDOR NÃO IDENTIFICADO", fn, w));
                return;
            }

            var nome = string.IsNullOrEmpty(dest.NomeFantasia) ? dest.RazaoSocial : dest.NomeFantasia;
            MainVerticalStack.Add(new LinhaSolida(1));
            MainVerticalStack.Add(TextBlock.Centro("CONSUMIDOR", fn, w));
            MainVerticalStack.Add(lv3);
            MainVerticalStack.Add(TextBlock.Centro($"CPF: {dest.CnpjCpf} {nome}", fr, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro($"End.: {dest.EnderecoLinha1}", fr, w));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro($"Bairro: {dest.EnderecoLinha2} {dest.EnderecoLinha3}", fr, w));
            MainVerticalStack.Add(new LinhaSolida(1));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}