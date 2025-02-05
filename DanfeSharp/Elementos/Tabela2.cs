using DanfeSharp.Graphics;
using org.pdfclown.documents.contents.colorSpaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DanfeSharp
{
    internal class Tabela2 : ElementoBase
    {
        public List<List<TabelaColuna>> Colunas { get; private set; }
        public List<List<String>> Linhas { get; private set; }
        public float PaddingSuperior { get; private set; }
        public float PaddingInferior { get; private set; }
        public float PaddingHorizontal { get; private set; }
        public bool BordasCaixa { get; set;} = false;

        public int LinhaAtual { get; private set; }
        public float TamanhoFonteCabecalho { get; private set; }

        private float _DY;
        private float _DY1;

        public Fonte FonteCorpo { get; private set; }
        public Fonte FonteCabecalho { get; private set; }

        public Tabela2(ElementoContexto contexto) : base(contexto)
        {
            Colunas = new List<List<TabelaColuna>> { new List<TabelaColuna>() };
            Linhas = new List<List<string>>();
            LinhaAtual = 0;
            TamanhoFonteCabecalho = 6;

            PaddingHorizontal = 0.6F;
            PaddingSuperior = 0.75F;
            PaddingInferior = 0.3F;

            // 7.7.7 Conteúdo dos Campos do Quadro “Dados dos Produtos/Serviços”
            // Deverá ter tamanho mínimo de seis(6) pontos, ou 17 CPP.
            FonteCorpo = Estilo.CriarFonteRegular(6F);
            FonteCabecalho = Estilo.CriarFonteRegular(5F);
        }

        public Tabela2 ComColuna(float larguraP, AlinhamentoHorizontal ah, params String[] cabecalho)
        {
            Colunas.Last().Add(new TabelaColuna(cabecalho, larguraP, ah));
            return this;
        }

        public Tabela2 NovaGrupoColunas()
        {
            Colunas.Add(new List<TabelaColuna>());
            return this;
        }

        public void AdicionarLinha(List<String> linha)
        {
            AdicionarLinha(new List<List<string>> { linha });
        }

        public void AdicionarLinha(List<List<String>> linhas)
        {
            if (linhas.Count != Colunas.Count) throw new ArgumentException(nameof(linhas));

            for(int i = 0; i < linhas.Count; i++)
            {
                var cols = Colunas[i];
                if (linhas[i].Count != cols.Count) throw new ArgumentException(nameof(linhas));

                Linhas.Add(linhas[i]);
            }
        }

        public void AjustarLarguraColunas()
        {
            foreach(var cols in Colunas)
            {
                var sw = cols.Sum(x => x.PorcentagemLargura);

                if (sw > 100F) throw new InvalidOperationException();

                var w = (100F - sw) / (float)cols.Where(x => x.PorcentagemLargura == 0).Count();

                foreach (var c in cols.Where(x => x.PorcentagemLargura == 0))
                    c.PorcentagemLargura = w;
            }
        }

        public void AjustarAltura()
        {
            var fixHeight = 1; // Margem de erro no calculo das linhas para impedir que não imprima a linha devido a uma pequena diferença na altura
            Height = CalcularAlturarCabacalho() + CalcularAlturarLinhas() + fixHeight;
        }

        private float CalcularAlturarCabacalho()
        {
            float cabecalhoAltura = 0;
            foreach(var cols in Colunas)
            {
                var ml = cols.Max(c => c.Cabecalho.Length);
                cabecalhoAltura += ml * FonteCabecalho.AlturaLinha + 1F;
            }

            return cabecalhoAltura;
        }

        private float CalcularAlturarLinhas()
        {
            return (FonteCorpo.AlturaLinha + ((PaddingSuperior + PaddingInferior) / Colunas.Count)) * Linhas.Count;
        }

        private Boolean DesenharLinha(Gfx gfx)
        {
            var y = _DY;
            _DY1 = _DY;

            List<TextBlock[]> tbs = new List<TextBlock[]>();
            var tbms = new List<float>();
            float tbm = 0;

            for(int ci = 0; ci < Colunas.Count; ci++)
            {
                var cols = Colunas[ci];
                var tb = new TextBlock[cols.Count];
                var tbm2 = 0f;
                var lna = LinhaAtual + ci;
                var x = X;

                tbs.Add(tb);

                for (int i = 0; i < cols.Count; i++)
                {
                    var c = cols[i];
                    var v = Linhas[lna][i];

                    float w = Width * c.PorcentagemLargura / 100F;
                    var width =  w - 2F * Estilo.PaddingHorizontal;

                    if (!String.IsNullOrWhiteSpace(v))
                    {
                        v = TextOverflow.TratarTexto(v, FonteCorpo, width, addElipses: false);

                        tb[i] = new TextBlock(Contexto, v, FonteCorpo)
                        {
                            Width = width,
                            X = x + PaddingHorizontal,
                            Y = y + PaddingSuperior,
                            AlinhamentoHorizontal = c.AlinhamentoHorizontal
                        };

                        var tbh = Math.Max(tb[i].Height, FonteCorpo.AlturaLinha);

                        if (tbh > tbm2)
                            tbm2 = tbh;
                    }
                    else
                    {
                        tb[i] = new TextBlock(Contexto, String.Empty, FonteCorpo)
                        {
                            Width = width,
                            X = x + PaddingHorizontal,
                            Y = y + PaddingSuperior,
                            AlinhamentoHorizontal = c.AlinhamentoHorizontal
                        };
                    }

                    x += w;
                }

                y += tbm2;
                tbm += tbm2;
                tbms.Add(tbm2);
            }

            if (tbm + _DY + PaddingInferior + PaddingSuperior > BoundingBox.Bottom)
                return false;

            if (BordasCaixa)
            {
                gfx.DrawRectangle(X, _DY, Width, tbm + PaddingInferior + PaddingSuperior);
                gfx.Stroke();
            }

            for(int i = 0; i < tbs.Count; i++)
            {
                var bordaHeight = tbms[i] + PaddingInferior + PaddingSuperior;

                foreach(var t in tbs[i])
                {
                    if (BordasCaixa)
                    {
                        var bx = t.X - PaddingHorizontal;
                        var by = t.Y - PaddingSuperior;
                        gfx.PrimitiveComposer.DrawLine(new PointF(bx, by).ToPointMeasure(), new PointF(bx, by + bordaHeight).ToPointMeasure());
                        gfx.PrimitiveComposer.Stroke();
                    }

                    t?.Draw(gfx);
                }
            }

            _DY += tbm + PaddingSuperior + PaddingInferior;

            return true;
        }

        public void DesenharCabecalho(Gfx gfx)
        {
            var rectHeight = CalcularAlturarCabacalho();
            gfx.PrimitiveComposer.BeginLocalState();
            gfx.PrimitiveComposer.SetFillColor(new DeviceRGBColor(245 / 255d, 245 / 255d, 245 / 255d));
            gfx.DrawRectangle(X, Y, Width, rectHeight);
            gfx.PrimitiveComposer.FillStroke();
            gfx.PrimitiveComposer.End();

            _DY = Y;

            foreach(var cols in Colunas)
            {
                var ml = cols.Max(c => c.Cabecalho.Length);
                float ac = ml * FonteCabecalho.AlturaLinha + 1F;
                float x = X;

                foreach (var coluna in cols)
                {
                    float w = Width * coluna.PorcentagemLargura / 100F;
                    var r = new RectangleF(x, _DY, w, ac);

                    var tb = new TextStack(Contexto, r.InflatedRetangle(1F));
                    tb.AlinhamentoVertical = AlinhamentoVertical.Centro;
                    tb.AlinhamentoHorizontal = coluna.AlinhamentoHorizontal;

                    foreach (var item in coluna.Cabecalho)
                    {
                        tb.AddLine(item, FonteCabecalho);
                    }

                    tb.Draw(gfx);

                    if (BordasCaixa)
                    {
                        var prmeiraColuna = x == X;
                        if (!prmeiraColuna)
                        {
                            gfx.PrimitiveComposer.DrawLine(new PointF(r.X, r.Y).ToPointMeasure(), new PointF(r.X, r.Bottom).ToPointMeasure());
                            gfx.PrimitiveComposer.Stroke();
                        }
                    }

                    x += w;
                }

                _DY += ac;
            }
        }


        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);
            gfx.SetLineWidth(0.25F);

            DesenharCabecalho(gfx);

            while (LinhaAtual < Linhas.Count)
            {
                Boolean r = DesenharLinha(gfx);

                if (r)
                {
                    if (LinhaAtual > 0 && !BordasCaixa)
                    {
                        gfx.PrimitiveComposer.BeginLocalState();
                        gfx.PrimitiveComposer.SetStrokeColor(new DeviceRGBColor(0.5, 0.5, 0.5));
                        gfx.PrimitiveComposer.SetLineDash(new org.pdfclown.documents.contents.LineDash(new double[] { 6, 1 }));
                        gfx.PrimitiveComposer.DrawLine(new PointF(BoundingBox.Left, _DY1).ToPointMeasure(), new PointF(BoundingBox.Right, _DY1).ToPointMeasure());
                        gfx.PrimitiveComposer.Stroke();
                        gfx.PrimitiveComposer.End();
                    }

                    LinhaAtual += Colunas.Count;
                }
                else
                    break;
            }
        }

        public override bool PossuiContono => false;
    }
}
