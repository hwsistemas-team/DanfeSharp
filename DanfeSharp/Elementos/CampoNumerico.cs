using System;
using DanfeSharp.Graphics;

namespace DanfeSharp.Elementos
{
    /// <summary>
    /// Campo para valores numéricos.
    /// </summary>
    internal class CampoNumerico : Campo
    {
        private double? ConteudoNumerico { get; set; }
        public int CasasDecimais { get; set; }

        public CampoNumerico(ElementoContexto contexto, string cabecalho, double? conteudoNumerico, int casasDecimais = 2) : base(contexto, cabecalho, null, AlinhamentoHorizontal.Direita)
        {
            CasasDecimais = casasDecimais;
            ConteudoNumerico = conteudoNumerico;
        }

        protected override void DesenharConteudo(Gfx gfx)
        {
            base.Conteudo = ConteudoNumerico.HasValue ? ConteudoNumerico.Value.ToString($"N{CasasDecimais}", Formatador.Cultura) : null;
            base.DesenharConteudo(gfx);
        }
    }
}
