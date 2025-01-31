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

            if (viewModel.NFCeItensEm2Linhas)
            {
                var tbl = new Tabela2(Estilo)
                    .ComColuna(15, ae, "Código")
                    .ComColuna(85, ae, "Descrição")
                    .NovaGrupoColunas()
                    .ComColuna(15, ae, "")
                    .ComColuna(28, ac, "Qtd Un x")
                    .ComColuna(28, ac, "Vl Unit =")
                    .ComColuna(29, ad, "Vl Total");

                foreach(var item in viewModel.Produtos)
                {
                    tbl.AdicionarLinha(new List<List<string>>
                    {
                        new List<string>
                        {
                            item.Codigo,
                            item.Descricao
                        },
                        new List<string>
                        {
                            null,
                            item.Quantidade.Formatar(),
                            item.ValorUnitario.Formatar(),
                            item.ValorTotal.Formatar()
                        }
                    });
                }

                tbl.AjustarAltura();
                MainVerticalStack.Add(tbl);
            }
            else
            {
                var tbl = new Tabela2(Estilo)
                .ComColuna(10, ae, "Código")
                .ComColuna(30, ae, "Descrição")
                .ComColuna(20, ac, "Qtd Un x")
                .ComColuna(20, ac, "Vl Unit =")
                .ComColuna(20, ad, "Vl Total");

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
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}