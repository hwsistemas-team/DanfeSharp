using System;
using System.Collections.Generic;
using DanfeSharp.Blocos;

namespace DanfeSharp.NFCe
{
    internal class BlocoProdutos : BlocoBase
    {
        public BlocoProdutos(BlocoContexto contexto) : base(contexto)
        {
            var ae = AlinhamentoHorizontal.Esquerda;
            var ac = AlinhamentoHorizontal.Centro;
            var ad = AlinhamentoHorizontal.Direita;

            if (Contexto.Config.NFCeItensEm2Linhas)
            {
                var tbl = new Tabela2(contexto)
                    .ComColuna(15, ae, "Código")
                    .ComColuna(85, ae, "Descrição")
                    .NovaGrupoColunas()
                    .ComColuna(15, ae, "")
                    .ComColuna(28, ac, "Qtd Un x")
                    .ComColuna(28, ac, "Vl Unit =")
                    .ComColuna(29, ad, "Vl Total");

                foreach(var item in ViewModel.Produtos)
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
                            item.Quantidade.Formatar(Config.FormatoProdutoQuantidade),
                            item.ValorUnitario.Formatar(Config.FormatoProdutoValorUnitario),
                            item.ValorTotal.Formatar()
                        }
                    });
                }

                tbl.AjustarAltura();
                MainVerticalStack.Add(tbl);
            }
            else
            {
                var tbl = new Tabela2(contexto)
                .ComColuna(10, ae, "Código")
                .ComColuna(30, ae, "Descrição")
                .ComColuna(20, ac, "Qtd Un x")
                .ComColuna(20, ac, "Vl Unit =")
                .ComColuna(20, ad, "Vl Total");

                foreach(var item in ViewModel.Produtos)
                {
                    tbl.AdicionarLinha(new List<string>
                    {
                        item.Codigo,
                        item.Descricao,
                        item.Quantidade.Formatar(Config.FormatoProdutoQuantidade),
                        item.ValorUnitario.Formatar(Config.FormatoProdutoValorUnitario),
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