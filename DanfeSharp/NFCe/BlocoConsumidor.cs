using DanfeSharp.Blocos;

namespace DanfeSharp.NFCe
{
    internal class BlocoConsumidor : BlocoBase
    {
        public BlocoConsumidor(BlocoContexto contexto) : base(contexto)
        {
            var fn = Estilo.FonteNFCeNegrito2;
            var fr = Estilo.FonteNFCe3;
            var ls = ElementoVazio.T0();
            var lv3 = ElementoVazio.T3();

            var dest = ViewModel.Destinatario;

            if (dest == null)
            {
                MainVerticalStack.Add(new LinhaSolida(contexto, 1));
                MainVerticalStack.Add(TextBlock.Centro(contexto, "CONSUMIDOR NÃO IDENTIFICADO", fn));
                return;
            }

            var nome = string.IsNullOrEmpty(dest.NomeFantasia) ? dest.RazaoSocial : dest.NomeFantasia;
            MainVerticalStack.Add(new LinhaSolida(contexto, 1));
            MainVerticalStack.Add(TextBlock.Centro(contexto, "CONSUMIDOR", fn));
            MainVerticalStack.Add(lv3);
            MainVerticalStack.Add(TextBlock.Centro(contexto, $"CPF: {dest.CnpjCpf} {nome}", fr));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, $"End.: {dest.EnderecoLinha1}", fr));
            MainVerticalStack.Add(ls);
            MainVerticalStack.Add(TextBlock.Centro(contexto, $"Bairro: {dest.EnderecoLinha2} {dest.EnderecoLinha3}", fr));
            MainVerticalStack.Add(new LinhaSolida(contexto, 1));
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}