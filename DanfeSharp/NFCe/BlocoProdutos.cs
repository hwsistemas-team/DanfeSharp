using System;
using System.Collections.Generic;
using DanfeSharp.Blocos;
using DanfeSharp.Modelo;

namespace DanfeSharp.NFCe
{
    internal class BlocoProdutos : BlocoBase
    {
        public BlocoProdutos(DanfeViewModel viewModel, Estilo estilo) : base(viewModel, estilo)
        {
            var ae = AlinhamentoHorizontal.Esquerda;
            var ac = AlinhamentoHorizontal.Centro;
            var ad = AlinhamentoHorizontal.Direita;

            var tbl = new Tabela2(Estilo)
                .ComColuna(10, ae, "Código")
                .ComColuna(30, ae, "Descrição")
                .ComColuna(20, ac, "Qtd Un x")
                .ComColuna(20, ac, "Vl Unit =")
                .ComColuna(20, ad, "Vl Total");

            tbl.AjustarLarguraColunas();

            foreach(var item in viewModel.Produtos)
            {
                tbl.AdicionarLinha(new List<string>
                {
                    item.Codigo,
                    item.Descricao,
                    item.Quantidade.Formatar(),
                    item.ValorUnitario.Formatar(),
                    item.ValorTotal.Formatar()
                });
            }

            tbl.AjustarAltura();
            MainVerticalStack.Add(tbl);
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}