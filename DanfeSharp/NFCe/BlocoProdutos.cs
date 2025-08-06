using System;
using System.Collections.Generic;
using DanfeSharp.Blocos;

namespace DanfeSharp.NFCe
{
    internal class BlocoProdutos : BlocoNFeBase
    {
        public BlocoProdutos(BlocoNFeContexto contexto) : base(contexto)
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
                    .ComColuna(29, ad, "Vl Total")
                    .NovaGrupoColunas()
                    .ComColuna(15, ae, "")
                    .ComColuna(70, ae, "")
                    .ComColuna(15, ad, "");

                tbl.LinhaSeparadora = false;

                foreach (var item in ViewModel.Produtos)
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
                        },
                        GerarLinhaAcrescimoDesconto(item)
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
                    .ComColuna(20, ad, "Vl Total")
                    .NovaGrupoColunas()
                    .ComColuna(10, ae, "")
                    .ComColuna(75, ae, "")
                    .ComColuna(15, ad, "");

                tbl.LinhaSeparadora = false;

                foreach (var item in ViewModel.Produtos)
                {
                    tbl.AdicionarLinha(new List<List<string>>
                    {
                        new List<string>
                        {
                            item.Codigo,
                            item.Descricao,
                            item.Quantidade.Formatar(Config.FormatoProdutoQuantidade),
                            item.ValorUnitario.Formatar(Config.FormatoProdutoValorUnitario),
                            item.ValorTotal.Formatar()
                        },
                        GerarLinhaAcrescimoDesconto(item)
                    });
                }

                tbl.AjustarAltura();
                MainVerticalStack.Add(tbl);
            }
        }

        private List<string> GerarLinhaAcrescimoDesconto(Modelo.ProdutoViewModel item)
        {
            var haDesc = item.ValorDesconto > 0;
            var haAcresc = item.ValorAcrescimo > 0;

            if (haDesc || haAcresc)
            {
                var descPercentual = item.ValorDesconto / item.ValorTotal * 100;
                var desconto = haDesc ? $"Desconto -{item.ValorDesconto.Formatar()} (-{descPercentual.Formatar(Formatador.FormatoPercentual)}%)" : "";

                var acrescPercentual = item.ValorAcrescimo / item.ValorTotal * 100;
                var acrescimo = haAcresc ? $"Acréscimo {item.ValorAcrescimo.Formatar()} ({acrescPercentual.Formatar(Formatador.FormatoPercentual)}%)" : "";

                var valorLiquido = item.ValorTotal - item.ValorDesconto + item.ValorAcrescimo;

                return new List<string> { "", String.Join("    ", acrescimo, desconto), valorLiquido.Formatar() };
            }

            return new List<string> { "", "", "" };
        }

        public override PosicaoBloco Posicao => PosicaoBloco.Topo;
        public override bool PossuiContono => false;
    }
}